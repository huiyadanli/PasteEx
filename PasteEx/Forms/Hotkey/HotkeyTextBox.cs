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
        private Hotkey Hotkey { get; }

        public bool HasWinKey { get; set; }

        /// <summary>
        /// Creates a new HotkeyControl
        /// </summary>
        public HotkeyTextBox()
        {
            Hotkey = new Hotkey();

            ContextMenu = new ContextMenu(); // Disable right-clicking
            GotFocus += delegate { User32.HideCaret(Handle); };
        }

        /// <summary>
        /// When the hotkey is modified externally, the hotkey string needs to be refreshed.
        /// </summary>
        public void RefreshText()
        {
            Hotkey.Windows = HasWinKey;
            Text = Hotkey.ToString();
        }

        /// <summary>
        /// Update the text in the box to notify the user what combination is currently pressed.
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // Clear the current hotkey
            if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                Hotkey.Reset();
                return;
            }
            else
            {
                Hotkey.Key = e.KeyCode;

                Hotkey.Alt = e.Alt;
                Hotkey.Control = e.Control;
                Hotkey.Shift = e.Shift;
                Hotkey.Windows = HasWinKey;
            }

            Text = Hotkey.ToString();
        }

        /// <summary>
        /// If the current hotkey isn't valid, reset it.
        /// </summary>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (Hotkey.Key == Keys.None)
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
