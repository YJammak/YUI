using System.Windows;
using System.Windows.Controls;

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
    }
}
