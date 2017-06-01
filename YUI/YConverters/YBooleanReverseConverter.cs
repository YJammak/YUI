using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace YUI.YConverters
{
    /// <summary>
    /// bool对调转换
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public sealed class YBooleanReverseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;
            return !v;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
