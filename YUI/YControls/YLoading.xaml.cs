using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YUI.YControls
{
    /// <summary>
    /// YLoading.xaml 的交互逻辑
    /// </summary>
    public partial class YLoading : UserControl
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
