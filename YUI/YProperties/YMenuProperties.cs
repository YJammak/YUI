using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace YUI.YProperties
{
    public static partial class YAttachProperty
    {
        #region YMenuPopupBackground YMenu弹出菜单背景色

        /// <summary>
        /// YMenu弹出菜单背景色附加属性
        /// </summary>
        public static readonly DependencyProperty YMenuPopupBackgroundProperty = DependencyProperty.RegisterAttached(
            "YMenuPopupBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 获取YMenu弹出菜单背景色
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Menu))]
        public static Brush GetYMenuPopupBackground(DependencyObject d)
        {
            return (Brush)d.GetValue(YMenuPopupBackgroundProperty);
        }
        /// <summary>
        /// 设置YMenu弹出菜单背景色
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYMenuPopupBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(YMenuPopupBackgroundProperty, value);
        }

        #endregion

        #region YMenuPopupBorderBrush YMenu弹出菜单BorderBrush

        /// <summary>
        /// YMenu弹出菜单BorderBrush附加属性
        /// </summary>
        public static readonly DependencyProperty YMenuPopupBorderBrushProperty = DependencyProperty.RegisterAttached(
            "YMenuPopupBorderBrush", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 获取YMenu弹出菜单BorderBrush
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Menu))]
        public static Brush GetYMenuPopupBorderBrush(DependencyObject d)
        {
            return (Brush)d.GetValue(YMenuPopupBorderBrushProperty);
        }
        /// <summary>
        /// 设置YMenu弹出菜单BorderBrush
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYMenuPopupBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(YMenuPopupBorderBrushProperty, value);
        }

        #endregion

        #region YMenuPopupBorderThickness YMenu弹出菜单BorderThickness

        /// <summary>
        /// YMenu弹出菜单BorderThickness附加属性
        /// </summary>
        public static readonly DependencyProperty YMenuPopupBorderThicknessProperty = DependencyProperty.RegisterAttached(
            "YMenuPopupBorderThickness", typeof(Thickness), typeof(YAttachProperty), new FrameworkPropertyMetadata(new Thickness(0),
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 获取YMenu弹出菜单BorderThickness
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Menu))]
        public static Thickness GetYMenuPopupBorderThickness(DependencyObject d)
        {
            return (Thickness)d.GetValue(YMenuPopupBorderThicknessProperty);
        }
        /// <summary>
        /// 设置YMenu弹出菜单BorderThickness
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYMenuPopupBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(YMenuPopupBorderThicknessProperty, value);
        }

        #endregion

        #region YMenuPopupCornerRadius YMenu弹出菜单CornerRadius

        /// <summary>
        /// YMenu弹出菜单BorderThickness附加属性
        /// </summary>
        public static readonly DependencyProperty YMenuPopupCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "YMenuPopupCornerRadius", typeof(CornerRadius), typeof(YAttachProperty), new FrameworkPropertyMetadata(new CornerRadius(0),
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 获取YMenu弹出菜单BorderThickness
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.Menu))]
        public static CornerRadius GetYMenuPopupCornerRadius(DependencyObject d)
        {
            return (CornerRadius)d.GetValue(YMenuPopupCornerRadiusProperty);
        }
        /// <summary>
        /// 设置YMenu弹出菜单BorderThickness
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYMenuPopupCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(YMenuPopupCornerRadiusProperty, value);
        }

        #endregion

        #region YMenuPopupBorderThickness YMenu弹出菜单BorderThickness

        /// <summary>
        /// YMenu弹出菜单BorderThickness附加属性
        /// </summary>
        public static readonly DependencyProperty YGridTopRightBorderThicknessProperty = DependencyProperty.RegisterAttached(
            "YGridTopRightBorderThickness", typeof(Thickness), typeof(YAttachProperty), new FrameworkPropertyMetadata(new Thickness(0),
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 获取YMenu弹出菜单BorderThickness
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.DataGrid))]
        public static Thickness GetYGridTopRightBorderThickness(DependencyObject d)
        {
            return (Thickness)d.GetValue(YGridTopRightBorderThicknessProperty);
        }
        /// <summary>
        /// 设置YMenu弹出菜单BorderThickness
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        [AttachedPropertyBrowsableForType(typeof(System.Windows.Controls.DataGrid))]
        public static void SetYGridTopRightBorderThickness(DependencyObject obj, Thickness value)
        {
            obj.SetValue(YGridTopRightBorderThicknessProperty, value);
        }

        #endregion
    }
}
