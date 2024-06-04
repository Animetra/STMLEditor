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
    public class ExpressionControl : ContentControl
    {
        public STMLExpression STMLExpression { get; set; }
        public bool IsEditorView { get; set; } // needs to be implemented in UI

        public static readonly DependencyProperty STMLExpressionProperty =
        DependencyProperty.Register("STMLExpression", typeof(STMLExpression),
        typeof(ExpressionControl), new PropertyMetadata(STMLExpressionChanged));

        public static readonly DependencyProperty IsEditorViewProperty =
DependencyProperty.Register("IsEditorView", typeof(bool),
typeof(ExpressionControl), new PropertyMetadata(IsEditorViewChanged));

        private static void STMLExpressionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ExpressionControl)d).STMLExpression = e.NewValue as STMLExpression;
        }
        private static void IsEditorViewChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((ExpressionControl)d).IsEditorView = (bool)e.NewValue;
        }
    }
}
