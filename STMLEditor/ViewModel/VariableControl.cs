using STMLEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using STML.Model;

namespace STMLEditor.ViewModel
{
    public class VariableControl : ContentControl
    {
        public STMLVariable Variable { get; set; }

        public static readonly DependencyProperty VariableProperty =
        DependencyProperty.Register("Variable", typeof(STMLVariable),
        typeof(VariableControl), new PropertyMetadata(VariableChanged));

        private static void VariableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((VariableControl)d).Variable = e.NewValue as STMLVariable;
        }
    }
}
