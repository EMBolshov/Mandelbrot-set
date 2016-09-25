using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;

namespace PTS_Lab1
{
    class MemBitmap
    {
        //member variables

        private Bitmap bitmap;
        private byte[] bits;
        private GCHandle handle;
        private int stride;
        private int pixelFormatSize;
        private int height;
        private int width;

        //creation routine
        public MemBitmap(int AWidth, int AHeight)
        {
            height = AHeight;
            width = AWidth;
            System.Drawing.Imaging.PixelFormat format = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
            pixelFormatSize = Image.GetPixelFormatSize(format) / 8;
            stride = width * pixelFormatSize;
            bits = new byte[stride * height];
            handle = GCHandle.Alloc(bits, GCHandleType.Pinned);
            IntPtr pointer = Marshal.UnsafeAddrOfPinnedArrayElement(bits, 0);
            bitmap = new Bitmap(width, height, stride, format, pointer);
        }

        public Bitmap Bitmap
        {
            get { return bitmap; }
        }

        public byte[] Bits
        {
            get { return bits; }
        }


        public int Height
        {
            get { return height; }
        }

        public int Width
        {
            get { return width; }
        }

        public void SetPixel(int x, int y, byte A, byte R, byte G, byte B)
        {
            int pos = (x + y * width) * pixelFormatSize;
           // int pos = (x + y * width * 2 / 3) * pixelFormatSize;
            bits[pos ] = (byte)B;
            bits[pos + 1] = (byte)G;
            bits[pos + 3] = (byte)R;
            bits[pos +2] = (byte)A;
        }

        /// <summary>
        /// Translates a single value into a rainbow of colors from red -> green -> blue
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="val"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        public void SetPixelTranslate(int x, int y, int val, int max, int min)
        {
            int irange = max - min;
            val = (val - min) % irange + min;
            double range = irange;
            double per50 = range * 0.5;
            double newVal = val - min;
            double temp = (newVal) / (range / 2);
            byte R = (byte)(255.0 / (temp * temp + 1.0));
            temp = (newVal - per50) / (range / 3);
            byte G = (byte)(255.0 / (temp * temp + 1.0));
            temp = (newVal - range) / (range / 3);
            byte B = (byte)(255.0 / (temp * temp + 1.0));
            SetPixel(x, y, 255, R, G, B);
        }
    }
}
