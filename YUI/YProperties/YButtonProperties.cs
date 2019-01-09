using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YUI.WPF.YProperties
{
    public static partial class YAttachProperty
    {
        #region YButtonIconAlignmentProperty YButton的Icon位置

        /// <summary>
        /// YButton的Icon位置附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonIconAlignmentProperty = DependencyProperty.RegisterAttached(
            "YButtonIconAlignment", typeof(Alignment), typeof(YAttachProperty), new FrameworkPropertyMetadata(Alignment.Left));
        /// <summary>
        /// 获取YButton的Icon位置
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static Alignment GetYButtonIconAlignment(DependencyObject d)
        {
            return (Alignment)d.GetValue(YButtonIconAlignmentProperty);
        }
        /// <summary>
        /// 设置YButton的Icon位置
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYButtonIconAlignment(DependencyObject obj, Alignment value)
        {
            obj.SetValue(YButtonIconAlignmentProperty, value);
        }

        #endregion

        #region YButtonIconTypeProperty YButton的Icon类型

        /// <summary>
        /// YButton的Icon位置附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonIconTypeProperty = DependencyProperty.RegisterAttached(
            "YButtonIconType", typeof(IconType), typeof(YAttachProperty), new FrameworkPropertyMetadata(IconType.Font));
        /// <summary>
        /// 获取YButton的Icon类型
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static IconType GetYButtonIconType(DependencyObject d)
        {
            return (IconType)d.GetValue(YButtonIconTypeProperty);
        }
        /// <summary>
        /// 设置YButton的Icon类型
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYButtonIconType(DependencyObject obj, IconType value)
        {
            obj.SetValue(YButtonIconTypeProperty, value);
        }

        #endregion

        #region YButtonBackgroundProperty 背景色
        /// <summary>
        /// 背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonBackgroundProperty = DependencyProperty.RegisterAttached(
            "YButtonBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYButtonBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YButtonBackgroundProperty, value);
        }

        /// <summary>
        /// 获取景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYButtonBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YButtonBackgroundProperty);
        }

        #endregion

        #region YButtonForegroundProperty 前景色
        /// <summary>
        /// 背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonForegroundProperty = DependencyProperty.RegisterAttached(
            "YButtonForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYButtonForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YButtonForegroundProperty, value);
        }

        /// <summary>
        /// 获取景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYButtonForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YButtonForegroundProperty);
        }

        #endregion

        #region YButtonMouseOverForegroundProperty 鼠标悬浮前景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonMouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "YButtonMouseOverForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置鼠标悬浮前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYButtonMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YButtonMouseOverForegroundProperty, value);
        }
        /// <summary>
        /// 获取鼠标悬浮前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYButtonMouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YButtonMouseOverForegroundProperty);
        }

        #endregion

        #region YButtonMouseOverBackgroundProperty 鼠标悬浮背景色
        /// <summary>
        /// 鼠标悬浮背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonMouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "YButtonMouseOverBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标悬浮背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYButtonMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YButtonMouseOverBackgroundProperty, value);
        }

        /// <summary>
        /// 获取鼠标悬浮背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYButtonMouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YButtonMouseOverBackgroundProperty);
        }

        #endregion

        #region YButtonPressedForegroundProperty 鼠标按下前景色
        /// <summary>
        /// 鼠标按下前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonPressedForegroundProperty =
            DependencyProperty.RegisterAttached("YButtonPressedForeground", typeof(Brush), typeof(YAttachProperty),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标按下前景色
        /// </summary>
        public static void SetYButtonPressedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YButtonPressedForegroundProperty, value);
        }

        /// <summary>
        /// 获取鼠标按下前景色
        /// </summary>
        public static Brush GetYButtonPressedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(YButtonPressedForegroundProperty);
        }

        #endregion

        #region YButtonPressedBackgroundProperty 鼠标按下背景色
        /// <summary>
        /// 鼠标按下背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YButtonPressedBackgroundProperty =
            DependencyProperty.RegisterAttached("YButtonPressedBackground", typeof(Brush), typeof(YAttachProperty),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标按下背景色
        /// </summary>
        public static void SetYButtonPressedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YButtonPressedBackgroundProperty, value);
        }

        /// <summary>
        /// 获取鼠标按下背景色
        /// </summary>
        public static Brush GetYButtonPressedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(YButtonPressedBackgroundProperty);
        }

        #endregion
    }
}
