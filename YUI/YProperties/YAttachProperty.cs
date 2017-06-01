using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace YUI.YProperties
{
    public static partial class YAttachProperty
    {
        #region 附加属性

        #region YBackgroundProperty 背景色
        /// <summary>
        /// 背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YBackgroundProperty = DependencyProperty.RegisterAttached(
            "YBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YBackgroundProperty, value);
        }

        /// <summary>
        /// 获取景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YBackgroundProperty);
        }

        #endregion

        #region YDisenableBackground 背景色
        /// <summary>
        /// 背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YDisenableBackgroundProperty = DependencyProperty.RegisterAttached(
            "YDisenableBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置背景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYDisenableBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YDisenableBackgroundProperty, value);
        }

        /// <summary>
        /// 获取景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYDisenableBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YDisenableBackgroundProperty);
        }

        #endregion

        #region YFocusBackground 获得焦点背景颜色

        /// <summary>
        /// 获得焦点背景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YFocusBackgroundProperty = DependencyProperty.RegisterAttached(
            "YFocusBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 设置获得焦点背景颜色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYFocusBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YFocusBackgroundProperty, value);
        }
        /// <summary>
        /// 获取获得焦点背景颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYFocusBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YFocusBackgroundProperty);
        }

        #endregion

        #region YForegroundProperty 前景色
        /// <summary>
        /// 前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YForegroundProperty = DependencyProperty.RegisterAttached(
            "YForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));

        /// <summary>
        /// 设置前景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YForegroundProperty, value);
        }

        /// <summary>
        /// 获取前景色附加属性
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YForegroundProperty);
        }

        #endregion

        #region YFocusForeground 获得焦点前景颜色

        /// <summary>
        /// 获得焦点前景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YFocusForegroundProperty = DependencyProperty.RegisterAttached(
            "YFocusForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 设置获得焦点前景颜色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYFocusForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YFocusForegroundProperty, value);
        }
        /// <summary>
        /// 获取获得焦点前景颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYFocusForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YFocusForegroundProperty);
        }

        #endregion

        #region YDisenableForeground 获得焦点前景颜色

        /// <summary>
        /// 获得焦点前景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YDisenableForegroundProperty = DependencyProperty.RegisterAttached(
            "YDisenableForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置获得焦点前景颜色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYDisenableForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YDisenableForegroundProperty, value);
        }
        /// <summary>
        /// 获取获得焦点前景颜色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYDisenableForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YDisenableForegroundProperty);
        }

        #endregion

        #region YFocusBorderBrush 焦点边框色，输入控件
        /// <summary>
        /// 焦点边框色，输入控件附加属性
        /// </summary>
        public static readonly DependencyProperty YFocusBorderBrushProperty = DependencyProperty.RegisterAttached(
            "YFocusBorderBrush", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 设置焦点边框色，输入控件
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYFocusBorderBrush(DependencyObject element, Brush value)
        {
            element.SetValue(YFocusBorderBrushProperty, value);
        }
        /// <summary>
        /// 获取焦点边框色，输入控件
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYFocusBorderBrush(DependencyObject element)
        {
            return (Brush)element.GetValue(YFocusBorderBrushProperty);
        }

        #endregion

        #region YAttachContentProperty 附加组件模板
        /// <summary>
        /// 附加组件模板附加属性
        /// </summary>
        public static readonly DependencyProperty YAttachContentProperty = DependencyProperty.RegisterAttached(
            "YAttachContent", typeof(ControlTemplate), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 获取附加组件模板
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static ControlTemplate GetYAttachContent(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(YAttachContentProperty);
        }
        /// <summary>
        /// 设置附加组件模板
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYAttachContent(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(YAttachContentProperty, value);
        }
        #endregion

        #region YWatermarkProperty 水印
        /// <summary>
        /// 水印附加属性
        /// </summary>
        public static readonly DependencyProperty YWatermarkProperty = DependencyProperty.RegisterAttached(
            "YWatermark", typeof(string), typeof(YAttachProperty), new FrameworkPropertyMetadata(""));
        /// <summary>
        /// 获取水印
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string GetYWatermark(DependencyObject d)
        {
            return (string)d.GetValue(YWatermarkProperty);
        }
        /// <summary>
        /// 设置水印
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYWatermark(DependencyObject obj, string value)
        {
            obj.SetValue(YWatermarkProperty, value);
        }
        #endregion

        #region YContentDecorationsProperty 内容文字修饰
        /// <summary>
        /// 内容文字修饰附加属性
        /// </summary>
        public static readonly DependencyProperty YContentDecorationsProperty = DependencyProperty.RegisterAttached(
            "YContentDecorations", typeof(TextDecorationCollection), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 获取内容文字修饰
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static TextDecorationCollection GetYContentDecorations(DependencyObject d)
        {
            return (TextDecorationCollection)d.GetValue(YContentDecorationsProperty);
        }
        /// <summary>
        /// 设置内容文字修饰
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYContentDecorations(DependencyObject obj, TextDecorationCollection value)
        {
            obj.SetValue(YContentDecorationsProperty, value);
        }
        #endregion

        #region YLabelProperty TextBox的头部Label
        /// <summary>
        /// TextBox的头部Label附加属性
        /// </summary>
        public static readonly DependencyProperty YLabelProperty = DependencyProperty.RegisterAttached(
            "YLabel", typeof(string), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 获取TextBox的头部Label
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static string GetYLabel(DependencyObject d)
        {
            return (string)d.GetValue(YLabelProperty);
        }
        /// <summary>
        /// 设置TextBox的头部Label
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYLabel(DependencyObject obj, string value)
        {
            obj.SetValue(YLabelProperty, value);
        }
        #endregion

        #region YLabelTemplateProperty TextBox的头部Label模板
        /// <summary>
        /// TextBox的头部Label模板附加属性
        /// </summary>
        public static readonly DependencyProperty YLabelTemplateProperty = DependencyProperty.RegisterAttached(
            "YLabelTemplate", typeof(ControlTemplate), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 获取TextBox的头部Label模板
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(TextBox))]
        public static ControlTemplate GetYLabelTemplate(DependencyObject d)
        {
            return (ControlTemplate)d.GetValue(YLabelTemplateProperty);
        }
        /// <summary>
        /// 设置TextBox的头部Label模板
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYLabelTemplate(DependencyObject obj, ControlTemplate value)
        {
            obj.SetValue(YLabelTemplateProperty, value);
        }
        #endregion

        #region YImageSourceProperty YButton图片

        /// <summary>
        /// YButton的Icon位置附加属性
        /// </summary>
        public static readonly DependencyProperty YImageSourceProperty = DependencyProperty.RegisterAttached(
            "YImageSource", typeof(ImageSource), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        /// <summary>
        /// 获取YButton图片
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static ImageSource GetYImageSource(DependencyObject d)
        {
            return (ImageSource)d.GetValue(YImageSourceProperty);
        }
        /// <summary>
        /// 设置YButton图片
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYImageSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(YImageSourceProperty, value);
        }

        #endregion

        #region YStretchProperty 拉伸模式

        /// <summary>
        /// YButton的Icon位置附加属性
        /// </summary>
        public static readonly DependencyProperty YStretchProperty = DependencyProperty.RegisterAttached(
            "YStretch", typeof(Stretch), typeof(YAttachProperty), new FrameworkPropertyMetadata(Stretch.Uniform));
        /// <summary>
        /// 获取YButton图片
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Stretch GetYStretch(DependencyObject d)
        {
            return (Stretch)d.GetValue(YStretchProperty);
        }
        /// <summary>
        /// 设置YButton图片
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYStretch(DependencyObject obj, Stretch value)
        {
            obj.SetValue(YStretchProperty, value);
        }

        #endregion

        #region YCaretBrush

        /// <summary>
        /// YMenu弹出菜单BorderBrush附加属性
        /// </summary>
        public static readonly DependencyProperty YCaretBrushProperty = DependencyProperty.RegisterAttached(
            "YCaretBrush", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Black));
        /// <summary>
        /// 获取YMenu弹出菜单BorderBrush
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYCaretBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(YCaretBrushProperty);
        }
        /// <summary>
        /// 设置YMenu弹出菜单BorderBrush
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYCaretBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(YCaretBrushProperty, value);
        }

        #endregion

        #region YPopupBackground

        /// <summary>
        /// Popup背景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YPopupBackgroundProperty = DependencyProperty.RegisterAttached(
            "YPopupBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.White));
        /// <summary>
        /// 获取Popup背景颜色附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYPopupBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(YPopupBackgroundProperty);
        }
        /// <summary>
        /// 设置Popup背景颜色附加属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYPopupBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YPopupBackgroundProperty, value);
        }

        #endregion

        #region YExpandCollapseToggleBackground

        /// <summary>
        /// ExpandCollapseToggle背景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YExpandCollapseToggleBackgroundProperty = DependencyProperty.RegisterAttached(
            "YExpandCollapseToggleBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0xF0, 0xF0, 0xF0))));
        /// <summary>
        /// 获取ExpandCollapseToggle背景颜色附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYExpandCollapseToggleBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(YExpandCollapseToggleBackgroundProperty);
        }
        /// <summary>
        /// 设置ExpandCollapseToggle背景颜色附加属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYExpandCollapseToggleBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YExpandCollapseToggleBackgroundProperty, value);
        }

        #endregion

        #region YExpandCollapseToggleMouseOverBackground

        /// <summary>
        /// ExpandCollapseToggle背景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YExpandCollapseToggleMouseOverBackgroundProperty = DependencyProperty.RegisterAttached(
            "YExpandCollapseToggleMouseOverBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromArgb(0xFF, 0x00, 0x00, 0x88))));
        /// <summary>
        /// 获取ExpandCollapseToggle背景颜色附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYExpandCollapseToggleMouseOverBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(YExpandCollapseToggleMouseOverBackgroundProperty);
        }
        /// <summary>
        /// 设置ExpandCollapseToggle背景颜色附加属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYExpandCollapseToggleMouseOverBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YExpandCollapseToggleMouseOverBackgroundProperty, value);
        }

        #endregion

        #region YScrollBarButtonVisibilityProperty

        /// <summary>
        /// ScrollBar按钮显示附加属性
        /// </summary>
        public static readonly DependencyProperty YScrollBarButtonVisibilityProperty = DependencyProperty.RegisterAttached(
            "YScrollBarButtonVisibility", typeof(Visibility), typeof(YAttachProperty), new FrameworkPropertyMetadata(Visibility.Visible, FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 获取ScrollBar按钮显示附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Visibility GetYScrollBarButtonVisibility(DependencyObject d)
        {
            return (Visibility)d.GetValue(YScrollBarButtonVisibilityProperty);
        }
        /// <summary>
        /// 设置ScrollBar按钮显示附加属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYScrollBarButtonVisibility(DependencyObject obj, Visibility value)
        {
            obj.SetValue(YScrollBarButtonVisibilityProperty, value);
        }

        #endregion

        #region YContentStyleProperty

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty YContentStyleProperty = DependencyProperty.RegisterAttached(
            "YContentStyle", typeof(Style), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Style GetYContentStyle(DependencyObject d)
        {
            return (Style)d.GetValue(YContentStyleProperty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYContentStyle(DependencyObject obj, Style value)
        {
            obj.SetValue(YContentStyleProperty, value);
        }

        #endregion

        #region YGridTopRightBackground

        /// <summary>
        /// Popup背景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YGridTopRightBackgroundProperty = DependencyProperty.RegisterAttached(
            "YGridTopRightBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.White));
        /// <summary>
        /// 获取Popup背景颜色附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Brush GetYGridTopRightBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(YGridTopRightBackgroundProperty);
        }
        /// <summary>
        /// 设置Popup背景颜色附加属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYGridTopRightBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YGridTopRightBackgroundProperty, value);
        }

        #endregion

        #region YGridTopRightBorderBrush

        /// <summary>
        /// Popup背景颜色附加属性
        /// </summary>
        public static readonly DependencyProperty YGridTopRightBorderBrushProperty = DependencyProperty.RegisterAttached(
            "YGridTopRightBorderBrush", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.White));
        /// <summary>
        /// 获取Popup背景颜色附加属性
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static Brush GetYGridTopRightBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(YGridTopRightBorderBrushProperty);
        }
        /// <summary>
        /// 设置Popup背景颜色附加属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        [AttachedPropertyBrowsableForType(typeof(DataGrid))]
        public static void SetYGridTopRightBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(YGridTopRightBorderBrushProperty, value);
        }

        #endregion

        #endregion
    }
}
