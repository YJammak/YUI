using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shell;
using YUI.YUtil;

namespace YUI.YControls
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
}
