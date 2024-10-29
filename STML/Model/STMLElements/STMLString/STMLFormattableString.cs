using System;
using System.Linq;


namespace STML.Model
{
    public class STMLFormattableString : STMLString
    {
        public ContentFormat? ContentFormat { get; set; }
        public STMLExpression? Expression { get; set; }
        public string[]? Styles => Expression?.GetActiveStyles();


        private string _resolved;
        public string Resolved
        {
            get => _resolved;
            private set { _resolved = value; OnPropertyChanged(); }

        }

        private string _formatted;
        public string Formatted
        {
            get => _formatted;
            private set { _formatted = value; OnPropertyChanged(); }
        }

        public STMLFormattableString(string text = "", STMLExpression? expression = null, ContentFormat? format = null) : base(text)
        {
            ContentFormat = format;
            Expression = expression;
        }

        // TODO: Maybe better to do with an xml reader? Make some research about that
        public void Resolve()
        {
            string text = Plain;
            if (Expression != null && text != "" && text != null)
            {
                int[]? refs = text!.FindAll("<ref value=");
                if (refs != null)
                {
                    foreach (int referenceIndex in refs)
                    {
                        int tagEnd = text!.IndexOf("/>", referenceIndex);

                        string? referenceName = text.GetAttribute(text!.IndexOf("value=", referenceIndex));

                        if (referenceName != "" && referenceName != null)
                        {
                            STMLTerm term = Expression.ParentLibrary.References.First(x => x.ReferenceName == referenceName);
                            text = text.ReplaceAt(referenceIndex, tagEnd + 2 - referenceIndex, term.Content);
                        }
                    }
                }

                Resolved = text;
            }
        }

        // TODO: Make research, if there is a public library (maybe from MS?) to do that:
        public void Format(ContentFormat format)
        {
            string text = Resolved;
            if (format is Model.ContentFormat.Unity)
            {
                text = text
                        .Trim()
                        .Replace("<style value=", "<style=")
                        .Replace("<color value=", "<color=")
                        .Replace("<size value=", "<size=")
                        .Replace("<material value=", "<material=")
                        .Replace("<quad value=", "<quad");

                if (Styles != null)
                {
                    foreach (string style in Styles)
                    {
                        text = text.NestInTags("style", style);
                    }
                }

                Formatted = text;
            }
            else if (ContentFormat is null)
            {
                Formatted = text;
            }
            else
            {
                throw new NotImplementedException();
            }
        }
    }
}
