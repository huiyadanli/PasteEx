using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace PasteEx.Forms
{
    public class WaterfallFlowPanel : Panel
    {
        private WaterfallFlowLayout layoutEngine;

        public WaterfallFlowPanel()
        {

        }

        public override LayoutEngine LayoutEngine
        {
            get
            {
                if (layoutEngine == null)
                {
                    layoutEngine = new WaterfallFlowLayout();
                }

                return layoutEngine;
            }
        }
    }

    public class WaterfallFlowLayout : LayoutEngine
    {
        public static readonly int width = 100; //px

        public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
        {
            Control parent = container as Control;
            // Use DisplayRectangle so that parent.Padding is honored.
            Rectangle parentDisplayRectangle = parent.DisplayRectangle;
            Point nextControlLocation = parentDisplayRectangle.Location;

            int columnNum = Convert.ToInt32(Math.Floor((parentDisplayRectangle.Width) * 1.0 / width));
            if(columnNum <=0 )
            {
                columnNum = 1;
            }
            Point[] columnPoint = new Point[columnNum];
            for (int j = 0; j < columnPoint.Length; j++)
            {
                columnPoint[j].X = j * width;
                columnPoint[j].Y = 0;
            }

            foreach (Control c in parent.Controls)
            {
                // Only apply layout to visible controls.
                if (!c.Visible)
                {
                    continue;
                }

                // Find the smallest height of the column.
                int minColumnIndex = 0;
                int minHeight = 0;
                for (int j = 0; j< columnPoint.Length; j++)
                {
                    if(j == 0)
                    {
                        minHeight = columnPoint[j].Y;
                    }
                    else if (columnPoint[j].Y < minHeight)
                    {
                        minHeight = columnPoint[j].Y;
                        minColumnIndex = j;
                    }
                }
                c.Location = columnPoint[minColumnIndex];
                columnPoint[minColumnIndex].Y += c.Height;
            }

            // Set panel's height
            int maxHeight = 0;
            for (int j = 0; j < columnPoint.Length; j++)
            {
                if (columnPoint[j].Y > maxHeight)
                {
                    maxHeight = columnPoint[j].Y;
                }
            }
            parent.Height = maxHeight;

            return false;
        }
    }
}
