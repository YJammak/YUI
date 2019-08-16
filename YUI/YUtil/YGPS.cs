using System;

namespace YUI.WPF.YUtil
{
    /// <summary>
    /// GPS坐标
    /// </summary>
    public class YGpsLocation
    {
        /// <summary>
        /// 经度
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public YGpsLocation() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        public YGpsLocation(double lng, double lat)
        {
            Longitude = lng;
            Latitude = lat;
        }
    }

    /// <summary>
    /// GPS定位坐标转换类
    /// </summary>
    public static class YGps
    {
        //定义一些常量
        private const double XPi = 3.14159265358979324 * 3000.0 / 180.0;
        private const double Pi = 3.1415926535897932384626;
        private const double A = 6378245.0;
        private const double Ee = 0.00669342162296594323;

        /// <summary>
        /// 指定坐标是否在国外
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public static bool IsOutOfChina(YGpsLocation location)
        {
            return (location.Longitude < 72.004 || location.Longitude > 137.8347) || (location.Latitude < 0.8293 || location.Latitude > 55.8271);
        }

        /// <summary>
        /// 指定坐标是否在国外
        /// </summary>
        /// <param name="lng"></param>
        /// <param name="lat"></param>
        /// <returns></returns>
        public static bool IsOutOfChina(double lng, double lat)
        {
            return (lng < 72.004 || lng > 137.8347) || (lat < 0.8293 || lat > 55.8271);
        }

        /// <summary>
        /// BD09 转 GCJ02
        /// 百度坐标 转 火星坐标
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static YGpsLocation BD09ToGCJ02(YGpsLocation location)
        {
            var result = BD09ToGCJ02(location.Longitude, location.Latitude);
            return new YGpsLocation(result[0], result[1]);
        }

        /// <summary>
        /// BD09 转 GCJ02
        /// 百度坐标 转 火星坐标
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static double[] BD09ToGCJ02(double bdLon, double bdLat)
        {
            var x = bdLon - 0.0065;
            var y = bdLat - 0.006;
            var z = Math.Sqrt(x * x + y * y) - 0.00002 * Math.Sin(y * XPi);
            var theta = Math.Atan2(y, x) - 0.000003 * Math.Cos(x * XPi);
            var ggLng = z * Math.Cos(theta);
            var ggLat = z * Math.Sin(theta);
            return new[] { ggLng, ggLat };
        }

        /// <summary>
        /// GCJ02 转 BD09
        /// 火星坐标 转 百度坐标
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static YGpsLocation GCJ02ToBD09(YGpsLocation location)
        {
            var result = GCJ02ToBD09(location.Longitude, location.Latitude);
            return new YGpsLocation(result[0], result[1]);
        }

        /// <summary>
        /// GCJ02 转 BD09
        /// 火星坐标 转 百度坐标
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static double[] GCJ02ToBD09(double lng, double lat)
        {
            var z = Math.Sqrt(lng * lng + lat * lat) + 0.00002 * Math.Sin(lat * XPi);
            var theta = Math.Atan2(lat, lng) + 0.000003 * Math.Cos(lng * XPi);
            var bdLng = z * Math.Cos(theta) + 0.0065;
            var bdLat = z * Math.Sin(theta) + 0.006;
            return new[] { bdLng, bdLat };
        }

        /// <summary>
        /// WGS84 转 GCJ02
        /// GPS坐标 转 火星坐标
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static YGpsLocation WGS84ToGCJ02(YGpsLocation location)
        {
            var result = WGS84ToGCJ02(location.Longitude, location.Latitude);
            return new YGpsLocation(result[0], result[1]);
        }

        /// <summary>
        /// WGS84 转 GCJ02
        /// GPS坐标 转 火星坐标
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static double[] WGS84ToGCJ02(double lng, double lat)
        {
            if (IsOutOfChina(lng, lat))
            {
                return new[] { lng, lat };
            }

            var dLat = TransformLat(lng - 105.0, lat - 35.0);
            var dLng = TransformLng(lng - 105.0, lat - 35.0);
            var radLat = lat / 180.0 * Pi;
            var magic = Math.Sin(radLat);
            magic = 1 - Ee * magic * magic;
            var sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((A * (1 - Ee)) / (magic * sqrtMagic) * Pi);
            dLng = (dLng * 180.0) / (A / sqrtMagic * Math.Cos(radLat) * Pi);
            var mgLat = lat + dLat;
            var mgLng = lng + dLng;
            return new[] { mgLng, mgLat };
        }

        /// <summary>
        /// GCJ02 转 WGS84
        /// 火星坐标 转 GPS坐标
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static YGpsLocation GCJ02ToWGS84(YGpsLocation location)
        {
            var result = GCJ02ToWGS84(location.Longitude, location.Latitude);
            return new YGpsLocation(result[0], result[1]);
        }

        /// <summary>
        /// GCJ02 转 WGS84
        /// 火星坐标 转 GPS坐标
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static double[] GCJ02ToWGS84(double lng, double lat)
        {
            if (IsOutOfChina(lng, lat))
            {
                return new[] { lng, lat };
            }

            var dLat = TransformLat(lng - 105.0, lat - 35.0);
            var dLng = TransformLng(lng - 105.0, lat - 35.0);
            var radLat = lat / 180.0 * Pi;
            var magic = Math.Sin(radLat);
            magic = 1 - Ee * magic * magic;
            var sqrtMagic = Math.Sqrt(magic);
            dLat = (dLat * 180.0) / ((A * (1 - Ee)) / (magic * sqrtMagic) * Pi);
            dLng = (dLng * 180.0) / (A / sqrtMagic * Math.Cos(radLat) * Pi);
            var mgLat = lat + dLat;
            var mgLng = lng + dLng;
            return new[] { lng * 2 - mgLng, lat * 2 - mgLat };
        }

        private static double TransformLat(double lng, double lat)
        {
            var ret = -100.0 + 2.0 * lng + 3.0 * lat + 0.2 * lat * lat + 0.1 * lng * lat + 0.2 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * Pi) + 20.0 * Math.Sin(2.0 * lng * Pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lat * Pi) + 40.0 * Math.Sin(lat / 3.0 * Pi)) * 2.0 / 3.0;
            ret += (160.0 * Math.Sin(lat / 12.0 * Pi) + 320 * Math.Sin(lat * Pi / 30.0)) * 2.0 / 3.0;
            return ret;
        }

        private static double TransformLng(double lng, double lat)
        {
            var ret = 300.0 + lng + 2.0 * lat + 0.1 * lng * lng + 0.1 * lng * lat + 0.1 * Math.Sqrt(Math.Abs(lng));
            ret += (20.0 * Math.Sin(6.0 * lng * Pi) + 20.0 * Math.Sin(2.0 * lng * Pi)) * 2.0 / 3.0;
            ret += (20.0 * Math.Sin(lng * Pi) + 40.0 * Math.Sin(lng / 3.0 * Pi)) * 2.0 / 3.0;
            ret += (150.0 * Math.Sin(lng / 12.0 * Pi) + 300.0 * Math.Sin(lng / 30.0 * Pi)) * 2.0 / 3.0;
            return ret;
        }
    }
}
