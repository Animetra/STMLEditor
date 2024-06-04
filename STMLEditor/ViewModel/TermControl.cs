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
    public class TermControl : ContentControl
    {
        public STMLTerm STMLTerm { get; set; }

        public static readonly DependencyProperty STMLTermProperty =
        DependencyProperty.Register("STMLTerm", typeof(STMLTerm),
        typeof(TermControl), new PropertyMetadata(STMLTermChanged));

        private static void STMLTermChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((TermControl)d).STMLTerm = e.NewValue as STMLTerm;
        }
    }
}
