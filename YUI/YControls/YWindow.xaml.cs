using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using YUI.WPF.YUtil;

namespace YUI.WPF.YControls
{
    /// <summary>
    /// YWindow.xaml 的交互逻辑
    /// </summary>
    [TemplatePart(Name = "PART_Header", Type = typeof(Border))]
    public class YWindow : Window
    {
        static YWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YWindow), new FrameworkPropertyMetadata(typeof(YWindow)));
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand CloseWindowCommand { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand MaximizeWindowCommand { get; protected set; }

        /// <summary>
        /// 
        /// </summary>
        public ICommand MinimizeWindowCommand { get; protected set; }

        private double _lastMaxHeight;
        private double _lastMaxWidth;

        /// <summary>
        /// 
        /// </summary>
        public YWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            //this.SetWindowChrome();

            MaxHeight = SystemParameters.WorkArea.Height;
            MaxWidth = SystemParameters.WorkArea.Width;

            _lastMaxHeight = MaxHeight;
            _lastMaxWidth = MaxWidth;

            SystemParameters.StaticPropertyChanged += SystemParametersStaticPropertyChanged;

            Icon = YSystemHelper.GetIcon(Process.GetCurrentProcess().MainModule.FileName);
            MouseLeftButtonDown += (sender, args) =>
            {
                DragMove();
            };

            Loaded += (sender, args) =>
            {
                try
                {
                    //设置窗口为 无边框 背景透明 模式
                    this.SetWindowNoBorder(true);
                    if (!AllowsTransparency)
                        this.SetWindowTransparent();

                    WindowStyle = WindowStyle.None;
                }
                catch
                {
                    //
                }
            };

            CloseWindowCommand = new RoutedUICommand();
            MaximizeWindowCommand = new RoutedUICommand();
            MinimizeWindowCommand = new RoutedUICommand();

            this.BindCommand(CloseWindowCommand, CloseWindowCommandExecute);
            this.BindCommand(MaximizeWindowCommand, MaximizeWindowCommandExecute);
            this.BindCommand(MinimizeWindowCommand, MinimizeWindowExecute);
        }

        private void CloseWindowCommandExecute(object o, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void MaximizeWindowCommandExecute(object o, ExecutedRoutedEventArgs e)
        {
            if (ResizeMode != ResizeMode.CanResize) return;

            SizeToContent = SizeToContent.Manual;

            WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            if (e != null)
                e.Handled = true;
        }

        private void MinimizeWindowExecute(object o, ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
            e.Handled = true;
        }

        /// <summary>
        /// 系统参数改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private void SystemParametersStaticPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (SystemParameters.WorkArea.Height != _lastMaxHeight ||
                SystemParameters.WorkArea.Width != _lastMaxWidth)
            {
                Dispatcher.Invoke(() =>
                {
                    MaxHeight = SystemParameters.WorkArea.Height;
                    MaxWidth = SystemParameters.WorkArea.Width;

                    _lastMaxHeight = MaxHeight;
                    _lastMaxWidth = MaxWidth;

                    if (WindowState == WindowState.Maximized)
                    {
                        WindowState = WindowState.Normal;
                        WindowState = WindowState.Maximized;
                    }
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var headerBorder = (GetTemplateChild("PART_Header") as Border);

            if (headerBorder != null)
            {
                headerBorder.MouseLeftButtonDown += HeaderBorderOnMouseLeftButtonDown;
            }
        }

        private void HeaderBorderOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                MaximizeWindowCommandExecute(null, null);
        }
    }

    /// <summary>
    /// 窗口圆角转右上角关闭按钮圆角
    /// </summary>
    public class WindowCornerRadiusToCloseButton : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var tk = (CornerRadius)value;

                return new CornerRadius(0, tk.TopRight, 0, 0);
            }
            catch
            {
                return new CornerRadius(0);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
