using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace YUI.YUtil
{
    /// <summary>
    /// 控件辅助类
    /// </summary>
    public static class YControlHelper
    {
        /// <summary>
        /// 计算文字长度
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="typeface"></param>
        /// <param name="fontsize"></param>
        /// <returns></returns>
        public static Size MeasureString(string candidate, Typeface typeface, double fontsize)
        {
            var formattedText = new FormattedText(
                candidate,
                CultureInfo.CurrentUICulture,
                FlowDirection.LeftToRight,
                typeface,
                fontsize,
                Brushes.Black);

            return new Size(formattedText.Width, formattedText.Height);
        }

        /// <summary>
        /// 计算文字长度
        /// </summary>
        /// <param name="candidate"></param>
        /// <param name="textBlock"></param>
        /// <returns></returns>
        public static Size MeasureString(string candidate, TextBlock textBlock)
        {
            return MeasureString(
                candidate,
                new Typeface(textBlock.FontFamily, textBlock.FontStyle, textBlock.FontWeight, textBlock.FontStretch),
                textBlock.FontSize
            );
        }

        /// <summary>
        /// 获取指定类型的父控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="element"></param>
        /// <returns></returns>
        public static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            var parent = element;
            while (parent != null)
            {
                var correctlyTyped = parent as T;
                if (correctlyTyped != null)
                {
                    return correctlyTyped;
                }

                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }

            return null;
        }

        /// <summary>
        /// 获取鼠标下边指定类型的控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetElementUnderMouse<T>() where T : UIElement
        {
            return FindVisualParent<T>(Mouse.DirectlyOver as UIElement);
        }

        /// <summary>
        /// 获取指定类型的子控件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T GetVisualChild<T>(Visual parent) where T : Visual
        {
            var childContent = default(T);
            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                childContent = v as T ?? GetVisualChild<T>(v);

                if (childContent != null)
                    break;
            }
            return childContent;
        }
    }
}
