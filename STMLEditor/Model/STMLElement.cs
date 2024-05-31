using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace STMLEditor.Model
{
    public abstract class STMLElement : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public BasicHeader Header { get; set; }
        public ObservableCollection<STMLElement> Children { get; set; } = new();

        public STMLElement()
        {
            Header = new BasicHeader("New STML Element");
            Children.CollectionChanged += CollectionChanged;
        }

        public virtual void AddChild(out STMLElement child) { child = null; }
        //public abstract void AddChild(STMLElement element);
    }
}
