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
        #region YCheckedForegroundProperty 被选中前景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YCheckedForegroundProperty = DependencyProperty.RegisterAttached(
            "YCheckedForeground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中前景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYCheckedForeground(DependencyObject element, Brush value)
        {
            element.SetValue(YCheckedForegroundProperty, value);
        }
        /// <summary>
        /// 获取被选中前景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYCheckedForeground(DependencyObject element)
        {
            return (Brush)element.GetValue(YCheckedForegroundProperty);
        }

        #endregion

        #region YCheckedBackgroundProperty 被选中背景色
        /// <summary>
        /// 鼠标悬浮前景色附加属性
        /// </summary>
        public static readonly DependencyProperty YCheckedBackgroundProperty = DependencyProperty.RegisterAttached(
            "YCheckedBackground", typeof(Brush), typeof(YAttachProperty), new FrameworkPropertyMetadata(Brushes.Transparent,
                FrameworkPropertyMetadataOptions.AffectsRender | FrameworkPropertyMetadataOptions.Inherits));
        /// <summary>
        /// 设置被选中背景色
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SetYCheckedBackground(DependencyObject element, Brush value)
        {
            element.SetValue(YCheckedBackgroundProperty, value);
        }
        /// <summary>
        /// 获取被选中背景色
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Brush GetYCheckedBackground(DependencyObject element)
        {
            return (Brush)element.GetValue(YCheckedBackgroundProperty);
        }

        #endregion
    }
}
