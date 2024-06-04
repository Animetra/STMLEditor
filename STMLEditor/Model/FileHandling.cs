using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Media.Converters;
using System.IO.Enumeration;
using Microsoft.Win32;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Linq.Expressions;
using STML.Model;

namespace STMLEditor.Model
{
    public static class FileHandling
    {
        private static STMLProject Project => ((App)Application.Current).CurrentProject;
        public static SaveStatus Status { get; set; } = SaveStatus.Unknown;
        public static string EditedProjectDirectory { get; set; } = "";

        public static void Save()
        {
            if (EditedProjectDirectory == "")
            {
                SaveAs();
            }
            else
            {
                ActuallySave();
            }
        }

        public static void SaveAs()
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C://Users";
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            dialog.Title = "Select your Unity Project's Assets Folder";

            if (dialog.ShowDialog() is CommonFileDialogResult.Ok)
            {
                EditedProjectDirectory = Path.Combine(dialog.FileName, Filenames.ModuleName);
                ActuallySave();
            }
        }

        public static void ActuallySave()
        {
            STMLWriter writer = new STMLWriter();
            writer.WriteProject(Project, EditedProjectDirectory);
            Status = SaveStatus.Saved;
        }

        public static STMLProject Open()
        {
            var dialog = new OpenFileDialog();
            dialog.FileName = "Animetra ScreenText File"; // Default file name
            dialog.DefaultExt = ".xml"; // Default file extension
            dialog.Filter = "ScreenText Project Files (.xml)|*.xml"; // Filter files by extension

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                EditedProjectDirectory = Path.GetDirectoryName(dialog.FileName);
                STMLReader reader = new STMLReader();
                STMLProject newProject = reader.ReadProject(EditedProjectDirectory);

                Status = SaveStatus.Unknown;
                return ((App)Application.Current).LoadProject(newProject);
            }

            return ((App)Application.Current).CurrentProject;
        }
    }

    public enum SaveStatus
    {
        Unknown,
        UnsavedChanges,
        Saved
    }
}
