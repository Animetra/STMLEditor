using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace STML.Model
{
    public class STMLProject : STMLElement, INotifyCollectionChanged
    {
        public new event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ObservableCollection<string> ProjectLanguages { get; private set; } = new ObservableCollection<string>();
        public string StandardLanguage { get; private set; }
        public string ActiveLanguage { get; private set; }

        public ContentFormat ActiveFormat { get; private set; }
        public ObservableCollection<STMLTerm> References { get; set; } = new ObservableCollection<STMLTerm>();

        public STMLProject() : base(null)
        {
            ProjectLanguages.CollectionChanged += CollectionChanged;
            AddLanguage("en");
            SetStandardLanguage("en");
        }

        public void AddLanguage(string languageCode)
        {
            ProjectLanguages.Add(languageCode);
        }

        public void SetStandardLanguage(string languageCode)
        {
            if (ProjectLanguages.Contains(languageCode))
            {
                StandardLanguage = languageCode;
            }
            else
            {
                throw new InvalidOperationException($"Language is not set up as project language. Add {languageCode} to project first.");
            }
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

        public override STMLElement? AddChild()
        {
            STMLDocument child = new STMLDocument(this);
            Children.Add(child);
            return child;
        }
    }
}