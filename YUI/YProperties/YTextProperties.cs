using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YUI.YProperties
{
    public static partial class YAttachProperty
    {
        #region YTextTrimmingProperty 裁剪文本

        /// <summary>
        /// 裁剪文本附加属性
        /// </summary>
        public static readonly DependencyProperty YTextTrimmingProperty = DependencyProperty.RegisterAttached(
            "YTextTrimming", typeof(TextTrimming), typeof(YAttachProperty), new FrameworkPropertyMetadata(TextTrimming.None));
        /// <summary>
        /// 获取裁剪文本
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static TextTrimming GetYTextTrimming(DependencyObject d)
        {
            return (TextTrimming)d.GetValue(YTextTrimmingProperty);
        }
        /// <summary>
        /// 设置裁剪文本
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYTextTrimming(DependencyObject obj, TextTrimming value)
        {
            obj.SetValue(YTextTrimmingProperty, value);
        }

        #endregion

        #region YTextAlignmentProperty 垂直

        /// <summary>
        /// 裁剪文本附加属性
        /// </summary>
        public static readonly DependencyProperty YTextAlignmentProperty = DependencyProperty.RegisterAttached(
            "YTextAlignment", typeof(System.Windows.HorizontalAlignment), typeof(YAttachProperty), new FrameworkPropertyMetadata(System.Windows.HorizontalAlignment.Center));
        /// <summary>
        /// 获取裁剪文本
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static System.Windows.HorizontalAlignment GetYTextAlignment(DependencyObject d)
        {
            return (System.Windows.HorizontalAlignment)d.GetValue(YTextAlignmentProperty);
        }
        /// <summary>
        /// 设置裁剪文本
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="value"></param>
        public static void SetYTextAlignment(DependencyObject obj, System.Windows.HorizontalAlignment value)
        {
            obj.SetValue(YTextAlignmentProperty, value);
        }

        #endregion
    }
}
