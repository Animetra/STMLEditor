using STML.Model;
using System.ComponentModel;
using System.Diagnostics;

namespace STML.Model
{
    public class STMLExpression : 
        STMLElement,
        IUsableInEditor
    {
        public bool UseNarratorStyle { get; set; } = true;
        public bool UseStyle { get; set; } = true;

        public STMLFormattableString Content { get; set; } = new();
        public STMLFormattableString Narrator { get; set; } = new();

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
            styles.Add(Style);
            styles.Add(Narrator.Resolved);
            return styles.ToArray();
        }
    }
}
