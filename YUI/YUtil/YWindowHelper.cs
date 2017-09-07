using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Shell;

namespace YUI.YUtil
{
    /// <summary>
    /// 窗口辅助类
    /// </summary>
    public static class YWindowHelper
    {
        private const int WS_EX_TRANSPARENT = 0x20;

        /// <summary> 
        /// 带有外边框和标题的windows的样式 
        /// </summary> 
        private const int WS_CAPTION = 0X00C0000;

        // public const long WS_BORDER = 0X0080000L; 

        /// <summary> 
        /// 带有alpha的样式 
        /// </summary> 
        private const int LWA_ALPHA = 0x00000002;

        /// <summary> 
        /// window的基本样式 
        /// </summary> 
        private const int GWL_STYLE = -16;

        /// <summary>
        /// 工具窗口
        /// </summary>
        private const int WS_EX_TOOLWINDOW = 0x80;

        /// <summary> 
        /// window的扩展样式 
        /// </summary> 
        private const int GWL_EXSTYLE = -20;

        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        /// <summary>
        /// 窗口从最小化恢复
        /// </summary>
        private const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        /// <summary> 
        /// Sets the layered window attributes. 
        /// </summary> 
        /// <param name="handle">要进行操作的窗口句柄</param> 
        /// <param name="colorKey">RGB的值</param> 
        /// <param name="alpha">Alpha的值，透明度</param> 
        /// <param name="flags">附带参数</param> 
        /// <returns>true or false</returns> 
        [DllImport("User32.dll")]
        public static extern bool SetLayeredWindowAttributes(IntPtr handle, int colorKey, byte alpha, int flags);

        /// <summary>
        /// 设置窗口边框用于拖拽改变大小
        /// </summary>
        /// <param name="window"></param>
        /// <param name="wc"></param>
        public static void SetWindowChrome(this Window window, WindowChrome wc = null)
        {
            if (window == null) return;

            if (wc == null)
            {
                switch (window.ResizeMode)
                {
                    case ResizeMode.NoResize:
                    case ResizeMode.CanResizeWithGrip:
                    case ResizeMode.CanMinimize:
                        wc = new WindowChrome
                        {
                            CaptionHeight = 0,
                            GlassFrameThickness = new Thickness(0),
                            CornerRadius = new CornerRadius(0),
                            UseAeroCaptionButtons = true,
                            ResizeBorderThickness = new Thickness(0)
                        };
                        break;
                    case ResizeMode.CanResize:
                        wc = new WindowChrome
                        {
                            CaptionHeight = 0,
                            GlassFrameThickness = new Thickness(7),
                            CornerRadius = new CornerRadius(0),
                            UseAeroCaptionButtons = true,
                            ResizeBorderThickness = new Thickness(6)
                        };
                        break;
                }
            }

            WindowChrome.SetWindowChrome(window, wc);
        }

        /// <summary>
        /// 窗口穿透
        /// </summary>
        /// <param name="window"></param>
        /// <param name="canPenetrate"></param>
        public static void SetWindowCanPenetrate(this Window window, bool canPenetrate)
        {
            if (window == null) return;

            var hwnd = new WindowInteropHelper(window).Handle;
            var extendedStyle = GetWindowLong(hwnd, GWL_EXSTYLE);

            if (canPenetrate)
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle | WS_EX_TRANSPARENT);
            else
                SetWindowLong(hwnd, GWL_EXSTYLE, extendedStyle & (~WS_EX_TRANSPARENT));
        }

        /// <summary>
        /// 设置窗口无边框
        /// </summary>
        /// <param name="window"></param>
        /// <param name="isNoBorder"></param>
        public static void SetWindowNoBorder(this Window window, bool isNoBorder)
        {
            if (window == null) return;

            // 获取窗体句柄 
            var hwnd = new WindowInteropHelper(window).Handle;

            // 获得窗体的 样式 
            var oldstyle = GetWindowLong(hwnd, GWL_STYLE);

            // 创建圆角窗体  12 这个值可以根据自身项目进行设置 
            //NativeMethods.SetWindowRgn(hwnd, NativeMethods.CreateRoundRectRgn(0, 0, Convert.ToInt32(this.ActualWidth), Convert.ToInt32(this.ActualHeight), 12, 12), true);

            if (isNoBorder)
                SetWindowLong(hwnd, GWL_STYLE, oldstyle & ~WS_CAPTION);
            else
                SetWindowLong(hwnd, GWL_STYLE, oldstyle | WS_CAPTION);
        }

        /// <summary>
        /// 设置窗口背景透明
        /// </summary>
        /// <param name="window"></param>
        public static void SetWindowTransparent(this Window window)
        {
            if (window == null) return;

            // 获取窗体句柄 
            var hwnd = new WindowInteropHelper(window).Handle;

            // 创建圆角窗体  12 这个值可以根据自身项目进行设置 
            //NativeMethods.SetWindowRgn(hwnd, NativeMethods.CreateRoundRectRgn(0, 0, Convert.ToInt32(this.ActualWidth), Convert.ToInt32(this.ActualHeight), 12, 12), true);

            SetLayeredWindowAttributes(hwnd, 1 | 2 << 8 | 3 << 16, 0, LWA_ALPHA);
        }

        /// <summary>
        /// 设置窗口为工具窗口
        /// </summary>
        /// <param name="window"></param>
        public static void SetWindowToolWindow(this Window window)
        {
            if (window == null) return;

            var hWnd = new WindowInteropHelper(window).Handle;

            var extendedStyle = GetWindowLong(hWnd, GWL_EXSTYLE);

            SetWindowLong(hWnd, GWL_EXSTYLE, extendedStyle | WS_EX_TOOLWINDOW);
        }

        /// <summary>
        /// 设置窗口从最小化恢复
        /// </summary>
        /// <param name="window"></param>
        public static void SetWindowRestore(this Window window)
        {
            if (window == null || window.WindowState != WindowState.Minimized) return;

            ShowWindow(new WindowInteropHelper(window).Handle, SW_RESTORE);
        }
    }
}
