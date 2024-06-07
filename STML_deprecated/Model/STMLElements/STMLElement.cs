using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace STML.Model
{
    public abstract class STMLElement :
        INotifyCollectionChanged,
        INotifyPropertyChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;
        public event PropertyChangedEventHandler? PropertyChanged;

        public virtual STMLHeader Header { get; set; }
        public STMLElement Parent { get; set; }
        public STMLLibrary ParentLibrary => GetAncestor<STMLLibrary>() ?? throw new InvalidOperationException("object has no parent library");
        public STMLDocument ParentDocument => GetAncestor<STMLDocument>() ?? throw new InvalidOperationException("object has no parent document");
        public STMLSection ParentSection => GetAncestor<STMLSection>() ?? throw new InvalidOperationException("object has no parent section");
        public ObservableCollection<STMLElement> Children { get; set; } = new();

        public STMLElement(STMLElement parent)
        {
            Header = new STMLHeader("New STML Element");
            Parent = parent;

            Children.CollectionChanged += CollectionChanged;
        }

        public abstract STMLElement? AddChild();

        public STMLElement? GetChild(string id)
        {
            return Children.FirstOrDefault(x => x?.Header.ID == id, null);
        }

        public STMLElement? GetDescendant(string id)
        {
            STMLElement? result = null;

            foreach (var child in Children)
            {
                result = child.Header.ID == id ? child : child.GetDescendant(id);
            }   
            
            return result;
        }

        public T? GetAncestor<T>() where T : STMLElement
        {
            STMLElement ancestor = this;
            while (ancestor is not (T or null))
            {
                ancestor = ancestor.Parent;
            }

            return (T?)ancestor;
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void AddCollectionHandler<T>(ObservableCollection<T> collection)
        {
            collection.CollectionChanged += CollectionChanged;
        }
    }
}
