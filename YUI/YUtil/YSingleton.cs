using System;
using System.Threading;

namespace YUI.WPF.YUtil
{
    /// <summary>
    /// 单例模式
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public static class YSingleton<TItem> where TItem : class, new()
    {
        private static TItem _instance;

        /// <summary>
        /// 获取单例模式
        /// </summary>
        /// <returns></returns>
        public static TItem GetInstance()
        {
            if (_instance == null)
                Interlocked.CompareExchange(ref _instance, Activator.CreateInstance<TItem>(), default(TItem));

            return _instance;
        }
    }
}
