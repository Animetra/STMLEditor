using STMLEditor.Model;
using System.Globalization;
using System.Windows.Data;

namespace STMLEditor.Converters
{
    internal class LanguageCodeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string languageCode = (string)value;
            return CultureInfo.GetCultures(CultureTypes.NeutralCultures).First(x => x.TwoLetterISOLanguageName == languageCode);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string languageInEnglish = (string)value;
            return CultureInfo.GetCultures(CultureTypes.NeutralCultures).First(x => x.EnglishName == languageInEnglish);
        }
    }
}