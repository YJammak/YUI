using System;
using System.Globalization;
using System.Linq;
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
        /// 绑定命令和命令事件到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement ui, ICommand com, Action<object, ExecutedRoutedEventArgs> call)
        {
            var bind = new CommandBinding(com);
            bind.Executed += new ExecutedRoutedEventHandler(call);
            ui.CommandBindings.Add(bind);
        }

        /// <summary>
        /// 绑定RelayCommand命令到宿主UI
        /// </summary>
        public static void BindCommand(this UIElement ui, ICommand com)
        {
            var bind = new CommandBinding(com);
            ui.CommandBindings.Add(bind);
        }

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
                if (parent is T correctlyTyped)
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

        /// <summary>
        /// 获取控件是否未通过验证
        /// </summary>
        /// <param name="node"></param>
        /// <param name="errorMsg"></param>
        /// <param name="errorElement"></param>
        /// <returns></returns>
        public static bool IsHasError(DependencyObject node, out string errorMsg, out IInputElement errorElement)
        {
            errorMsg = string.Empty;
            errorElement = null;
            if (node == null) return false;

            var isValid = !Validation.GetHasError(node);
            if (!isValid)
            {
                if (node is IInputElement element)
                {
                    if (element.IsEnabled)
                    {
                        var ve = Validation.GetErrors(node).FirstOrDefault();
                        if (ve != null)
                        {
                            errorMsg = ve.ErrorContent.ToString();
                        }
                        errorElement = element;
                        Keyboard.Focus(element);
                        return true;
                    }
                }
            }

            foreach (var subnode in LogicalTreeHelper.GetChildren(node))
            {
                if (!(subnode is DependencyObject)) continue;

                if (IsHasError((DependencyObject)subnode, out errorMsg, out errorElement)) return true;
            }

            return false;
        }
    }
}
