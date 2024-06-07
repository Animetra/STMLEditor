using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System;
using System.Linq;
using System.Collections.Generic;

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
        public ObservableCollection<STMLElement> Children { get; set; } = new ObservableCollection<STMLElement>();

        public STMLElement(STMLElement parent)
        {
            Header = new STMLHeader("New STML Element");
            Parent = parent;

            Children.CollectionChanged += CollectionChanged;
        }

        public abstract STMLElement? AddChild();

        public STMLElement? GetChild(string id)
        {
            return Children.FirstOrDefault(x => x?.Header.ID == id);
        }

        public STMLElement? GetDescendant(Predicate<STMLElement> condition)
        {
            STMLElement? result = null;

            foreach (var child in Children)
            {
                if (condition(child))
                {
                    return child;
                }
                else
                {
                    result = child.GetDescendant(condition);
                    if (result != null)
                    {
                        return result;
                    }
                }
            }   
            
            return result;
        }

        public STMLElement[] GetDescendants(Predicate<STMLElement> condition)
        {
            List<STMLElement> results = new List<STMLElement>();

            foreach (var child in Children)
            {
                if (condition(child))
                {
                    results.Add(child);
                }

                results.AddRange(child.GetDescendants(condition));
            }

            return results.ToArray();
        }

        public void ForEachDescendantWhere(Action<STMLElement> action, Predicate<STMLElement> condition)
        {
            foreach (var child in Children)
            {
                if (condition(child))
                {
                    action(child);
                }

                child.ForEachDescendantWhere(action, condition);
            }
        }

        public T? GetAncestor<T>() where T : STMLElement
        {
            STMLElement ancestor = this;
            while (!(ancestor is T || ancestor is null))
            {
                ancestor = ancestor.Parent;
            }

            return (T?)ancestor;
        }


        public void ResolveContent()
        {
            ForEachDescendantWhere(x => ((STMLExpression)x).Resolve(), x => x is STMLExpression);
        }

        public void FormatContent(ContentFormat format)
        {
            ForEachDescendantWhere(x => ((STMLExpression)x).Format(format), x => x is STMLExpression);
        }

        public void ResolveAndFormatContent(ContentFormat format)
        {
            ForEachDescendantWhere(x => ((STMLExpression)x).ResolveAndFormat(format), x => x is STMLExpression);
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
