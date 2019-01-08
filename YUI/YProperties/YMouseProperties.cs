using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YUI.WPF.YProperties
{
    public static partial class YAttachProperty
    {
        #region YMouseOverForegroundProperty 鼠标悬浮前景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YMouseOverForegroundProperty = DependencyProperty.RegisterAttached(
            "YMouseOverForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置鼠标悬浮前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYMouseOverForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YMouseOverForegroundProperty, value);
        }
        /// <summary>
        /// 获取鼠标悬浮前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYMouseOverForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YMouseOverForegroundProperty);
        }

        #endregion

        #region YMouseOverBackgroundProperty 鼠标悬浮背景色
        /// <summary>
        /// 鼠标悬浮背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YMouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "YMouseOverBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标悬浮背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYMouseOverBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YMouseOverBackgroundProperty, value);
        }

        /// <summary>
        /// 获取鼠标悬浮背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYMouseOverBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YMouseOverBackgroundProperty);
        }

        #endregion

        #region YMouseOverBorderBrushProperty 鼠标进入边框色，输入控件
        /// <summary>
        /// 鼠标进入边框色，输入控件附加属性
        /// </summary>
        public static readonly DependencyProperty YMouseOverBorderBrushProperty =
            DependencyProperty.RegisterAttached("YMouseOverBorderBrush", typeof(Brush), typeof(YAttachProperty),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标进入边框色，输入控件
        /// </summary>
        public static void SetYMouseOverBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(YMouseOverBorderBrushProperty, value);
        }

        /// <summary>
        /// 获取鼠标进入边框色，输入控件
        /// </summary>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        [AttachedPropertyBrowsableForType(typeof(CheckBox))]
        [AttachedPropertyBrowsableForType(typeof(RadioButton))]
        [AttachedPropertyBrowsableForType(typeof(DatePicker))]
        [AttachedPropertyBrowsableForType(typeof(ComboBox))]
        [AttachedPropertyBrowsableForType(typeof(RichTextBox))]
        [AttachedPropertyBrowsableForType(typeof(Button))]
        public static Brush GetYMouseOverBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(YMouseOverBorderBrushProperty);
        }

        #endregion

        #region YPressedForegroundProperty 鼠标按下前景色
        /// <summary>
        /// 鼠标按下前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YPressedForegroundProperty =
            DependencyProperty.RegisterAttached("YPressedForeground", typeof(Brush), typeof(YAttachProperty),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标按下前景色
        /// </summary>
        public static void SetYPressedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YPressedForegroundProperty, value);
        }

        /// <summary>
        /// 获取鼠标按下前景色
        /// </summary>
        public static Brush GetYPressedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(YPressedForegroundProperty);
        }

        #endregion

        #region YPressedBackgroundProperty 鼠标按下背景色
        /// <summary>
        /// 鼠标按下背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YPressedBackgroundProperty =
            DependencyProperty.RegisterAttached("YPressedBackground", typeof(Brush), typeof(YAttachProperty),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标按下背景色
        /// </summary>
        public static void SetYPressedBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YPressedBackgroundProperty, value);
        }

        /// <summary>
        /// 获取鼠标按下背景色
        /// </summary>
        public static Brush GetYPressedBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(YPressedBackgroundProperty);
        }

        #endregion

        #region YPressedBorderBrushProperty 鼠标按下边框颜色
        /// <summary>
        /// 鼠标按下前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YPressedBorderBrushProperty =
            DependencyProperty.RegisterAttached("YPressedBorderBrush", typeof(Brush), typeof(YAttachProperty),
                new FrameworkPropertyMetadata(Brushes.Transparent,
                    FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置鼠标按下前景色
        /// </summary>
        public static void SetYPressedBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(YPressedBorderBrushProperty, value);
        }

        /// <summary>
        /// 获取鼠标按下前景色
        /// </summary>
        public static Brush GetYPressedBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(YPressedBorderBrushProperty);
        }

        #endregion
    }
}
