using STML.Model;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

/*namespace STMLEditor.Converters
{
    [ValueConversion(typeof(STMLExpression), typeof(FlowDocument))]
    internal class TextToRichTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((STMLExpression)value).FormattedContent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return formatter.FormatToSTML((FlowDocument)value);
        }
    }
}*/