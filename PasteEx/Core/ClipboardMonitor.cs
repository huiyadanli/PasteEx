using PasteEx.Library;
using System;
using System.Threading;
using System.Windows.Forms;

namespace PasteEx.Core
{
    /// <summary>
    /// reference:
    /// https://stackoverflow.com/questions/621577/clipboard-event-c-sharp
    /// </summary>
    public static class ClipboardMonitor
    {
        public delegate void OnClipboardChangeEventHandler(object sender);
        public static event OnClipboardChangeEventHandler OnClipboardChange;

        public static void Start()
        {
            ClipboardWatcher.Start();
            ClipboardWatcher.OnClipboardChange += OnClipboardChangeEvent;
        }

        public static void Stop()
        {
            ClipboardWatcher.OnClipboardChange -= OnClipboardChangeEvent;
            ClipboardWatcher.Stop();
        }

        private static void OnClipboardChangeEvent(object sender)
        {
            OnClipboardChange?.Invoke(sender);
        }

        /// <summary>
        /// Sent when the contents of the clipboard have changed.
        /// </summary>
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        class ClipboardWatcher : Form
        {
            // static instance of this form
            private static ClipboardWatcher mInstance;

            public delegate void OnClipboardChangeEventHandler(object sender);
            public static event OnClipboardChangeEventHandler OnClipboardChange;

            // start listening
            public static void Start()
            {
                // we can only have one instance if this class
                if (mInstance != null)
                    return;

                Thread t = new Thread(new ParameterizedThreadStart(x => Application.Run(new ClipboardWatcher())));
                t.SetApartmentState(ApartmentState.STA); // give the [STAThread] attribute
                t.Start();
            }

            // stop listening (dispose form)
            public static void Stop()
            {
                if (mInstance == null)
                {
                    return;
                }

                mInstance.Invoke(new MethodInvoker(() =>
                {
                    bool b = User32.RemoveClipboardFormatListener(mInstance.Handle);
                }));
                mInstance.Invoke(new MethodInvoker(mInstance.Close));

                mInstance.Dispose();

                mInstance = null;
            }

            // on load: (hide this window)
            protected override void SetVisibleCore(bool value)
            {
                CreateHandle();

                mInstance = this;

                bool b = User32.AddClipboardFormatListener(mInstance.Handle);

                base.SetVisibleCore(false);
            }

            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                if (m.Msg == WM_CLIPBOARDUPDATE)
                {
                    IntPtr hwnd = User32.GetClipboardOwner();
                    User32.GetWindowThreadProcessId(hwnd, out uint processId);


                    ClipChanged(processId);
                }
            }

            private void ClipChanged(object processId)
            {
                OnClipboardChange?.Invoke(processId);
            }
        }
    }
}
