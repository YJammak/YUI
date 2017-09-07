using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using YUI.YUtil;

namespace YUI.Test
{
    static class Program
    {
        [System.STAThread()]
        [System.Diagnostics.DebuggerNonUserCode()]
        [System.CodeDom.Compiler.GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public static void Main(string[] args)
        {
            var app = new App();
            app.InitializeComponent();
            var manager = new YSingleInstanceManager(app);
            manager.NextInstanceStartuped += line =>
            {
                Application.Current.MainWindow?.Activate();
            };
            manager.Run(args);
        }
    }
}
