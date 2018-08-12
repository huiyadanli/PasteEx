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
    public partial class FormCollection : Form
    {
        WaterfallFlowPanel panel = new WaterfallFlowPanel();

        public FormCollection()
        {
            InitializeComponent();

            // Hide HorizontalScroll
            splitContainer1.Panel1.HorizontalScroll.Enabled = false;
            splitContainer1.Panel1.HorizontalScroll.Visible = false;
            splitContainer1.Panel1.HorizontalScroll.Maximum = 0;
            splitContainer1.Panel1.AutoScroll = true;

            // Init panel
            splitContainer1.Panel1.Controls.Add(panel);
            panel.Width = splitContainer1.Panel1.Width;
            panel.Height = splitContainer1.Panel1.Height;
            panel.Resize += (sender, e) =>
            {
                splitContainer1.Panel1.VerticalScroll.Value = splitContainer1.Panel1.VerticalScroll.Maximum;
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PictureBox p1 = new PictureBox();
            p1.Image = Image.FromFile(@"D:\Data\我的图片\e13f12c79f3df8dcad5631c3cf11728b4610286d.jpg");
            p1.SizeMode = PictureBoxSizeMode.Zoom;
            p1.Width = 100;
            p1.Height = (int)Math.Round(p1.Width * 1.0 / p1.Image.Width * p1.Image.Height, 0);
            p1.BorderStyle = BorderStyle.FixedSingle;


            panel.Controls.Add(p1);

            PictureBox p2 = new PictureBox();
            p2.Image = Image.FromFile(@"D:\Data\我的图片\8644ebf81a4c510fcc59efa86059252dd52aa5e6.jpg");
            p2.SizeMode = PictureBoxSizeMode.Zoom;
            p2.Width = 100;
            p2.Height = (int)Math.Round(p2.Width * 1.0 / p2.Image.Width * p2.Image.Height, 0);
            p2.BorderStyle = BorderStyle.FixedSingle;

            panel.Controls.Add(p2);
        }

        private void FormCollection_Resize(object sender, EventArgs e)
        {
            panel.Width = splitContainer1.Panel1.Width;
        }
    }
}
