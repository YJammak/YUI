using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Shapes;

namespace YUI.YUtil
{
    /// <summary>
    /// Popup辅助类
    /// </summary>
    public static class YPopUpHelper
    {
        /// <summary>
        /// 在指定控件上弹出信息
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="message">信息</param>
        /// <param name="duration">持续时间(ms)</param>
        public static void ShowPopUpOnControl(this FrameworkElement control, string message, double duration)
        {
            ShowPopUpOnControl(control, message, duration, new FontFamily("微软雅黑"));
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
        public static void ShowPopUpOnControl(this FrameworkElement control, string message, double duration,
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

            ShowPopUpOnControl(control, popText, duration);
        }

        /// <summary>
        /// 在指定控件上弹出信息
        /// </summary>
        /// <param name="control">控件</param>
        /// <param name="content">内容</param>
        /// <param name="duration">持续时间(ms)</param>
        public static void ShowPopUpOnControl(this FrameworkElement control, FrameworkElement content, double duration)
        {
            if (control == null)
                return;

            var pop = new Popup { AllowsTransparency = true };

            var popBorder = new Border
            {
                CornerRadius = new CornerRadius(2),
                Background = new SolidColorBrush(Colors.WhiteSmoke),
                Margin = new Thickness(5, 0, 5, 5),
                Child = content
            };

            var polygon = new Polygon();
            polygon.Points.Add(new Point(4, 0));
            polygon.Points.Add(new Point(8, 6));
            polygon.Points.Add(new Point(0, 6));
            polygon.Fill = new SolidColorBrush(Colors.WhiteSmoke);

            polygon.Margin = new Thickness(8, 5, 0, 0);

            var panel = new StackPanel();
            panel.Children.Add(polygon);
            panel.Children.Add(popBorder);

            var dropShadow = new DropShadowEffect
            {
                BlurRadius = 3,
                Color = Colors.Black,
                ShadowDepth = 0
            };

            panel.Effect = dropShadow;

            pop.Child = panel;

            pop.PlacementTarget = control;
            pop.Placement = PlacementMode.Relative;

            pop.VerticalOffset = control.ActualHeight - 6;
            pop.HorizontalOffset = control.ActualWidth / 10;

            pop.IsOpen = true;

            System.Timers.Timer t = new System.Timers.Timer { Interval = duration };

            t.Elapsed += (x, y) =>
            {
                pop.Dispatcher.Invoke(() =>
                {
                    pop.IsOpen = false;
                    t.Stop();
                });
            };

            t.Start();
        }
    }
}
