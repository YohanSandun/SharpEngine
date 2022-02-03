using System;
using System.Drawing;

namespace SharpEngine.Engine
{
    class Texture
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public byte[] PixelData { get; set; }

        public Texture(Image image)
        {
            Bitmap original = new Bitmap(image);
            if (original.PixelFormat != System.Drawing.Imaging.PixelFormat.Format32bppArgb)
            {
                Bitmap clone = new Bitmap(original.Width, original.Height,
                System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (Graphics gr = Graphics.FromImage(clone))
                {
                    gr.DrawImage(original, new Rectangle(0, 0, clone.Width, clone.Height));
                }
                original.Dispose();
                original = clone;
            }
            Width = original.Width;
            Height = original.Height;

            Rectangle rect = new Rectangle(0, 0, original.Width, original.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                original.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                original.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            int bytes = Math.Abs(bmpData.Stride) * original.Height;
            PixelData = new byte[bytes];

            System.Runtime.InteropServices.Marshal.Copy(ptr, PixelData, 0, bytes);

            original.UnlockBits(bmpData);
            image.Dispose();
            original.Dispose();
        }

        public Texture(string fileName) : this(Image.FromFile(fileName))
        {

        }

        public (byte,byte,byte,byte) GetPixel(long x, long y)
        {
            if (x < 0)
                x = 0;
            if (y < 0)
                y = 0;
            if (x >= Width)
                x = Width - 1;
            y = Math.Abs(y);
            if (y >= Height)
                y = Height - 1;
            long pixel = (y * Width + x) * 4;
            return (PixelData[pixel + 3], PixelData[pixel + 2], PixelData[pixel + 1], PixelData[pixel]);
        }
    }
}
