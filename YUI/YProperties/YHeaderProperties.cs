using System.Windows;
using System.Windows.Media;

namespace YUI.WPF.YProperties
{
    public static partial class YAttachProperty
    {
        #region YHeaderBackgroundProperty 背景色
        /// <summary>
        /// 背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YHeaderBackgroundProperty = DependencyProperty.RegisterAttached(
            "YHeaderBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYHeaderBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YHeaderBackgroundProperty, value);
        }

        /// <summary>
        /// 获取景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYHeaderBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YHeaderBackgroundProperty);
        }

        #endregion

        #region YHeaderForegroundProperty 前景色
        /// <summary>
        /// 背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YHeaderForegroundProperty = DependencyProperty.RegisterAttached(
            "YHeaderForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYHeaderForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YHeaderForegroundProperty, value);
        }

        /// <summary>
        /// 获取景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYHeaderForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YHeaderForegroundProperty);
        }

        #endregion

        #region YHasHeaderPropert 是否含有Header
        /// <summary>
        /// 背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YHasHeaderPropert = DependencyProperty.RegisterAttached(
            "YHasHeader", typeof(bool), typeof(YAttachProperty), new FrameworkPropertyMetadata(true));

        /// <summary>
        /// 设置背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYHasHeader(DependencyObject element, bool value)
        {
            element.SetValue(YHasHeaderPropert, value);
        }

        /// <summary>
        /// 获取景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static bool GetYHasHeader(DependencyObject element)
        {
            return (bool)element.GetValue(YHasHeaderPropert);
        }

        #endregion
    }
}
