using System;

namespace SharpEngine.Engine
{
    class Core
    {
        // Field of view; default 45 degrees
        public int FOV { get; set; } = 45;
        public float FocalLength { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Core(int width, int height)
        {
            Width = width;
            Height = height;
            FocalLength = (float)(Width / 2.0 / Math.Tan(FOV / 2.0 * Math.PI / 180.0));
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
                original.X + Width / 2,
                original.Y + Height / 2,
                original.Z
                ); ;
        }
    }
}
