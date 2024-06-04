using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace STML.Model
{
    public abstract class STMLElement : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public virtual STMLHeader Header { get; set; }
        public STMLElement Parent { get; set; }
        public ObservableCollection<STMLElement> Children { get; set; } = new();

        public STMLElement(STMLElement parent)
        {
            Header = new STMLHeader("New STML Element");
            Parent = parent;

            Children.CollectionChanged += CollectionChanged;
        }

        public virtual STMLElement AddChild() { return null; }
    }
}
