using STMLEditor.Model;
using STMLEditor.ViewModel;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using STML.Model;


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

        private ObservableCollection<IUsableInEditor> _objectsInEditor;
        public ObservableCollection<IUsableInEditor> ObjectsInEditor
        {
            get => _objectsInEditor;
            set { _objectsInEditor = value; OnPropertyChanged(); }
        }

        private bool _isEditorView;
        public bool IsEditorView
        {
            get => _isEditorView;
            set {_isEditorView = value; OnPropertyChanged(); }
        }

        public ICommand NewProject { get; set; }
        public ICommand OpenProject { get; set; }
        public ICommand SaveProject { get; set; }
        public ICommand SaveAsProject { get; set; }
        public ICommand Quit { get; set; }

        public ICommand AddLibrary { get; set; } 
        public ICommand AddDictionary { get; set; }
        public ICommand AddScript { get; set; }
        public ICommand AddChildElement { get; set; } 

        public MainWindow()
        {

            NewProject = new Command(ExecuteNewProject);
            OpenProject = new Command(ExecuteOpenProject);
            SaveProject = new Command(ExecuteSaveProject);
            SaveAsProject = new Command(ExecuteSaveAsProject);
            Quit = new Command(ExecuteQuit);

            AddLibrary = new Command(ExecuteAddLibrary);
            AddDictionary = new Command(ExecuteAddDictionary);
            AddScript = new Command(ExecuteAddScript);
            AddChildElement = new Command(ExecuteAddChild);

            IsEditorView = true;

            DataContext = this;
            ObjectsInEditor = new();

            ObjectsInEditor.CollectionChanged += CollectionChanged;
            InitializeComponent();

            ExecuteNewProject();
        }

        private void ExecuteNewProject()
        {
            ActiveProject = ThisApp.LoadProject();
            ActiveProject.Documents.Add(new STMLLibrary());
            RefreshTextEditor();
        }

        private void ExecuteOpenProject()
        {
            ActiveProject = FileHandling.Open();
            RefreshTextEditor();
        }
        
        private void ExecuteSaveProject() => FileHandling.Save();

        private void ExecuteSaveAsProject() => FileHandling.SaveAs();
        
        private void ExecuteQuit()
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
            if (SelectedElement is STMLSection section)
            {
                ObjectsInEditor = new ObservableCollection<IUsableInEditor>(section.Children.Select(x => (IUsableInEditor)x));
            }
            else if (SelectedElement is IUsableInEditor text)
            {
                ObjectsInEditor = [text];
            }
        }

        private void InsertRef(object sender, RoutedEventArgs e)
        {
            STMLTerm? term = ((Button)sender).Tag as STMLTerm ?? throw new InvalidOperationException("referenced object is not a term");
            IInputElement focusedControl = Keyboard.FocusedElement;

            if (focusedControl is TextBox textbox)
            {
                textbox.SelectedText = textbox.SelectedText.NestInSTMLTags("ref", term.ReferenceName);
            }
        }

        private void NestInTag(object sender, RoutedEventArgs e)
        {
            string? toNestIn = ((Button)sender).Tag.ToString();

            if (toNestIn is not ("" or null))
            {
                string[] toNestInArray = toNestIn.Split(",");
                string tag = toNestInArray[0];
                string? value = toNestInArray.Count() > 1 ? toNestInArray[1] : null;

                IInputElement focusedControl = Keyboard.FocusedElement;

                if (focusedControl is TextBox textbox)
                {
                    textbox.SelectedText = value is not null ? textbox.SelectedText.NestInSTMLTags(tag, value) : textbox.SelectedText.NestInTags(tag);
                    if (value is not null)
                    {
                        textbox.Select(textbox.SelectionStart + tag.Length + 9, value.Length);

                    }
                }
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}