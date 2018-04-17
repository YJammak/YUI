using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Effects;
using YUI.YControls;

namespace YUI.YUtil
{
    /// <summary>
    /// Popup辅助类
    /// </summary>
    public static class YPopUpToolTipHelper
    {
        /// <summary>
        /// 在指定控件上弹出信息
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="message">信息</param>
        /// <param name="duration">持续时间(ms)</param>
        public static void PopupToolTip(this FrameworkElement control, string message, double duration = 3000)
        {
            PopupToolTip(control, message, duration, new FontFamily("微软雅黑"));
        }

        /// <summary>
        /// 在指定控件上弹出信息
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="message">信息</param>
        /// <param name="duration">持续时间(ms)</param>
        /// <param name="fontFamily"></param>
        /// <param name="fontSize">字体大小</param>
        /// <param name="foreground">字体颜色</param>
        public static void PopupToolTip(this FrameworkElement control, string message, double duration,
            FontFamily fontFamily, double fontSize = 12, Brush foreground = null)
        {
            if (control == null)
                return;

            var popText = new TextBlock
            {
                Text = message,
                Foreground = foreground ?? new SolidColorBrush(Color.FromRgb(0x55, 0x55, 0x55)),
                FontSize = fontSize,
                Margin = new Thickness(10, 3, 10, 3),
                FontFamily = fontFamily,
                TextAlignment = TextAlignment.Left,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            PopupToolTip(control, popText, duration);
        }

        private static readonly ConcurrentDictionary<FrameworkElement, Popup> dictionary = new ConcurrentDictionary<FrameworkElement, Popup>();

        /// <summary>
        /// 在指定控件上弹出信息
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="content">内容</param>
        /// <param name="duration">持续时间(ms)</param>
        public static void PopupToolTip(this FrameworkElement control, FrameworkElement content, double duration)
        {
            if (control == null)
                return;

            var pop = new Popup { AllowsTransparency = true };

            var border = new YTailBorder
            {
                Background = Brushes.White,
                Child = content,
                Placement = Placement.TopLeft,
                TailWidth = 8,
                TailHeight = 6,
                TailHorizontalOffset = 6,
                Effect = new DropShadowEffect
                {
                    BlurRadius = 3,
                    Color = Colors.Black,
                    ShadowDepth = 0,
                    Opacity = 0.5
                },
                Margin = new Thickness(5),
                CornerRadius = new CornerRadius(2),
                Padding = new Thickness(0)
            };

            var grid = new Grid();
            grid.Children.Add(border);

            pop.Child = grid;

            pop.PlacementTarget = control;
            pop.Placement = PlacementMode.Bottom;

            pop.VerticalOffset = -5;
            pop.HorizontalOffset = control.ActualWidth / 10 - 5;

            if (dictionary.ContainsKey(control))
            {
                dictionary[control].IsOpen = false;
                dictionary[control] = pop;
            }

            pop.IsOpen = true;

            dictionary.AddOrUpdate(control, pop, (element, popup) => pop);

            var t = new System.Timers.Timer { Interval = duration };

            t.Elapsed += (x, y) =>
            {
                pop.Dispatcher.Invoke(() =>
                {
                    pop.IsOpen = false;

                    if (ReferenceEquals(dictionary[control], pop))
                        dictionary.TryRemove(control, out _);

                    t.Stop();
                });
            };

            t.Start();
        }
    }
}
