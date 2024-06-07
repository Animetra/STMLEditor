using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace STML.Model
{
    public class STMLFormattableString : STMLString
    {
        public ContentFormat? ContentFormat { get; set; }
        public STMLExpression? Expression { get; set; }
        public string[]? Styles => Expression?.GetActiveStyles();
        public override string Plain
        {
            get => _plain;
            set { _plain = value; Resolve(); Format(); }
        }

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
            private set { _formatted= value; OnPropertyChanged(); }
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
            if (Expression is not null && text is not ("" or null))
            {
                int[]? refs = text!.FindAll("<ref value=");
                if (refs is not null)
                {
                    foreach (int referenceIndex in refs)
                    {
                        int tagEnd = text!.IndexOf("/>", referenceIndex);

                        string? referenceName = text.GetAttribute(text!.IndexOf("value=", referenceIndex));

                        if (referenceName is not (null or ""))
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
        public void Format()
        {
            string text = Resolved;
            if (ContentFormat is Model.ContentFormat.Unity)
            {
                text = text
                        .Trim()
                        .Replace("<style class=", "<style=")
                        .Replace("<color value=", "<color=")
                        .Replace("<size value=", "<size=")
                        .Replace("<material value=", "<material=")
                        .Replace("<quad value=", "<quad");

                if (Styles is not null)
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
