using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SrvsTool.Controls
{
    public class PBLabel : Label
    {
        public Color StartColor { get; set; }
        public Color MiddelColor { get; set; }
        public Color EndColor { get; set; }
        
        public int Percentage { get; set; }

        public PBLabel()
        {
            StartColor = SystemColors.ActiveCaption;
            MiddelColor = SystemColors.Control;
            EndColor = SystemColors.Control;
            Percentage = 0;
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Percentage > 0)
            {
                Color[] blendColorColors = new Color[] { StartColor, MiddelColor, EndColor };
                ColorBlend blendColors = new ColorBlend() { Colors = blendColorColors };
                float[] bPts = new float[] { 0f, 0.5f, 1f };
                blendColors.Positions = bPts;

                LinearGradientBrush GBrush = new LinearGradientBrush(
                    new Point(0, 0),
                    new Point(1 + (this.Width * Percentage) / 100, 0), StartColor, EndColor);
                Rectangle rect = new Rectangle(0, 0, (this.Width * Percentage) / 100, this.Height);
                // Fill with gradient 
                GBrush.InterpolationColors = blendColors;

                e.Graphics.FillRectangle(GBrush, rect);

            }
            // draw text on label

            SolidBrush drawBrush = new SolidBrush(this.ForeColor);
            StringFormat sf = new StringFormat();
            // align with center

            sf.Alignment = StringAlignment.Center;
            // set rectangle bound text

            RectangleF rectF = new
            RectangleF(0, this.Height / 2 - Font.Height / 2, this.Width, this.Height);
            // output string

            e.Graphics.DrawString(this.Text, this.Font, drawBrush, rectF, sf);


        }
    }
}
