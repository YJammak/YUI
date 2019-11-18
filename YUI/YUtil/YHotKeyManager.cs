using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Input;

namespace YUI.WPF.YUtil
{
    /// <summary>
    /// 热键管理器
    /// </summary>
    internal static class YHotKeyManager
    {
        /// <summary>
        /// 热键消息
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        // ReSharper disable once InconsistentNaming
        public const int WM_HOTKEY = 0x312;

        /// <summary>
        /// 注册热键
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        [HandleProcessCorruptedStateExceptions]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, ModifierKeys fsModifuers, int vk);

        /// <summary>
        /// 注销热键
        /// </summary>
        [DllImport("user32.dll", SetLastError = true)]
        [HandleProcessCorruptedStateExceptions]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        /// <summary>
        /// 向原子表中添加全局原子
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        [HandleProcessCorruptedStateExceptions]
        public static extern short GlobalAddAtom(string lpString);

        /// <summary>
        /// 在表中搜索全局原子
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        [HandleProcessCorruptedStateExceptions]
        public static extern short GlobalFindAtom(string lpString);

        /// <summary>
        /// 在表中删除全局原子
        /// </summary>
        [DllImport("kernel32.dll", SetLastError = true)]
        [HandleProcessCorruptedStateExceptions]
        public static extern short GlobalDeleteAtom(string nAtom);
    }
}
