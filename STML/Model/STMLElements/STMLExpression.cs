using System;
using System.Collections.Generic;

namespace STML.Model
{
    public class STMLExpression :
        STMLElement
    {
        public bool UseNarratorStyle { get; set; } = true;
        public bool UseStyle { get; set; } = true;
        public STMLFormattableString ActiveLanguageContent => Content.GetContentOfActiveLanguage(ParentProject);
        public STMLFormattableString ActiveLanguageNarrator => Narrator.GetContentOfActiveLanguage(ParentProject);

        public Dictionary<string, STMLFormattableString> Content { get; set; } = new Dictionary<string, STMLFormattableString>();
        public Dictionary<string, STMLFormattableString> Narrator { get; set; } = new Dictionary<string, STMLFormattableString>();

        private string _style;
        public string Style
        {
            get => _style;
            set { _style = value; OnPropertyChanged(); }
        }

        public STMLExpression(STMLSection parent) : base(parent)
        {
            Header = new STMLHeader("New Expression");
            Content.AddEntryForEachLanguage(new STMLFormattableString("", this, ContentFormat.Unity), ParentProject);
            Narrator.AddEntryForEachLanguage(new STMLFormattableString("", this, ContentFormat.Unity), ParentProject);
            Style = "";
        }

        public override STMLElement? AddChild()
        {
            throw new InvalidOperationException("Expressions can't have children");
        }

        public string[] GetActiveStyles()
        {
            List<string> styles = new List<string>();
            if (Style != "") { styles.Add(Style); }
            string resolvedNarrator = Narrator.GetContentOfActiveLanguage(ParentProject).Resolved;
            if (resolvedNarrator != "") { styles.Add(resolvedNarrator); }

            return styles.ToArray();
        }
    }
}
