using Microsoft.WindowsAPICodePack.Dialogs;
using STMLEditor.Model;
using System.Windows;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace STMLEditor
{
    /// <summary>
    /// Interaktionslogik für SaveAs.xaml
    /// </summary>
    public partial class SaveAsWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public string ProjectTitle { get; set; } = "New Project";

        private string _path = "C://Users";

        public string Path
        {
            get => _path;
            set { _path = value; OnPropertyChanged(); }
        }

        public SaveAsWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C://Users";
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            dialog.Title = "Select your Unity Project's Assets Folder";

            if (dialog.ShowDialog() is CommonFileDialogResult.Ok)
            {
                Path = dialog.FileName;
            }
            
            Activate();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            //FileHandling.Save(System.IO.Path.Combine(Path, "AnimetraScreenText", ProjectTitle));
            Close();
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
