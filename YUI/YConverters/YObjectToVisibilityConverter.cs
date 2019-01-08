using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace YUI.WPF.YConverters
{
    /// <summary>
    /// 对象转换为是否显示
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class YObjectToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var p = parameter?.ToString() ?? "";

            if (value == null)
                if (p == "H")
                    return Visibility.Hidden;
                else
                    return Visibility.Collapsed;

            return Visibility.Visible;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
