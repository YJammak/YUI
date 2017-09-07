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
using YUI.YControls;
using YUI.YUtil;

namespace YUI.Test
{
    public class DataGridTest : IYAutoCompleteBoxKeys
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

        public IEnumerable<string> Keywords { get; set; } = new List<string>();

        public override string ToString()
        {
            return Name;
        }
    }

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : YWindow
    {
        public List<DataGridTest> Data { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Data = new List<DataGridTest>();

            for (int i = 0; i < 10000; ++i)
            {
                Data.Add(new DataGridTest { ID = i.ToString(), Name = $"Test{i}", Phone = "1234125412", Address = "asdgbhiuas" });
            }

            //YJsonHelper.LoadAssembly("../Newtonsoft.Json.dll");
            //Console.WriteLine(YJsonHelper.SerializeObject(Data));
            var md5 = YMD5Helper.GetFileMD5("D:\\HKVideoCtrl.ocx");
        }

        private bool s = false;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ygifImage.Source = new Uri($"Resources/GIF{(s ? "1" : "2")}.gif", UriKind.RelativeOrAbsolute);
            s = !s;
            textBox.PopupToolTip("这是弹出信息");
            YMessageBox.ShowWindow(yAutoCompleteBox.SelectSuggest?.ToString() ?? "没选中", "这是测试标题", MessageBoxButton.OKCancel, MessageBoxImage.Information);
        }
    }
}
