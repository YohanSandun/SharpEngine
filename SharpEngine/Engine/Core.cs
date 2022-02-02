using System;
using System.Collections.Generic;
using System.Drawing;
using SharpEngine.Objects;

namespace SharpEngine.Engine
{
    class Core
    {
        // Field of view; default 45 degrees
        private int fov = 45;

        public Random Random { get; set; }
        public int FOV { get { return fov; } set { fov = value; CalculateFL(); } }
        public float FocalLength { get; set; }

        private int width;
        public int Width { get { return width; } set { width = value; CalculateFL(); } }
        public int Height { get; set; }

        public List<Object3D> Objects { get; set; } = new List<Object3D>();

        public Core(int width, int height)
        {
            Width = width;
            Height = height;
            Random = new Random(100);
        }

        private void CalculateFL()
        {
            FocalLength = (float)(width / 2.0 / Math.Tan(FOV / 2.0 * Math.PI / 180.0));
        }

        public Vector Translate(Vector original, Vector translation)
        {
            return new Vector(
                original.X + translation.X,
                original.Y + translation.Y,
                original.Z + translation.Z
                );
        }

        public Vector Rotate(Vector original, Vector rotation)
        {
            return new Vector(
                 (float)(original.X * (Math.Cos(rotation.Z) * Math.Cos(rotation.Y)) + original.Y * (Math.Cos(rotation.Z) * Math.Sin(rotation.Y) * Math.Sin(rotation.X) - Math.Sin(rotation.Z) * Math.Cos(rotation.X)) + original.Z * (Math.Cos(rotation.Z) * Math.Sin(rotation.Y) * Math.Cos(rotation.X) + Math.Sin(rotation.Z) * Math.Sin(rotation.X))),
                 (float)(original.X * (Math.Sin(rotation.Z) * Math.Cos(rotation.Y)) + original.Y * (Math.Sin(rotation.Z) * Math.Sin(rotation.Y) * Math.Sin(rotation.X) + Math.Cos(rotation.Z) * Math.Cos(rotation.X)) + original.Z * (Math.Sin(rotation.Z) * Math.Sin(rotation.Y) * Math.Cos(rotation.X) - Math.Cos(rotation.Z) * Math.Sin(rotation.X))),
                 (float)(original.X * (-Math.Sin(rotation.Y)) + original.Y * (Math.Cos(rotation.Y) * Math.Sin(rotation.X)) + original.Z * (Math.Cos(rotation.Y) * Math.Cos(rotation.X))));
        }

        public Vector ApplyPerspective(Vector original)
        {
            return new Vector(
                original.X * FocalLength / (FocalLength + original.Z),
                original.Y * FocalLength / (FocalLength + original.Z),
                original.Z);
        }

        public Vector CenterScreen(Vector original)
        {
            return new Vector(
                original.X + width / 2,
                original.Y + Height / 2,
                original.Z
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

            foreach (Triangle triangle in triangles)
            {
                triangle.CalculateRenderPoints();
                if (triangle.NormalZ < 0)
                    triangle.Render(graphics);
            }
        }
    }
}
