using System;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace YUI.WPF.YControls
{
    /// <summary>
    /// YPopup
    /// </summary>
    public class YPopup : Popup
    {
        /// <summary>
        /// Popup打开
        /// </summary>
        /// <param name="e"></param>
        protected override void OnOpened(EventArgs e)
        {
            base.OnOpened(e);

            // 当popup打开时获取焦点
            Child.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }

        /// <summary>
        /// Popup失去输入焦点
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostKeyboardFocus(KeyboardFocusChangedEventArgs e)
        {
            base.OnLostKeyboardFocus(e);

            if (IsKeyboardFocusWithin)
                return;

            if (IsOpen)
                Child.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
        }
    }
}
