using System.Windows;
using System.Windows.Controls;

namespace YUI.WPF.YControls
{
    /// <summary>
    /// YLoading.xaml 的交互逻辑
    /// </summary>
    public class YLoading : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(YLoading), new PropertyMetadata(false));

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        static YLoading()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(YLoading), new FrameworkPropertyMetadata(typeof(YLoading)));
        }
    }
}
