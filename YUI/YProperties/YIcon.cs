using System.Windows;
using System.Windows.Media;

namespace YUI.YProperties
{
    /// <summary>
    /// 附加属性类
    /// </summary>
    public static partial class YAttachProperty
    {
        #region YIconFontFamilyProperty 字体图标字体
        /// <summary>
        /// 字体图标字体附加属性
        /// </summary>
        public static readonly DependencyProperty YIconFontFamilyProperty = DependencyProperty.RegisterAttached(
            "YIconFontFamily", typeof(FontFamily), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        
        /// <summary>
        /// 获取字体图标字体
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static FontFamily GetYIconFontFamily(DependencyObject d)
        {
            return (FontFamily)d.GetValue(YIconFontFamilyProperty);
        }

        /// <summary>
        /// 设置字体图标字体
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconFontFamily(DependencyObject obj, FontFamily value)
        {
            obj.SetValue(YIconFontFamilyProperty, value);
        }
        #endregion

        #region YIconProperty 字体图标
        /// <summary>
        /// 字体图标附加属性
        /// </summary>
        public static readonly DependencyProperty YIconProperty = DependencyProperty.RegisterAttached(
            "YIcon", typeof(string), typeof(YAttachProperty), new FrameworkPropertyMetadata(""));
        /// <summary>
        /// 获取字体图标
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetYIcon(DependencyObject d)
        {
            return (string)d.GetValue(YIconProperty);
        }
        /// <summary>
        /// 设置字体图标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIcon(DependencyObject obj, string value)
        {
            obj.SetValue(YIconProperty, value);
        }
        #endregion

        #region YIconCheckedProperty 字体图标
        /// <summary>
        /// 字体图标附加属性
        /// </summary>
        public static readonly DependencyProperty YIconCheckedProperty = DependencyProperty.RegisterAttached(
            "YIconChecked", typeof(string), typeof(YAttachProperty), new FrameworkPropertyMetadata(""));
        /// <summary>
        /// 获取字体图标
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetYIconChecked(DependencyObject d)
        {
            return (string)d.GetValue(YIconCheckedProperty);
        }
        /// <summary>
        /// 设置字体图标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconChecked(DependencyObject obj, string value)
        {
            obj.SetValue(YIconCheckedProperty, value);
        }
        #endregion

        #region YIconCheckedNullProperty 字体图标
        /// <summary>
        /// 字体图标附加属性
        /// </summary>
        public static readonly DependencyProperty YIconCheckedNullProperty = DependencyProperty.RegisterAttached(
            "YIconCheckedNull", typeof(string), typeof(YAttachProperty), new FrameworkPropertyMetadata(""));
        /// <summary>
        /// 获取字体图标
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetYIconCheckedNull(DependencyObject d)
        {
            return (string)d.GetValue(YIconCheckedNullProperty);
        }
        /// <summary>
        /// 设置字体图标
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconCheckedNull(DependencyObject obj, string value)
        {
            obj.SetValue(YIconCheckedNullProperty, value);
        }
        #endregion

        #region YIconSizeProperty 字体图标大小
        /// <summary>
        /// 字体图标大小附加属性
        /// </summary>
        public static readonly DependencyProperty YIconSizeProperty = DependencyProperty.RegisterAttached(
            "YIconSize", typeof(double), typeof(YAttachProperty), new FrameworkPropertyMetadata(12D));
        /// <summary>
        /// 获取字体图标大小
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static double GetYIconSize(DependencyObject d)
        {
            return (double)d.GetValue(YIconSizeProperty);
        }
        /// <summary>
        /// 设置字体图标大小
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconSize(DependencyObject obj, double value)
        {
            obj.SetValue(YIconSizeProperty, value);
        }
        #endregion

        #region YIconMarginProperty 字体图标边距
        /// <summary>
        /// 字体图标边距附加属性
        /// </summary>
        public static readonly DependencyProperty YIconMarginProperty = DependencyProperty.RegisterAttached(
            "YIconMargin", typeof(Thickness), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 获取字体图标边距
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Thickness GetYIconMargin(DependencyObject d)
        {
            return (Thickness)d.GetValue(YIconMarginProperty);
        }
        /// <summary>
        /// 设置字体图标边距
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconMargin(DependencyObject obj, Thickness value)
        {
            obj.SetValue(YIconMarginProperty, value);
        }
        #endregion

        #region YIconForegroundProperty YIcon颜色

        /// <summary>
        /// YIcon颜色
        /// </summary>
        public static readonly DependencyProperty YIconForegroundProperty = DependencyProperty.RegisterAttached(
            "YIconForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.White));
        /// <summary>
        /// 获取YIcon颜色
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYIconForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(YIconForegroundProperty);
        }
        /// <summary>
        /// 设置YIcon颜色
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YIconForegroundProperty, value);
        }

        #endregion

        #region YIconMouseOverForegroundProperty YIcon鼠标悬停颜色

        /// <summary>
        /// YIcon鼠标悬停颜色
        /// </summary>
        public static readonly DependencyProperty YIconMouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "YIconMouseOverForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.White));
        /// <summary>
        /// 获取YIcon鼠标悬停颜色
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYIconMouseOverForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(YIconMouseOverForegroundProperty);
        }
        /// <summary>
        /// 设置YIcon鼠标悬停颜色
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconMouseOverForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YIconMouseOverForegroundProperty, value);
        }

        #endregion

        #region YIconPressedForegroundProperty YIcon鼠标按下颜色

        /// <summary>
        /// YIcon鼠标按下颜色
        /// </summary>
        public static readonly DependencyProperty YIconPressedForegroundProperty = DependencyProperty.RegisterAttached(
            "YIconPressedForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.White));
        /// <summary>
        /// 获取YIcon鼠标按下颜色
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYIconPressedForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(YIconPressedForegroundProperty);
        }
        /// <summary>
        /// 设置YIcon鼠标按下颜色
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconPressedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YIconPressedForegroundProperty, value);
        }

        #endregion

        #region YIconCheckedForegroundProperty YIcon选中颜色

        /// <summary>
        /// YIcon选中颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YIconCheckedForegroundProperty = DependencyProperty.RegisterAttached(
            "YIconCheckedForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.White));
        /// <summary>
        /// 获取YIcon选中颜色
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYIconCheckedForeground(DependencyObject d)
        {
            return (Brush)d.GetValue(YIconCheckedForegroundProperty);
        }
        /// <summary>
        /// 设置YIcon选中颜色
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYIconCheckedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YIconCheckedForegroundProperty, value);
        }

        #endregion
    }
}
