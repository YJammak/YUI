using System.Windows;

namespace YUI.WPF.YProperties
{
    public static partial class YAttachProperty
    {
        #region YCornerRadiusProperty Border圆角
        /// <summary>
        /// Border圆角附加属性
        /// </summary>
        public static readonly DependencyProperty YCornerRadiusProperty = DependencyProperty.RegisterAttached(
            "YCornerRadius", typeof(CornerRadius), typeof(YAttachProperty), new FrameworkPropertyMetadata(null));
        
        /// <summary>
        /// 获取Border圆角
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static CornerRadius GetYCornerRadius(DependencyObject d)
        {
            return (CornerRadius)d.GetValue(YCornerRadiusProperty);
        }
        
        /// <summary>
        /// 设置Border圆角
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYCornerRadius(DependencyObject obj, CornerRadius value)
        {
            obj.SetValue(YCornerRadiusProperty, value);
        }
        #endregion
    }
}
