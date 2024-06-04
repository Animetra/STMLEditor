using STMLEditor.Model;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using STML.Model;

namespace STMLEditor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private STMLProject _currentProject = new();
        public STMLProject CurrentProject
        {
            get => _currentProject;
            set { _currentProject = value; OnPropertyChanged(); }
        }

        public STMLProject LoadProject(STMLProject projectToLoad = null)
        {
            CurrentProject = projectToLoad ?? new STMLProject();
            return CurrentProject;
        }

        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

}
