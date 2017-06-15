using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Timers;

namespace YUI.YUtil
{
    /// <summary>
    /// 网络监控类
    /// </summary>
    public static class YNetworkMonitor
    {
        /// <summary>
        /// 上传速度改变事件
        /// </summary>
        public static event Action<long> SendSpeedChanged;

        /// <summary>
        /// 上传速度改变事件
        /// </summary>
        public static event Action<long> ReceivedSpeedChanged;

        /// <summary>
        /// 网络接口
        /// </summary>
        public static NetworkInterface[] NetworkInterfaces => NetworkInterface.GetAllNetworkInterfaces();


        private static IEnumerable<NetworkInterface> monitorInterfaces;

        /// <summary>
        /// 上传速度
        /// </summary>
        public static long SentSpeed { get; set; }

        /// <summary>
        /// 下载速度
        /// </summary>
        public static long ReceivedSpeed { get; set; }

        /// <summary>
        /// 上传总量
        /// </summary>
        public static long TotalSend { get; set; }

        /// <summary>
        /// 下载总量
        /// </summary>
        public static long TotalReceived { get; set; }

        private static long lastTotalSend;
        private static long lastTotalReceived;

        private static bool IsMonitorAll { get; set; }

        private static Timer timer;

        private static bool IsMonitoring;

        /// <summary>
        /// 开始监控
        /// </summary>
        /// <param name="interfaces">监控的网络接口（为null时监控所有网络接口）</param>
        public static void StartMonitor(IEnumerable<NetworkInterface> interfaces = null)
        {
            if (IsMonitoring)
                StopMonitor();

            monitorInterfaces = interfaces ?? NetworkInterfaces;

            IsMonitorAll = interfaces == null;

            lastTotalSend = 0;
            lastTotalReceived = 0;
            SentSpeed = 0;
            ReceivedSpeed = 0;
            foreach (var networkInterface in monitorInterfaces)
            {
                lastTotalSend += networkInterface.GetIPv4Statistics().BytesSent / 1024;
                lastTotalReceived += networkInterface.GetIPv4Statistics().BytesReceived / 1024;
            }
            TotalSend = lastTotalSend;
            TotalReceived = lastTotalReceived;

            timer = new Timer(1000);
            timer.Elapsed += (sender, args) =>
            {
                if (IsMonitorAll)
                    monitorInterfaces = NetworkInterfaces;

                if (monitorInterfaces == null)
                    return;

                long tempSent = 0;
                long tempReceived = 0;
                foreach (var networkInterface in monitorInterfaces)
                {
                    tempSent += networkInterface.GetIPStatistics().BytesSent / 1024;
                    tempReceived += networkInterface.GetIPStatistics().BytesReceived / 1024;
                }

                lastTotalSend = TotalSend;
                lastTotalReceived = TotalReceived;

                TotalSend = tempSent;
                TotalReceived = tempReceived;

                SentSpeed = TotalSend - lastTotalSend;
                ReceivedSpeed = TotalReceived - lastTotalReceived;

                SendSpeedChanged?.Invoke(SentSpeed);
                ReceivedSpeedChanged?.Invoke(ReceivedSpeed);
            };
            timer.Start();
            IsMonitoring = true;
        }

        /// <summary>
        /// 停止监控
        /// </summary>
        public static void StopMonitor()
        {
            timer.Stop();
            IsMonitoring = false;
        }
    }
}
