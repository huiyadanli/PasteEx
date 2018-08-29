using System;
using System.Collections;
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

        public Hotkey()
        {
            Reset();
        }

        public Hotkey(string hotkeyStr)
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

        public override string ToString()
        {
            return string.Format("{0}{1}{2}{3}{4}",
                Windows ? "Win+" : string.Empty,
                Control ? "Ctrl+" : string.Empty,
                Shift ? "Shift+" : string.Empty,
                Alt ? "Alt+" :
                String.Empty, Key);
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
