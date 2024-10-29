/*using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace STML.Model
{
    public class STMLLibrary : STMLElement
    {
        // TODO: definitely not the right location for that:
        public static ObservableCollection<string> AllLanguages { get; set; } = new ObservableCollection<string>(CultureInfo.GetCultures(CultureTypes.NeutralCultures).Select(x => x.EnglishName));
        public ObservableCollection<string> LibraryLanguages

        public string Language { get; set; } = "en"; // ISO-639-1

        public ObservableCollection<STMLTerm> References { get; set; } = new ObservableCollection<STMLTerm>();

        public STMLLibrary() : base(null)
        {
            Header = new STMLHeader("New Library");
            AddCollectionHandler(References);
        }

        public override STMLElement AddChild()
        {
            STMLDocument child = new STMLDocument(this);
            Children.Add(child);

            return child;
        }

        public string AddReference(string name, STMLTerm reference)
        {
            string uniqueName = name;
            if (name != "" && name != null)
            {
                int i = 0;

                while (References.Any(x => x.ReferenceName == name))
                {
                    i++;
                    uniqueName = $"{name}_{i}";
                }

                References.Add(reference);
            }

            return uniqueName;
        }
    }
}*/