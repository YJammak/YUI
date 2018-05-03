using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace YUI.YUtil
{
    /// <summary>
    /// 自定义按键枚举
    /// </summary>
    public enum YKeys
    {
        /// <summary>
        /// 
        /// </summary>
        Space = 32,

        /// <summary>
        /// 
        /// </summary>
        Left = 37,

        /// <summary>
        /// 
        /// </summary>
        Up = 38,

        /// <summary>
        /// 
        /// </summary>
        Right = 39,

        /// <summary>
        /// 
        /// </summary>
        Down = 40,

        /// <summary>
        /// 
        /// </summary>
        A = 65,

        /// <summary>
        /// 
        /// </summary>
        B = 66,

        /// <summary>
        /// 
        /// </summary>
        C = 67,

        /// <summary>
        /// 
        /// </summary>
        D = 68,

        /// <summary>
        /// 
        /// </summary>
        E = 69,

        /// <summary>
        /// 
        /// </summary>
        F = 70,

        /// <summary>
        /// 
        /// </summary>
        G = 71,

        /// <summary>
        /// 
        /// </summary>
        H = 72,

        /// <summary>
        /// 
        /// </summary>
        I = 73,

        /// <summary>
        /// 
        /// </summary>
        J = 74,

        /// <summary>
        /// 
        /// </summary>
        K = 75,

        /// <summary>
        /// 
        /// </summary>
        L = 76,

        /// <summary>
        /// 
        /// </summary>
        M = 77,

        /// <summary>
        /// 
        /// </summary>
        N = 78,

        /// <summary>
        /// 
        /// </summary>
        O = 79,

        /// <summary>
        /// 
        /// </summary>
        P = 80,

        /// <summary>
        /// 
        /// </summary>
        Q = 81,

        /// <summary>
        /// 
        /// </summary>
        R = 82,

        /// <summary>
        /// 
        /// </summary>
        S = 83,

        /// <summary>
        /// 
        /// </summary>
        T = 84,

        /// <summary>
        /// 
        /// </summary>
        U = 85,

        /// <summary>
        /// 
        /// </summary>
        V = 86,

        /// <summary>
        /// 
        /// </summary>
        W = 87,

        /// <summary>
        /// 
        /// </summary>
        X = 88,

        /// <summary>
        /// 
        /// </summary>
        Y = 89,

        /// <summary>
        /// 
        /// </summary>
        Z = 90,

        /// <summary>
        /// 
        /// </summary>
        F1 = 112,

        /// <summary>
        /// 
        /// </summary>
        F2 = 113,

        /// <summary>
        /// 
        /// </summary>
        F3 = 114,

        /// <summary>
        /// 
        /// </summary>
        F4 = 115,

        /// <summary>
        /// 
        /// </summary>
        F5 = 116,

        /// <summary>
        /// 
        /// </summary>
        F6 = 117,

        /// <summary>
        /// 
        /// </summary>
        F7 = 118,

        /// <summary>
        /// 
        /// </summary>
        F8 = 119,

        /// <summary>
        /// 
        /// </summary>
        F9 = 120,

        /// <summary>
        /// 
        /// </summary>
        F10 = 121,

        /// <summary>
        /// 
        /// </summary>
        F11 = 122,

        /// <summary>
        /// 
        /// </summary>
        F12 = 123,
    }

    /// <inheritdoc />
    /// <summary>
    /// 热键注册帮助
    /// </summary>
    public sealed class YHotKey : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public static event Action<YHotKey> HotKeyPressed;

        private int Id { get; }

        private bool IsKeyRegistered { get; set; }

        private IntPtr Handle { get; }

        /// <summary>
        /// 
        /// </summary>
        public YKeys Key { get; }

        /// <summary>
        /// 
        /// </summary>
        public ModifierKeys KeyModifier { get; }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="modifierKeys"></param>
        /// <param name="key"></param>
        /// <param name="window"></param>
        public YHotKey(ModifierKeys modifierKeys, YKeys key, Window window)
            : this(modifierKeys, key, new WindowInteropHelper(window))
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        /// <param name="modifierKeys"></param>
        /// <param name="key"></param>
        /// <param name="window"></param>
        public YHotKey(ModifierKeys modifierKeys, YKeys key, WindowInteropHelper window)
            : this(modifierKeys, key, window.Handle)
        {
            if (window == null)
                throw new ArgumentNullException(nameof(window));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modifierKeys"></param>
        /// <param name="key"></param>
        /// <param name="windowHandle"></param>
        public YHotKey(ModifierKeys modifierKeys, YKeys key, IntPtr windowHandle)
        {
            if (modifierKeys == ModifierKeys.None)
                throw new ArgumentNullException(nameof(modifierKeys));

            if (!Enum.IsDefined(typeof(YKeys), key))
                throw new ArgumentNullException(nameof(key));

            if (windowHandle == IntPtr.Zero)
                throw new ArgumentNullException(nameof(windowHandle));

            Key = key;
            KeyModifier = modifierKeys;
            Id = GetHashCode();
            Handle = windowHandle;
            RegisterHotKey();
            ComponentDispatcher.ThreadPreprocessMessage += ThreadPreprocessMessageMethod;
        }

        private void ThreadPreprocessMessageMethod(ref MSG msg, ref bool handled)
        {
            if (handled) return;

            if (msg.message != YHotKeyManager.WM_HOTKEY || (int) (msg.wParam) != Id) return;

            OnHotKeyPressed();
            handled = true;
        }
        private void OnHotKeyPressed()
        {
            HotKeyPressed?.Invoke(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RegisterHotKey()
        {
            if (IsKeyRegistered)
                UnregisterHotKey();

            IsKeyRegistered = YHotKeyManager.RegisterHotKey(Handle, Id, KeyModifier, (int)Key);
            if (!IsKeyRegistered)
                throw new ApplicationException("已存在该热键");
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnregisterHotKey()
        {
            IsKeyRegistered = !YHotKeyManager.UnregisterHotKey(Handle, Id);
        }

        /// <summary>
        /// 
        /// </summary>
        ~YHotKey()
        {
            Dispose();
        }

        /// <inheritdoc />
        /// <summary>
        /// </summary>
        public void Dispose()
        {
            ComponentDispatcher.ThreadPreprocessMessage -= ThreadPreprocessMessageMethod;
            UnregisterHotKey();
        }
    }
}
