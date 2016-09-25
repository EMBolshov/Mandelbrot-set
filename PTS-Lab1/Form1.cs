using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PTS_Lab1
{
    public partial class Form1 : Form
    {
        MemBitmap memBitmap;
        double xpic, ypic, sizepic;

        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            memBitmap = new MemBitmap(800, 800);
            pictureBox.Image = memBitmap.Bitmap;
            // draw on bitmap
            xpic = 0.0;
            ypic = 0.0;
            sizepic = 4.0;
            DrawIt();
        }

           

        private void DrawIt()
        {
            DoMandelbrot(xpic, ypic, sizepic, 250, memBitmap);
            pictureBox.Invalidate();
            pictureBox.Refresh();
        }

        void DoMandelbrot(double xcenter, double ycenter, double size, int range, MemBitmap mb)
        {
            double left = xcenter - size / 2.0;
            double top = ycenter - size / 2.0;
            double xr = size;
            double yr = size;
            double xs = xr / mb.Width;
            double ys = yr / mb.Height;
            double xc, yc;
            double xsqr, ysqr, x, y;
            int i, j, cnt;
            for (yc = top, j = 0; j < mb.Height; yc += ys, j++)
            {
                for (xc = left, i = 0; i < mb.Width; xc += xs, i++)
                {
                    cnt = 0;
                    x = y = xsqr = ysqr = 0.0;
                    while (cnt < range && xsqr + ysqr < 4.0)
                    {
                        xsqr = x * x;
                        ysqr = y * y;
                        y *= x;
                        y += y + yc;
                        x = xsqr - ysqr + xc;
                        cnt++;
                    }
                  //  cnt *=25;
                    cnt = cnt * 25;
                    memBitmap.SetPixelTranslate(i, j, cnt, range, 0);
                }
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox_MouseDown_1(object sender, MouseEventArgs e)
        {
            // get the new middle of the picture.  This is the point that was
            // clicked on by the mouse.
            double left = xpic - sizepic / 2.0;
            double top = ypic - sizepic / 2.0;
            xpic = sizepic * e.X / memBitmap.Width + left;
            ypic = sizepic * e.Y / memBitmap.Height + top;
            // decrease the size of the picture
            sizepic = sizepic / 1.5;
            DrawIt();
        }
    }
}
