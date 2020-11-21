using System;
using System.Globalization;
using Xamarin.Forms;

namespace InterTwitter.Converters
{
    public class MultiTriggerButtonConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var a = !string.IsNullOrWhiteSpace((string)value);
            return a;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }
    }
}
