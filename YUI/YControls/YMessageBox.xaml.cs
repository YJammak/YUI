using System;
using System.Windows;
using System.Windows.Media;

namespace YUI.YControls
{
    /// <summary>
    /// YMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class YMessageBox
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageBoxImageProperty = DependencyProperty.Register(
            "MessageBoxImage", typeof(MessageBoxImage), typeof(YMessageBox), new PropertyMetadata(default(MessageBoxImage)));

        /// <summary>
        /// 
        /// </summary>
        public MessageBoxImage MessageBoxImage
        {
            get => (MessageBoxImage)GetValue(MessageBoxImageProperty);
            set => SetValue(MessageBoxImageProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageProperty = DependencyProperty.Register(
            "Message", typeof(string), typeof(YMessageBox), new PropertyMetadata(default(string)));

        /// <summary>
        /// 
        /// </summary>
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MessageBoxButtonProperty = DependencyProperty.Register(
            "MessageBoxButton", typeof(MessageBoxButton), typeof(YMessageBox), new PropertyMetadata(default(MessageBoxButton)));

        /// <summary>
        /// 
        /// </summary>
        public MessageBoxButton MessageBoxButton
        {
            get => (MessageBoxButton)GetValue(MessageBoxButtonProperty);
            set => SetValue(MessageBoxButtonProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ButtonBackgroundProperty = DependencyProperty.Register(
            "ButtonBackground", typeof(Brush), typeof(YMessageBox), new PropertyMetadata(Brushes.DodgerBlue));

        /// <summary>
        /// 
        /// </summary>
        public Brush ButtonBackground
        {
            get => (Brush) GetValue(ButtonBackgroundProperty);
            set => SetValue(ButtonBackgroundProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty MouseOverButtonBackgroundProperty = DependencyProperty.Register(
            "MouseOverButtonBackground", typeof(Brush), typeof(YMessageBox), new PropertyMetadata(Brushes.DodgerBlue));

        /// <summary>
        /// 
        /// </summary>
        public Brush MouseOverButtonBackground
        {
            get => (Brush) GetValue(MouseOverButtonBackgroundProperty);
            set => SetValue(MouseOverButtonBackgroundProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty PressedButtonBackgroundProperty = DependencyProperty.Register(
            "PressedButtonBackground", typeof(Brush), typeof(YMessageBox), new PropertyMetadata(Brushes.DodgerBlue));

        /// <summary>
        /// 
        /// </summary>
        public Brush PressedButtonBackground
        {
            get => (Brush) GetValue(PressedButtonBackgroundProperty);
            set => SetValue(PressedButtonBackgroundProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty ButtonForegroundProperty = DependencyProperty.Register(
            "ButtonForeground", typeof(Brush), typeof(YMessageBox), new PropertyMetadata(Brushes.White));

        /// <summary>
        /// 
        /// </summary>
        public Brush ButtonForeground
        {
            get => (Brush) GetValue(ButtonForegroundProperty);
            set => SetValue(ButtonForegroundProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IconForegroundProperty = DependencyProperty.Register(
            "IconForeground", typeof(Brush), typeof(YMessageBox), new PropertyMetadata(Brushes.DodgerBlue));

        /// <summary>
        /// 
        /// </summary>
        public Brush IconForeground
        {
            get => (Brush) GetValue(IconForegroundProperty);
            set => SetValue(IconForegroundProperty, value);
        }

        /// <summary>
        /// 
        /// </summary>
        private YMessageBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static bool? ShowWindow(string message)
        {
            return ShowWindow(message, "", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool? ShowWindow(string message, string title)
        {
            return ShowWindow(message, title, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="button"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool? ShowWindow(string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                var window = new YMessageBox()
                {
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button,
                };

                PlaySound(image);

                return window.ShowDialog();
            }

            bool? result = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var window = new YMessageBox()
                {
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button
                };

                PlaySound(image);

                result = window.ShowDialog();
            });

            return result;
        }

        /// <summary>
        /// 播放相关声音
        /// </summary>
        /// <param name="image"></param>
        public static void PlaySound(MessageBoxImage image)
        {
            switch (image)
            {
                case MessageBoxImage.None:
                    break;
                case MessageBoxImage.Hand:
                    System.Media.SystemSounds.Hand.Play();
                    break;
                case MessageBoxImage.Question:
                    System.Media.SystemSounds.Question.Play();
                    break;
                case MessageBoxImage.Exclamation:
                    System.Media.SystemSounds.Exclamation.Play();
                    break;
                case MessageBoxImage.Asterisk:
                    System.Media.SystemSounds.Asterisk.Play();
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="button"></param>
        /// <param name="image"></param>
        /// <returns></returns>
        public static bool? ShowWindow(Window owner, string message, string title, MessageBoxButton button, MessageBoxImage image)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                var window = new YMessageBox()
                {
                    Owner = owner,
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };

                return window.ShowDialog();
            }

            bool? result = null;

            Application.Current.Dispatcher.Invoke(() =>
            {
                var window = new YMessageBox()
                {
                    Owner = owner,
                    Message = message,
                    Title = title,
                    MessageBoxImage = image,
                    MessageBoxButton = button
                };

                result = window.ShowDialog();
            });

            return result;
        }


        private void YesButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void NoButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void OkButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
