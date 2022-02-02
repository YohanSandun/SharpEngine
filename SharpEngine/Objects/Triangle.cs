using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using SharpEngine.Engine;

namespace SharpEngine.Objects
{
    class Triangle
    {
        public Vector Position { get; set; }
        public Vector[] Vertices { get; set; }
        public Vector[] RenderPoints { get; set; }

        protected Core core;

        private Color color;

        public Vector Rotation = Vector.Zero;

        public Vector[] WorldPoints { get; set; }
        public float AverageZ { get; set; }
        public float NormalZ { get; set; }

        public Triangle(Core core, Vector position, Vector v1, Vector v2, Vector v3, Color color)
        {
            Position = position;
            this.core = core;
            this.color = color;

            Vertices = new Vector[3];
            Vertices[0] = v1;
            Vertices[1] = v2;
            Vertices[2] = v3;

            RenderPoints = new Vector[3];
            WorldPoints = new Vector[3];
        }

        public void Render(byte[] buffer)
        {
            /*
            GraphicsPath gp = new GraphicsPath();
            gp.AddLine(
                RenderPoints[0].X, RenderPoints[0].Y,
                RenderPoints[1].X, RenderPoints[1].Y
                );
            gp.AddLine(
                RenderPoints[1].X, RenderPoints[1].Y,
                RenderPoints[2].X, RenderPoints[2].Y
                );
            gp.CloseFigure();

            try
            {
                graphics.FillPath(brush, gp);
            }
            catch { }
            */
            Vector temp;
            if (RenderPoints[0].Y > RenderPoints[1].Y)
            {
                temp = RenderPoints[0];
                RenderPoints[0] = RenderPoints[1];
                RenderPoints[1] = temp;
            }
            if (RenderPoints[0].Y > RenderPoints[2].Y)
            {
                temp = RenderPoints[0];
                RenderPoints[0] = RenderPoints[2];
                RenderPoints[2] = temp;
            }
            if (RenderPoints[1].Y > RenderPoints[2].Y)
            {
                temp = RenderPoints[1];
                RenderPoints[1] = RenderPoints[2];
                RenderPoints[2] = temp;
            }

            if (RenderPoints[0].Y < RenderPoints[1].Y)
            {
                float slope1 = (RenderPoints[1].X - RenderPoints[0].X) / (RenderPoints[1].Y - RenderPoints[0].Y);
                float slope2 = (RenderPoints[2].X - RenderPoints[0].X) / (RenderPoints[2].Y - RenderPoints[0].Y);
                int lines = (int)(RenderPoints[1].Y - RenderPoints[0].Y);
                for (int i = 0; i <= lines; i++)
                {
                    int x1 = (int)(RenderPoints[0].X + i * slope1);
                    int x2 = (int)(RenderPoints[0].X + i * slope2);
                    int y = Math.Abs((int)(RenderPoints[0].Y + i));
                    //if (y < 0 || x1 < 0 || x2 < 0 || y > core.Height || x1 > core.Width || x2 > core.Width)
                    //    continue;
                    if (x1 > x2)
                    {
                        int t = x1;
                        x1 = x2;
                        x2 = t;
                    }
                    for (int j = x1; j <= x2; j++)
                    {
                        int pixel = (core.Width * y + j) * 4;
                        if (pixel < buffer.Length)
                        {
                            buffer[pixel] = color.B;
                            buffer[pixel + 1] = color.G;
                            buffer[pixel + 2] = color.R;
                            buffer[pixel + 3] = 255;
                        }
                    }
                }
            }
            if (RenderPoints[1].Y < RenderPoints[2].Y)
            {
                float slope1 = (RenderPoints[2].X - RenderPoints[1].X) / (RenderPoints[2].Y - RenderPoints[1].Y);
                float slope2 = (RenderPoints[2].X - RenderPoints[0].X) / (RenderPoints[2].Y - RenderPoints[0].Y);
                float sx = RenderPoints[2].X - (RenderPoints[2].Y - RenderPoints[1].Y) * slope2;
                int lines = (int)(RenderPoints[2].Y - RenderPoints[1].Y);
                for (int i = 0; i <= lines; i++)
                {
                    int x1 = (int)(RenderPoints[1].X + i * slope1);
                    int x2 = (int)(sx + i * slope2);
                    int y = Math.Abs((int)(RenderPoints[1].Y + i));
                    //if (y < 0 || x1 < 0 || x2 < 0 || y > core.Height || x1 > core.Width || x2 > core.Width)
                    //    continue;

                    if (x1 > x2)
                    {
                        int t = x1;
                        x1 = x2;
                        x2 = t;
                    }
                    for (int j = x1; j <= x2; j++)
                    {
                        int pixel = (core.Width * y + j) * 4;
                        if (pixel < buffer.Length)
                        {
                            buffer[pixel] = color.B;
                            buffer[pixel + 1] = color.G;
                            buffer[pixel + 2] = color.R;
                            buffer[pixel + 3] = 255;
                        }
                    }
                }
            }
        }

        public void CalculateWorldPosition(Vector position, Vector rotation)
        {
            for (int i = 0; i < WorldPoints.Length; i++)
                WorldPoints[i] = core.Rotate(Vertices[i], rotation);

            for (int i = 0; i < WorldPoints.Length; i++)
                WorldPoints[i] = core.Translate(WorldPoints[i], position);

            AverageZ = (WorldPoints[0].Z + WorldPoints[1].Z + WorldPoints[2].Z) / 3.0f;
        }

        public void CalculateRenderPoints()
        {
            for (int i = 0; i < RenderPoints.Length; i++)
                RenderPoints[i] = core.ApplyPerspective(WorldPoints[i]);

            for (int i = 0; i < RenderPoints.Length; i++)
                RenderPoints[i] = core.CenterScreen(RenderPoints[i]);

            NormalZ = (RenderPoints[1].X - RenderPoints[0].X) * (RenderPoints[2].Y - RenderPoints[0].Y) - (RenderPoints[1].Y - RenderPoints[0].Y) * (RenderPoints[2].X - RenderPoints[0].X);
        }
    }
}
