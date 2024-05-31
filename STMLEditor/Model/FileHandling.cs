using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Media.Converters;
using System.IO.Enumeration;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using STMLEditor.Extensions;
using Microsoft.WindowsAPICodePack.Dialogs;



namespace STMLEditor.Model
{
    public static class FileHandling
    {
        private static Project Project => Project.CurrentProject;
        public static SaveStatus Status { get; set; } = SaveStatus.Unknown;
        public static string EditingPath { get; set; } = "";

        public static void Save()
        {
            if (EditingPath == "")
            {
                SaveAs();
            }
            else
            {
                ActuallySave();
            }
        }

        public static void SaveAs()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C://Users";
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            dialog.Title = "Select your Unity Project's Assets Folder";

            if (dialog.ShowDialog() is CommonFileDialogResult.Ok)
            {
                EditingPath = Path.Combine(dialog.FileName, "AnimetraScreenText");
            }

            ActuallySave();
        }

        public static void ActuallySave()
        {
            string textsPath = Path.Combine(EditingPath, "Texts");

            Directory.CreateDirectory(EditingPath);
            // Save projectfile here
            Directory.CreateDirectory(textsPath);

            byte[] header = JsonSerializer.SerializeToUtf8Bytes(Project.Header, new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true });
            byte[] settings = JsonSerializer.SerializeToUtf8Bytes(Project.Settings, new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true });

            using (var stream = File.Open(Path.Combine(EditingPath, "ScreenTextProject.stproj"), FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream, Encoding.UTF8, false))
                {
                    writer.Write(header);
                    writer.Write(settings);
                }
            }

            foreach (var library in Project.Hierarchy)
            {
                string libraryPath = Path.Combine(textsPath, library.Header.ID);

                foreach (var document in library.Children)
                {
                    XmlWriterSettings xmlSettings = new();
                    xmlSettings.OmitXmlDeclaration = true;
                    xmlSettings.Indent = true;
                    Directory.CreateDirectory(libraryPath);

                    using (XmlWriter writer = XmlWriter.Create(Path.Combine(libraryPath, document.Header.ID + ".xml"), xmlSettings))
                    {
                        XDocument xDocument =
                        new XDocument(
                            new XElement("root",
                                new XElement("stml"),
                                new XElement("header",
                                    new XElement("title", Project.Header.Name),
                                    new XElement("description", Project.Header.Description),
                                    new XElement("language", Project.Header.Description),
                                    new XElement("author", Project.Header.Description),
                                    new XElement("version", Project.Header.Description)
                            ),
                                new XElement("screentext", document.Children.Select(x =>
                                    new XElement("section", x.Children.Select(y =>
                                    {
                                        XElement text = null;
                                        if (y is STMLTerm term)
                                        {
                                            text = new XElement("term", term.Content);
                                        }
                                        else if (y is STMLExpression expression)
                                        {
                                            text = new XElement("expression", expression.Content);
                                        }

                                        return text;
                                    }
                                    ))
                                ))
                        )
                    );

                        xDocument.Save(writer);
                    }
                }
            }

            Status = SaveStatus.Saved;
        }

        public static void Open()
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "Animetra ScreenText File"; // Default file name
            dialog.DefaultExt = ".ast"; // Default file extension
            dialog.Filter = "Text documents (.ast)|*.ast"; // Filter files by extension

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                byte[] byteArray;

                using (var stream = File.Open(dialog.FileName, FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream, Encoding.UTF8, false))
                    {
                        byteArray = reader.ReadAllBytes();
                    }
                }

                Project.CurrentProject = JsonSerializer.Deserialize<Project>(new ReadOnlySpan<byte>(byteArray));
                Status = SaveStatus.Unknown;
                EditingPath = dialog.FileName;
            }

        }
    }

    public enum SaveStatus
    {
        Unknown,
        UnsavedChanges,
        Saved
    }
}
