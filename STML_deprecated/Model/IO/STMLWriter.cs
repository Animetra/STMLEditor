using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Reflection;

namespace STML.Model
{
    public class STMLWriter
    {
        public void WriteProject(STMLProject project, string projectPath)
        {
            string version = Assembly.GetAssembly(GetType()).GetName().Version.ToString();

            string projectFilePath = Path.Combine(projectPath, Filenames.ProjectFileName);
            string contentPath = Path.Combine(projectPath, Filenames.ContentFolderName);
            string projectBackupFolderPath = Path.Combine(Path.GetDirectoryName(projectPath), "TempBackup");

            if (File.Exists(projectPath))
            {
                File.Copy(projectPath, projectBackupFolderPath);
                File.Delete(projectPath);
            }

            Directory.CreateDirectory(projectPath);

            XmlWriterSettings xmlSettings = new();
            xmlSettings.OmitXmlDeclaration = true;
            xmlSettings.Indent = true;

            using (XmlWriter writer = XmlWriter.Create(projectFilePath, xmlSettings))
            {
                XDocument xDocument =
                new XDocument(
                        new XElement("root",
                            new XElement("stml",
                                new XAttribute("version", version)),
                            new XElement("libraries",
                                project.Libraries.Select(x => new XElement("library", x.Header.ID)))));

                xDocument.Save(writer);
            }

            Directory.CreateDirectory(contentPath);

            foreach (STMLLibrary library in project.Libraries)
            {
                string libraryPath = Path.Combine(contentPath, library.Header.ID);
                Directory.CreateDirectory(libraryPath);

                using (XmlWriter writer = XmlWriter.Create(Path.Combine(libraryPath, Filenames.LibraryInfoName), xmlSettings))
                {
                    XDocument xDocument =
                    new XDocument(
                            new XElement("root",
                                new XElement("stml",
                                    new XAttribute("version", version)),
                                new XElement("header",
                                    new XElement("name", library.Header.Name),
                                    new XElement("id", library.Header.ID),
                                    new XElement("comments", library.Header.Comments),
                                    new XElement("language", library.Language)),
                                new XElement("documents",
                                    library.Children.Select(x => new XElement("document", x.Header.ID)))));

                    xDocument.Save(writer);
                }

                foreach (STMLDocument document in library.Children)
                {
                    using (XmlWriter writer = XmlWriter.Create(Path.Combine(libraryPath, document.Header.ID + ".xml"), xmlSettings))
                    {
                        XDocument xDocument =
                        new XDocument(
                            new XElement("root",
                                new XElement("stml",
                                    new XAttribute("version", version)),
                                new XElement("header",
                                    new XElement("name", document.Header.Name),
                                    new XElement("id", document.Header.ID),
                                    new XElement("comments", document.Header.Comments)),
                                new XElement("screentext", document.Children.Select(x =>
                                {
                                    if (x is STMLDictionary dictionary)
                                    {
                                        return new XElement("dictionary", x.Children.Select(y =>
                                                 new XElement("term",
                                                     new XAttribute("name", y.Header.Name),
                                                     new XAttribute("id", y.Header.ID),
                                                     new XAttribute("comments", y.Header.Comments),
                                                     new XAttribute("referenceName", ((STMLTerm)y).ReferenceName),
                                                     ((STMLTerm)y).Content.Plain)),
                                                 new XAttribute("name", x.Header.Name),
                                                 new XAttribute("id", x.Header.ID),
                                                 new XAttribute("comments", x.Header.Comments));
                                    }
                                    else if (x is STMLScript script)
                                    {
                                        return new XElement("script", x.Children.Select(y =>
                                                 new XElement("expression",
                                                     new XAttribute("name", y.Header.Name),
                                                     new XAttribute("id", y.Header.ID),
                                                     new XAttribute("comments", y.Header.Comments),
                                                     new XAttribute("narrator", ((STMLExpression)y).Narrator.Plain),
                                                     new XAttribute("style", ((STMLExpression)y).Style),
                                                     ((STMLExpression)y).Content.Plain)),
                                                 new XAttribute("name", x.Header.Name),
                                                 new XAttribute("id", x.Header.ID),
                                                 new XAttribute("comments", x.Header.Comments));
                                    }
                                    else
                                    {
                                        throw new InvalidOperationException("object type not known");
                                    }
                                }))));

                        xDocument.Save(writer);
                    }
                }
            }

            File.Delete(projectBackupFolderPath);
        }
    }
}
