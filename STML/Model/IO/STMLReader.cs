namespace STML.Model
{
    public class STMLReader
    {/*
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

                STMLLibrary? newLibrary = newProject.AddLibrary();
                if (libDoc?.Root is XElement libraryRoot && libraryRoot.Element("header") is XElement libraryHeader)
                {
                    newLibrary.Header = new STMLHeader(
                                            libraryHeader.Element("name")?.Value ?? "{MISSING NAME}",
                                            libraryHeader.Element("id")?.Value ?? "{MISSING ID}",
                                            libraryHeader.Element("comments")?.Value ?? "");
                    newLibrary.Language = libraryHeader.Element("language")?.Value ?? "";
                }
                else
                {
                    throw new Exception("File has no valid structure");
                }

                foreach (var document in Directory.GetFiles(folder).Where(x => x != libraryInfoPath && Path.GetExtension(x) == ".xml"))
                {
                    XDocument stmlDoc;
                    try
                    {
                        using (var reader = new StreamReader(document))
                        {
                            stmlDoc = XDocument.Load(reader);
                        }
                    }
                    catch
                    {
                        throw new Exception($"could not read file {document}.");
                    }

                    if (stmlDoc?.Element("root") is XElement documentRoot && documentRoot.Element("header") is XElement documentHeader && documentRoot.Element("screentext") is XElement documentScreentext)
                    {
                        STMLDocument newDocument = (STMLDocument)newLibrary.AddChild();
                        newDocument.Header = new STMLHeader(
                                                documentHeader.Element("name")?.Value ?? "{MISSING NAME}",
                                                documentHeader.Element("id")?.Value ?? "{MISSING ID}",
                                                documentHeader.Element("comments")?.Value ?? "");

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

                            newSection!.Header = new STMLHeader(
                                                    section.Attribute("name")?.Value ?? "{MISSING NAME}",
                                                    section.Attribute("id")?.Value ?? "{MISSING ID}",
                                                    section.Attribute("comments")?.Value ?? "");

                            foreach (XElement text in section.Elements())
                            {
                                STMLElement newText = newSection.AddChild();
                                newText.Header = new STMLHeader(
                                                    text.Attribute("name")?.Value ?? "{MISSING NAME}",
                                                    text.Attribute("id")?.Value ?? "{MISSING ID}",
                                                    text.Attribute("comments")?.Value ?? "");

                                if (newText is STMLTerm term)
                                {
                                    term.Content = new STMLString(text.Value);
                                    term.ReferenceName = text.Attribute("referenceName")?.Value ?? "";
                                }

                                if (newText is STMLExpression expression)
                                {
                                    expression.Content = new STMLFormattableString(text.Value, expression, null);
                                    expression.Narrator = new STMLFormattableString(text.Attribute("narrator")?.Value ?? "", expression, null);
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
            }

            return newProject;
        }*/
    }
}
