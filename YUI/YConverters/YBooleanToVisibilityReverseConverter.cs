using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YUI.YConverters
{
    /// <summary>
    /// bool转换成Visibility反向
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public sealed class YBooleanToVisibilityReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool && (bool)value)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
