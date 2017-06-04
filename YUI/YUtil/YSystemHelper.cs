using System;
using System.Reflection;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace YUI.YUtil
{
    /// <summary>
    /// DPI信息
    /// </summary>
    public class DpiInfo
    {
        /// <summary>
        /// DPI在X轴的缩放
        /// </summary>
        public double DpiXScale => DpiX / 96.0;

        /// <summary>
        /// DPI在Y轴的缩放
        /// </summary>
        public double DpiYScale => DpiY / 96.0;

        /// <summary>
        /// X轴DPI
        /// </summary>
        public int DpiX { get; set; }

        /// <summary>
        /// Y轴DPI
        /// </summary>
        public int DpiY { get; set; }
    }

    /// <summary>
    /// 系统信息辅助类
    /// </summary>
    public static class YSystemHelper
    {
        /// <summary>
        /// 获取系统DPI
        /// </summary>
        /// <returns></returns>
        public static DpiInfo GetDpiInfo()
        {
            var dpiXProperty = typeof(SystemParameters).GetProperty("DpiX", BindingFlags.NonPublic | BindingFlags.Static);
            var dpiYProperty = typeof(SystemParameters).GetProperty("Dpi", BindingFlags.NonPublic | BindingFlags.Static);

            if (dpiXProperty == null || dpiYProperty == null)
                throw new Exception("获取DPI失败");

            var dpiX = (int)dpiXProperty.GetValue(null, null);
            var dpiY = (int)dpiYProperty.GetValue(null, null);

            return new DpiInfo() { DpiX = dpiX, DpiY = dpiY };
        }

        /// <summary>
        /// 获取经过DPI处理过的点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Point GetPointDealWithDpi(Point p)
        {
            var dpi = GetDpiInfo();
            return new Point(p.X / dpi.DpiXScale, p.Y / dpi.DpiYScale);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static ImageSource GetIcon(string fileName)
        {
            var icon = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
            if (icon == null) return null;
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                new Int32Rect(0, 0, icon.Width, icon.Height),
                BitmapSizeOptions.FromEmptyOptions());
        }
    }
}
