using System;
using System.Collections.Generic;
using System.Drawing;
using SharpEngine.Objects;

namespace SharpEngine.Engine
{
    class Core
    {
        // Field of view; default 45 degrees

        public Random Random { get; set; }
        public int FOV { get; set; } = 45;
        public float FocalLength { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public List<Object3D> Objects { get; set; } = new List<Object3D>();

        public Bitmap Bmp { get; set; }
        private int bufferSize;
        private byte[] buffer;

        public Core(int width, int height)
        {
            Height = height;
            Width = width;
            Random = new Random(100);
            Initialize();
        }

        public void Initialize()
        {
            CalculateFL();

            Bmp = new Bitmap(Width, Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            Rectangle rect = new Rectangle(0, 0, Bmp.Width, Bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                Bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                Bmp.PixelFormat);

            IntPtr ptr = bmpData.Scan0;
            bufferSize = Math.Abs(bmpData.Stride) * Bmp.Height;

            buffer = new byte[bufferSize];

            System.Runtime.InteropServices.Marshal.Copy(ptr, buffer, 0, bufferSize);

            Bmp.UnlockBits(bmpData);
        }

        private void CalculateFL()
        {
            FocalLength = (float)(Width / 2.0 / Math.Tan(FOV / 2.0 * Math.PI / 180.0));
        }

        public Vector Translate(Vector original, Vector translation)
        {
            return new Vector(
                original.X + translation.X,
                original.Y + translation.Y,
                original.Z + translation.Z,
                original.U,
                original.V,
                original.W
                );
        }

        public Vector Rotate(Vector original, Vector rotation)
        {
            return new Vector(
                 (float)(original.X * (Math.Cos(rotation.Z) * Math.Cos(rotation.Y)) + original.Y * (Math.Cos(rotation.Z) * Math.Sin(rotation.Y) * Math.Sin(rotation.X) - Math.Sin(rotation.Z) * Math.Cos(rotation.X)) + original.Z * (Math.Cos(rotation.Z) * Math.Sin(rotation.Y) * Math.Cos(rotation.X) + Math.Sin(rotation.Z) * Math.Sin(rotation.X))),
                 (float)(original.X * (Math.Sin(rotation.Z) * Math.Cos(rotation.Y)) + original.Y * (Math.Sin(rotation.Z) * Math.Sin(rotation.Y) * Math.Sin(rotation.X) + Math.Cos(rotation.Z) * Math.Cos(rotation.X)) + original.Z * (Math.Sin(rotation.Z) * Math.Sin(rotation.Y) * Math.Cos(rotation.X) - Math.Cos(rotation.Z) * Math.Sin(rotation.X))),
                 (float)(original.X * (-Math.Sin(rotation.Y)) + original.Y * (Math.Cos(rotation.Y) * Math.Sin(rotation.X)) + original.Z * (Math.Cos(rotation.Y) * Math.Cos(rotation.X))),
                 original.U,
                 original.V,
                 original.W
                 );
        }

        public Vector ApplyPerspective(Vector original)
        {
            return new Vector(
                original.X * FocalLength / (FocalLength + original.Z),
                original.Y * FocalLength / (FocalLength + original.Z),
                original.Z,
                original.U * FocalLength / (FocalLength + original.Z),
                original.V * FocalLength / (FocalLength + original.Z),
                original.W * FocalLength / (FocalLength + original.Z)
                );
        }

        public Vector CenterScreen(Vector original)
        {
            return new Vector(
                original.X + Width / 2,
                original.Y + Height / 2,
                original.Z,
                original.U,
                original.V,
                original.W
                ); ;
        }

        public void Render(Graphics graphics)
        {
            List<Triangle> triangles = new List<Triangle>();
            foreach (Object3D obj in Objects)
            {
                foreach (Triangle triangle in obj.Triangles)
                {
                    triangle.CalculateWorldPosition(obj.Position, obj.Rotation);
                    triangles.Add(triangle);
                }
            }

            triangles.Sort((p, q) => q.AverageZ.CompareTo(p.AverageZ));

            // Clear bitmap
            for (int i = 0; i < buffer.Length; i += 4)
            {
                buffer[i] = 0;
                buffer[i + 1] = 0;
                buffer[i + 2] = 0;
                buffer[i + 3] = 255;
            }

            foreach (Triangle triangle in triangles)
            {
                triangle.CalculateRenderPoints();
                if (triangle.NormalZ < 0)
                    triangle.Render(buffer);
            }

            Rectangle rect = new Rectangle(0, 0, Bmp.Width, Bmp.Height);
            System.Drawing.Imaging.BitmapData bmpData =
                Bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                Bmp.PixelFormat);

            System.Runtime.InteropServices.Marshal.Copy(buffer, 0, bmpData.Scan0, bufferSize);

            Bmp.UnlockBits(bmpData);

            graphics.DrawImage(Bmp, 0, 0);

        }
    }
}
