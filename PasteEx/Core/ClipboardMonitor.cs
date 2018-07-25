using PasteEx.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Core
{
    /// <summary>
    /// reference:
    /// https://stackoverflow.com/questions/621577/clipboard-event-c-sharp
    /// </summary>
    public static class ClipboardMonitor
    {
        public delegate void OnClipboardChangeEventHandler(ClipboardFormat format, object data);
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

        private static void OnClipboardChangeEvent(ClipboardFormat format, object data)
        {
            OnClipboardChange?.Invoke(format, data);
        }


        /// <summary>
        /// Sent when the contents of the clipboard have changed.
        /// </summary>
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        class ClipboardWatcher : Form
        {
            // static instance of this form
            private static ClipboardWatcher mInstance;

            public delegate void OnClipboardChangeEventHandler(ClipboardFormat format, object data);
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
                Util.Logger.Info("ManagedThreadId：" + t.ManagedThreadId);
            }

            // stop listening (dispose form)
            public static void Stop()
            {
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
                    ClipChanged();
                }
            }

            static readonly string[] formats = Enum.GetNames(typeof(ClipboardFormat));

            private void ClipChanged()
            {
                IDataObject iData = Clipboard.GetDataObject();

                ClipboardFormat? format = null;

                foreach (var f in formats)
                {
                    if (iData.GetDataPresent(f))
                    {
                        format = (ClipboardFormat)Enum.Parse(typeof(ClipboardFormat), f);
                        break;
                    }
                }

                object data = iData.GetData(format.ToString());

                if (data == null || format == null)
                    return;

                OnClipboardChange?.Invoke((ClipboardFormat)format, data);
            }
        }
    }

    public enum ClipboardFormat : byte
    {
        /// <summary>Specifies the standard ANSI text format. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Text,
        /// <summary>Specifies the standard Windows Unicode text format. This static field
        /// is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        UnicodeText,
        /// <summary>Specifies the Windows device-independent bitmap (DIB) format. This static
        /// field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Dib,
        /// <summary>Specifies a Windows bitmap format. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Bitmap,
        /// <summary>Specifies the Windows enhanced metafile format. This static field is
        /// read-only.</summary>
        /// <filterpriority>1</filterpriority>
        EnhancedMetafile,
        /// <summary>Specifies the Windows metafile format, which Windows Forms does not
        /// directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        MetafilePict,
        /// <summary>Specifies the Windows symbolic link format, which Windows Forms does
        /// not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        SymbolicLink,
        /// <summary>Specifies the Windows Data Interchange Format (DIF), which Windows Forms
        /// does not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Dif,
        /// <summary>Specifies the Tagged Image File Format (TIFF), which Windows Forms does
        /// not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Tiff,
        /// <summary>Specifies the standard Windows original equipment manufacturer (OEM)
        /// text format. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        OemText,
        /// <summary>Specifies the Windows palette format. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Palette,
        /// <summary>Specifies the Windows pen data format, which consists of pen strokes
        /// for handwriting software, Windows Forms does not use this format. This static
        /// field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        PenData,
        /// <summary>Specifies the Resource Interchange File Format (RIFF) audio format,
        /// which Windows Forms does not directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Riff,
        /// <summary>Specifies the wave audio format, which Windows Forms does not directly
        /// use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        WaveAudio,
        /// <summary>Specifies the Windows file drop format, which Windows Forms does not
        /// directly use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        FileDrop,
        /// <summary>Specifies the Windows culture format, which Windows Forms does not directly
        /// use. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Locale,
        /// <summary>Specifies text consisting of HTML data. This static field is read-only.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        Html,
        /// <summary>Specifies text consisting of Rich Text Format (RTF) data. This static
        /// field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Rtf,
        /// <summary>Specifies a comma-separated value (CSV) format, which is a common interchange
        /// format used by spreadsheets. This format is not used directly by Windows Forms.
        /// This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        CommaSeparatedValue,
        /// <summary>Specifies the Windows Forms string class format, which Windows Forms
        /// uses to store string objects. This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        StringFormat,
        /// <summary>Specifies a format that encapsulates any type of Windows Forms object.
        /// This static field is read-only.</summary>
        /// <filterpriority>1</filterpriority>
        Serializable,
    }
}
