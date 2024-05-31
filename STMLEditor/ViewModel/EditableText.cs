using STMLEditor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace STMLEditor.ViewModel
{
    public class EditableText : ContentControl
    {
        public STMLText STMLText { get; set; }

        public static readonly DependencyProperty STMLTextProperty =
        DependencyProperty.Register("STMLText", typeof(STMLText),
        typeof(EditableText), new PropertyMetadata(STMLTextChanged));

        private static void STMLTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((EditableText)d).STMLText = e.NewValue as STMLText;
        }
    }
}
