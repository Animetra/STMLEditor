using STMLEditor.Model;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace STMLEditor
{
    public partial class MainWindow:
        Window,
        INotifyPropertyChanged,
        INotifyCollectionChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public event NotifyCollectionChangedEventHandler? CollectionChanged;

        private Project Project => Project.CurrentProject;
        public ObservableCollection<STMLElement> Hierarchy => Project.CurrentProject.Hierarchy;

        private STMLElement _selectedElement;
        public STMLElement SelectedElement
        {
            get => _selectedElement;
            set { _selectedElement = value; OnPropertyChanged(); }
        }

        private ObservableCollection<STMLText> _textsToEdit;
        public ObservableCollection<STMLText> TextsToEdit
        {
            get => _textsToEdit;
            set { _textsToEdit = value; OnPropertyChanged(); }
        }

        public MainWindow()
        {
            DataContext = this;
            TextsToEdit = new();
            TextsToEdit.CollectionChanged += CollectionChanged;
            InitializeComponent();
        }

        private void New(object sender, RoutedEventArgs e)
        {
            Project.CurrentProject = new();
        }

        private void Open(object sender, RoutedEventArgs e)
        {
            FileHandling.Open();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            FileHandling.Save();
        }

        private void SaveAs(object sender, RoutedEventArgs e)
        {
            FileHandling.SaveAs();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            if (FileHandling.Status is SaveStatus.UnsavedChanges)
            {
                //TODO: Dialog
            }

            Application.Current.Shutdown();
        }

        private void AddLibrary(object sender, RoutedEventArgs e)
        {
            Project.AddLibrary();
        }

        private void AddChild(object sender, RoutedEventArgs e)
        {
            if (ProjectTreeView.SelectedItem is not null)
            {
                ((STMLElement)ProjectTreeView.SelectedItem).AddChild(out STMLElement newChild);
                if (ProjectTreeView.SelectedItem is TreeViewItem treeViewItem) // TODO: Doesn't work
                {
                    treeViewItem.IsExpanded = true;
                }
            }

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
                TextsToEdit = new ObservableCollection<STMLText>(section.Children.Select(x => (STMLText)x));
            }
            else if (SelectedElement is STMLText text)
            {
                TextsToEdit = [text];
            }
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}