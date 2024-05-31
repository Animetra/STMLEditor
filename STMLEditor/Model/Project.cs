using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace STMLEditor.Model
{
    class Project : INotifyCollectionChanged
    {
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        public static Project CurrentProject { get; set; } = new();
        public Settings Settings { get; set; } = new();
        public BasicHeader Header { get; set; } = new();
        public ObservableCollection<STMLElement> Hierarchy { get; set; } = new();

        public Project()
        {
            Header.Name = "New Project";
            Hierarchy.CollectionChanged += CollectionChanged;
        }

        public void AddLibrary()
        {
            Hierarchy.Add(new STMLLibrary());
        }
    }
}