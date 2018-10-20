using PasteEx.Library;
using System;
using System.Collections;
using System.Windows.Forms;

namespace PasteEx.Forms.Hotkey
{
    /// <summary>
    /// A simple control that allows the user to select pretty much any valid hotkey combination
    /// </summary>
    public class HotkeyTextBox : TextBox
    {
        /// <summary>
        /// Forces the control to be non-multiline.
        /// </summary>
        public override bool Multiline { get { return false; } }

        // These variables store the current hotkey and modifier(s)
        private Hotkey hotkey;

        public bool HasWinKey { get; set; }

        /// <summary>
        /// Creates a new HotkeyControl
        /// </summary>
        public HotkeyTextBox()
        {
            hotkey = new Hotkey();

            ContextMenu = new ContextMenu(); // Disable right-clicking
            GotFocus += delegate { User32.HideCaret(Handle); };
        }

        /// <summary>
        /// When the hotkey is modified externally, the hotkey string needs to be refreshed.
        /// </summary>
        public void RefreshText(string hotkeyStr = null)
        {
            if (!string.IsNullOrEmpty(hotkeyStr))
            {
                hotkey = new Hotkey(hotkeyStr);
            }

            hotkey.Windows = HasWinKey;
            Text = hotkey.ToString();
        }

        /// <summary>
        /// Update the text in the box to notify the user what combination is currently pressed.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Clear the current hotkey
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                hotkey.Reset();
                return;
            }
            else
            {
                hotkey.Key = e.KeyCode;

                hotkey.Alt = e.Alt;
                hotkey.Control = e.Control;
                hotkey.Shift = e.Shift;
                hotkey.Windows = HasWinKey;
            }

            Text = hotkey.ToString();
        }

        /// <summary>
        /// If the current hotkey isn't valid, reset it.
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (hotkey.Key == Keys.None)
            {
                Text = "";
            }
        }

        /// <summary>
        /// Prevents the whatever entered to show up in the TextBox
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
