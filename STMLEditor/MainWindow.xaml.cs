using STMLEditor.Model;
using STMLEditor.ViewModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using STML.Model;
using System.Diagnostics.Tracing;
using System.ComponentModel.Design;

namespace STMLEditor
{
    public partial class MainWindow:
        Window,
        INotifyPropertyChanged,
        INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private App ThisApp => (App)Application.Current;

        private STMLProject _activeProject;
        public STMLProject ActiveProject
        {
            get => _activeProject;
            set { _activeProject = value; OnPropertyChanged(); }
        }

        private STMLElement _selectedElement;
        public STMLElement SelectedElement
        {
            get => _selectedElement;
            set { _selectedElement = value; OnPropertyChanged(); }
        }

        private ObservableCollection<object> _objectsInEditor;
        public ObservableCollection<object> ObjectsInEditor
        {
            get => _objectsInEditor;
            set { _objectsInEditor = value; OnPropertyChanged(); }
        }

        public bool IsEditorView { get; set; } = true;

        public ICommand AddLibrary { get; set; } 
        public ICommand AddDictionary { get; set; }
        public ICommand AddScript { get; set; }
        public ICommand AddChildElement { get; set; } 

        public MainWindow()
        {
            AddLibrary = new Command(ExecuteAddLibrary);
            AddDictionary = new Command(ExecuteAddDictionary);
            AddScript = new Command(ExecuteAddScript);
            AddChildElement = new Command(ExecuteAddChild);

            DataContext = this;
            ObjectsInEditor = new();

            ObjectsInEditor.CollectionChanged += CollectionChanged;
            InitializeComponent();

            New(null, null);
        }

        private void New(object sender, RoutedEventArgs e)
        {
            ActiveProject = ThisApp.LoadProject();
            ActiveProject.Libraries.Add(new STMLLibrary());
            RefreshTextEditor();
            // TODO: Doesn't work yet
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            ActiveProject = FileHandling.Open();
            RefreshTextEditor();
        }
        
        private void Save(object sender, RoutedEventArgs e) => FileHandling.Save();
        
        private void SaveAs(object sender, RoutedEventArgs e) => FileHandling.SaveAs();
        

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (FileHandling.Status is SaveStatus.UnsavedChanges)
            {
                // TODO: Dialog, ask if user really wants to shut down
            }

            Application.Current.Shutdown();
        }

        private void ExecuteAddLibrary()
        {
            ActiveProject.AddLibrary();
            RefreshTextEditor();
        }

        private void ExecuteAddDictionary()
        {
            ((STMLDocument)ProjectTreeView.SelectedItem).AddDictionary();
            RefreshTextEditor();
        }

        private void ExecuteAddScript()
        {
            ((STMLDocument)ProjectTreeView.SelectedItem).AddScript();
            RefreshTextEditor();
        }

        private void ExecuteAddChild()
        {
            ((STMLElement)ProjectTreeView.SelectedItem).AddChild();
            RefreshTextEditor();
        }

        private void RemoveElement(object sender, RoutedEventArgs e)
        {
            STMLElement toDestroy = (STMLElement)ProjectTreeView.SelectedItem;
            toDestroy.Parent.Children.Remove(toDestroy);

            RefreshTextEditor();
        }

        private void OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedElement = (STMLElement)ProjectTreeView.SelectedItem;
            RefreshTextEditor();
        }

        private void RefreshTextEditor()
        {
            if (SelectedElement is STMLLibrary library)
            {
                ObjectsInEditor = new ObservableCollection<object>(library.Variables);
            }
            else if (SelectedElement is STMLSection section)
            {
                ObjectsInEditor = new ObservableCollection<object>(section.Children.Select(x => (STMLText)x));
            }
            else if (SelectedElement is STMLText text)
            {
                ObjectsInEditor = [text];
            }
        }

        private void InsertTag(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            IInputElement focusedControl = Keyboard.FocusedElement;

            Debug.Write("Type: " + focusedControl.GetType().ToString());

            if (focusedControl is TextBox textbox)
            {
                textbox.SelectedText = $"<{tag}/>";
            }
        }

        private void NestInTag(object sender, RoutedEventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();
            string endTag = tag;
            int endOfTagName = tag.IndexOf(" ");
            if (tag.IndexOf(" ") is not -1)
            {
                endTag = tag.Substring(0, endOfTagName);
            }

            IInputElement focusedControl = Keyboard.FocusedElement;

            Debug.Write("Type: " + focusedControl.GetType().ToString());

            if (focusedControl is TextBox textbox)
            {
                string text = textbox.SelectedText;
                textbox.SelectedText = $"<{tag}>{text}</{endTag}>";
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}