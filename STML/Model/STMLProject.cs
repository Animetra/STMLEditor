using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace STML.Model
{
    public class STMLProject : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ObservableCollection<string> ProjectLanguages { get; private set; } = new ObservableCollection<string>();
        public string StandardLanguage { get; private set; }
        public ObservableCollection<STMLDocument> Documents { get; set; } = new ObservableCollection<STMLDocument>();

        public STMLProject()
        {
            ProjectLanguages.CollectionChanged += CollectionChanged;
            Documents.CollectionChanged += CollectionChanged;
            AddLanguage("en");
            SetStandardLanguage("en");
        }

        public STMLDocument AddDocument()
        {
            STMLDocument newDocument = new STMLDocument();
            Documents.Add(newDocument);
            return newDocument;
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
    }
}