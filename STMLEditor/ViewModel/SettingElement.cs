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
    public class SettingElement : ContentControl
    {
        public object Label { get; set; }
        public object Value { get; set; }

        public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(object),
        typeof(SettingElement), new PropertyMetadata(LabelChanged));

        private static void LabelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SettingElement)d).Label = e.NewValue;
        }

        public static readonly DependencyProperty ValueProperty =
DependencyProperty.Register("Value", typeof(object),
typeof(SettingElement), new PropertyMetadata(ValueChanged));

        private static void ValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((SettingElement)d).Value = e.NewValue;
        }
    }
}
