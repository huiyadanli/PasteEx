using PasteEx.Util;
using System;
using System.Windows.Forms;

namespace PasteEx.Forms.Hotkey
{
    public class Hotkey
    {
        public bool Alt { get; set; }

        public bool Control { get; set; }

        public bool Shift { get; set; }

        public bool Windows { get; set; }

        private Keys key;
        public Keys Key
        {
            get { return key; }
            set
            {
                if (value != Keys.ControlKey && value != Keys.Alt
                    && value != Keys.Menu && value != Keys.ShiftKey)
                {
                    key = value;
                }
                else
                {
                    key = Keys.None;
                }
            }
        }

        public ModifierKeys ModifierKey
        {
            get
            {
                return
                    (Windows ? ModifierKeys.Win : 0) |
                    (Control ? ModifierKeys.Control : 0) |
                    (Shift ? ModifierKeys.Shift : 0) |
                    (Alt ? ModifierKeys.Alt : 0);
            }
        }

        public Hotkey()
        {
            Reset();
        }

        public Hotkey(string hotkeyStr)
        {
            try
            {
                string[] keyStrs = hotkeyStr.Replace(" ", "").Split('+');
                foreach (string keyStr in keyStrs)
                {
                    string k = keyStr.ToLower();
                    if (k == "win")
                        Windows = true;
                    else if (k == "ctrl")
                        Control = true;
                    else if (k == "shift")
                        Shift = true;
                    else if (k == "alt")
                        Alt = true;
                    else
                        Key = (Keys)Enum.Parse(typeof(Keys), keyStr);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(Resources.Strings.TipHotkeyInvalid + " - " + hotkeyStr + Environment.NewLine + ex.Message);
                throw new ArgumentException(Resources.Strings.TipHotkeyInvalid);
            }
        }

        public override string ToString()
        {
            string str = String.Empty;
            if (Key != Keys.None)
            {
                str = string.Format("{0}{1}{2}{3}{4}",
                    Windows ? "Win + " : String.Empty,
                    Control ? "Ctrl + " : String.Empty,
                    Shift ? "Shift + " : String.Empty,
                    Alt ? "Alt + " : String.Empty,
                    Key);
            }
            return str;
        }

        public void Reset()
        {
            Alt = false;
            Control = false;
            Shift = false;
            Windows = false;
            Key = Keys.None;
        }

    }
}
