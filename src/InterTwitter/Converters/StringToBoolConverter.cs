using System;
using System.Globalization;
using Xamarin.Forms;

namespace InterTwitter.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string text = value as string;

            bool isValid = text != null ? text.Length >= 0 && text.Length <= 280 : true;

            return isValid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
