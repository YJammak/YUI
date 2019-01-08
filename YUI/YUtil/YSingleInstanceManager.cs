using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.VisualBasic.ApplicationServices;

namespace YUI.WPF.YUtil
{
    /// <summary>
    /// 下一个实例启动事件句柄
    /// </summary>
    /// <param name="commandLine"></param>
    public delegate void NextInstanceStartupedHandle(IReadOnlyCollection<string> commandLine);

    /// <summary>
    /// 程序单例模式
    /// </summary>
    public class YSingleInstanceManager
    {
        /// <summary>
        /// 下一个实例启动事件
        /// </summary>
        public event NextInstanceStartupedHandle NextInstanceStartuped;

        private SingleInstanceManager manager { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="app"></param>
        public YSingleInstanceManager(Application app)
        {
            manager = new SingleInstanceManager(app);
            manager.NextInstanceStartuped += args =>
            {
                NextInstanceStartuped?.Invoke(args);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commadLine"></param>
        public void Run(string[] commadLine)
        {
            manager?.Run(commadLine);
        }
    }


    internal class SingleInstanceManager : WindowsFormsApplicationBase
    {

        /// <summary>
        /// 下一个实例启动事件
        /// </summary>
        public event NextInstanceStartupedHandle NextInstanceStartuped;

        private Application App { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="app"></param>
        public SingleInstanceManager(Application app)
        {
            IsSingleInstance = true;
            App = app ?? throw new ArgumentNullException(nameof(app));
        }

        /// <summary>
        /// 程序启动
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override bool OnStartup(Microsoft.VisualBasic.ApplicationServices.StartupEventArgs e)
        {
            // First time app is launched
            App.Run();
            return false;
        }

        /// <summary>
        /// 第二个实例启动时处理函数
        /// </summary>
        /// <param name="eventArgs"></param>
        protected override void OnStartupNextInstance(StartupNextInstanceEventArgs eventArgs)
        {
            // Subsequent launches
            base.OnStartupNextInstance(eventArgs);
            NextInstanceStartuped?.Invoke(eventArgs.CommandLine);
        }
    }
}
