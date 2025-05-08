using System;
using System.Linq;


namespace STML.Model
{
    public class STMLFormattableString : STMLString
    {
        public ContentFormat? ContentFormat { get; set; }
        public STMLExpression? Expression { get; set; }
        public string[]? Styles => Expression?.GetActiveStyles();

        public string Resolved => Resolve();
        public string Formatted => Format();


        public STMLFormattableString(string text = "", STMLExpression? expression = null, ContentFormat? format = null) : base(text)
        {
            ContentFormat = format;
            Expression = expression;
        }

        public override T Clone<T>()
        {
            return (new STMLFormattableString(Plain, Expression, ContentFormat) as T)!;
        }

        // TODO: Maybe better to do with an xml reader? Make some research about that
        public string Resolve()
        {
            string text = Plain ?? "";
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
                            STMLTerm term = Expression.ParentProject.References.First(x => x.ReferenceName == referenceName);
                            text = text.ReplaceAt(referenceIndex, tagEnd + 2 - referenceIndex, term.Content[Expression.ParentProject.ActiveLanguage]);
                        }
                    }
                }
            }

            return text!;
        }

        // TODO: Make research, if there is a public library (maybe from MS?) to do that:
        public string Format()
        {
            string text = Resolved;
            if (text != "")
            {
                if (ContentFormat is Model.ContentFormat.Unity)
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
                }
                else if (ContentFormat is null)
                {

                }
                else
                {
                    throw new NotImplementedException();
                }
            }

            return text;
        }
    }
}
