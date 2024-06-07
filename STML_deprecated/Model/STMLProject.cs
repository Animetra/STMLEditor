using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Net.Http.Headers;

namespace STML.Model
{
    public class STMLProject : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public ObservableCollection<STMLLibrary> Libraries { get; set; } = new();

        public STMLProject()
        {
            Libraries.CollectionChanged += CollectionChanged;
        }

        public STMLLibrary AddLibrary()
        {
            STMLLibrary newLibrary = new STMLLibrary();
            Libraries.Add(newLibrary);
            return newLibrary;
        }
    }

}