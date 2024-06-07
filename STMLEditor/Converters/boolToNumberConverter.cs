using STMLEditor.Model;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace STMLEditor.Converters
{
    [ValueConversion(typeof(bool), typeof(float))]
    internal class BoolToNumberConverter : IValueConverter
    {
        public float TrueValue { get; set; }
        public float FalseValue { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (float)value == TrueValue;
        }
    }
}