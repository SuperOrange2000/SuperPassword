using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SuperPassword.Common.Converter
{
    class SolidColorBrushToColorConvertrer : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            return ((SolidColorBrush)value).Color.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
