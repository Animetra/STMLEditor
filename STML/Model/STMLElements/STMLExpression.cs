using System.Collections.Generic;
using System;

namespace STML.Model
{
    public class STMLExpression : 
        STMLElement,
        IUsableInEditor
    {
        public bool UseNarratorStyle { get; set; } = true;
        public bool UseStyle { get; set; } = true;

        public STMLFormattableString Content { get; set; } = new STMLFormattableString();
        public STMLFormattableString Narrator { get; set; } = new STMLFormattableString();

        private string _style;
        public string Style
        {
            get => _style;
            set { _style = value; OnPropertyChanged(); }
        }

        public STMLExpression(STMLSection parent) : base(parent)
        {

            Header = new STMLHeader("New Expression");
            Content = new STMLFormattableString("", this, ContentFormat.Unity);
            Narrator = new STMLFormattableString("", this, ContentFormat.Unity);
            Style = "";
        }

        public override STMLElement? AddChild()
        {
            throw new InvalidOperationException("Expression can't have children");
        }

        public string[] GetActiveStyles()
        {
            List<string> styles = new List<string>();
            if (Style != "") { styles.Add(Style); }

            if (Narrator != "") { styles.Add(Narrator.Resolved); }

            return styles.ToArray();
        }

        public void Resolve()
        {
            Content.Resolve();
            Narrator.Resolve();
        }

        public void Format(ContentFormat format)
        {
            Content.Format(format);
            Narrator.Format(format);
        }

        public void ResolveAndFormat(ContentFormat format)
        {
            Resolve();
            Format(format);
        }
    }
}
