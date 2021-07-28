using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Forms
{
    public partial class FormInfo : Form
    {
        private static FormInfo dialogue = null;
        public FormInfo()
        {
            InitializeComponent();
        }

        public static FormInfo GetInstance()
        {
            if (dialogue == null)
            {
                dialogue = new FormInfo();
            }
            return dialogue;
        }

        public void SetInfo(string text)
        {
            rtbInfo.Text = text;
        }
    }
}
