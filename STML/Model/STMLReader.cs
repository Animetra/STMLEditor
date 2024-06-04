using STML.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace STML.Model
{
    public class STMLReader
    {
        public STMLProject ReadProject(string projectPath)
        {
            STMLProject newProject = new STMLProject();

            // TODO: Check integrity
            // TODO: ProjectFile


            // Contents
            string contentPath = Path.Combine(projectPath, Filenames.ContentFolderName);

            foreach (var folder in Directory.GetDirectories(contentPath))
            {
                
                // TODO: Check integrity
                string libraryInfoPath = Path.Combine(folder, Filenames.LibraryInfoName);
                XDocument libDoc;

                using (var reader = new StreamReader(libraryInfoPath))
                {
                    libDoc = XDocument.Load(reader);
                }

                STMLLibrary? newLibrary = null;
                if (libDoc?.Root is XElement libraryRoot && libraryRoot.Element("header") is XElement libraryHeader)
                {
                    newLibrary = new STMLLibrary();
                    newLibrary.Header.Name = libraryHeader.Element("name")?.Value ?? "{MISSING NAME}";
                    newLibrary.Header.ID = libraryHeader.Element("id")?.Value ?? "{MISSING ID}";
                    newLibrary.Header.Comments = libraryHeader.Element("comments")?.Value ?? "";
                    newLibrary.Language = libraryHeader.Element("language")?.Value ?? "";
                }
                else
                {
                    throw new Exception("File has no valid structure");
                }

                foreach (var document in Directory.GetFiles(folder).Where(x => x != libraryInfoPath))
                {
                    XDocument stmlDoc;

                    using (var reader = new StreamReader(document))
                    {
                        stmlDoc = XDocument.Load(reader);
                    }

                    if (stmlDoc?.Element("root") is XElement documentRoot && documentRoot.Element("header") is XElement documentHeader && documentRoot.Element("screentext") is XElement documentScreentext)
                    {
                        STMLDocument newDocument = (STMLDocument)newLibrary.AddChild();
                        newDocument.Header.Name = documentHeader.Element("name")?.Value ?? "{MISSING NAME}";
                        newDocument.Header.ID = documentHeader.Element("id")?.Value ?? "{MISSING ID}";
                        newDocument.Header.Comments = documentHeader.Element("comments")?.Value ?? "";

                        foreach (XElement section in documentScreentext.Elements())
                        {
                            STMLSection? newSection = null;
                            if (section.Name == "dictionary")
                            {
                                newSection = (STMLSection)newDocument.AddDictionary();
                            }
                            else if (section.Name == "script")
                            {
                                newSection = (STMLSection)newDocument.AddScript();
                            }

                            newSection!.Header.Name = section.Attribute("name")?.Value ?? "{MISSING NAME}";
                            newSection!.Header.ID = section.Attribute("id")?.Value ?? "{MISSING ID}";
                            newSection!.Header.Comments = section.Attribute("comments")?.Value ?? "";

                            foreach (XElement text in section.Elements())
                            {
                                STMLText newText = (STMLText)newSection.AddChild();
                                newText.Header.Name = text.Attribute("name")?.Value ?? "{MISSING NAME}";
                                newText.Header.ID = text.Attribute("id")?.Value ?? "{MISSING ID}";
                                newText.Header.Comments = text.Attribute("comments")?.Value ?? "";
                                newText.Content = text.Value;

                                if (newText is STMLExpression expression)
                                {
                                    expression.Narrator = text.Attribute("narrator")?.Value ?? "";
                                    expression.Style = text.Attribute("style")?.Value ?? "";
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception($"File has no valid structure: {document}");
                    }
                }

                newProject.AddLibrary();
            }

            return newProject;
        }
    }
}
