using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STML.Model
{
    public class STMLLibrary : STMLElement
    {
        public static ObservableCollection<string> AllLanguages { get; set; } = new ObservableCollection<string>(CultureInfo.GetCultures(CultureTypes.NeutralCultures).Select(x => x.EnglishName));
        public string Language { get; set; } = "en";

        public ObservableCollection<STMLVariable> Variables { get; set; } = new();

        public STMLLibrary() : base(null)
        {
            Header = new STMLHeader("New Library");
        }

        public override STMLElement AddChild()
        {
            STMLDocument child = new STMLDocument(this);
            Children.Add(child);

            return child;
        }
    }
}
