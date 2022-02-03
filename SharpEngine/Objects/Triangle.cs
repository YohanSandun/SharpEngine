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

        public Vector Rotation = Vector.Zero;
        public Vector ScaleVector = Vector.One;

        public Vector[] WorldPoints { get; set; }
        public float AverageZ { get; set; }
        public float NormalZ { get; set; }

        private Texture texture;

        public Triangle(Core core, Vector position, Vector v1, Vector v2, Vector v3, Texture texture)
        {
            Position = position;
            this.core = core;
            this.texture = texture;

            Vertices = new Vector[3];
            Vertices[0] = v1;
            Vertices[1] = v2;
            Vertices[2] = v3;

            RenderPoints = new Vector[3];
            WorldPoints = new Vector[3];
        }

        public void Render(byte[] buffer)
        {
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

                    float us = RenderPoints[0].U + (y - RenderPoints[0].Y) / (RenderPoints[1].Y - RenderPoints[0].Y) * (RenderPoints[1].U - RenderPoints[0].U);
                    float vs = RenderPoints[0].V + (y - RenderPoints[0].Y) / (RenderPoints[1].Y - RenderPoints[0].Y) * (RenderPoints[1].V - RenderPoints[0].V);
                    float ws = RenderPoints[0].W + (y - RenderPoints[0].Y) / (RenderPoints[1].Y - RenderPoints[0].Y) * (RenderPoints[1].W - RenderPoints[0].W);

                    float ue = RenderPoints[0].U + (y - RenderPoints[0].Y) / (RenderPoints[2].Y - RenderPoints[0].Y) * (RenderPoints[2].U - RenderPoints[0].U);
                    float ve = RenderPoints[0].V + (y - RenderPoints[0].Y) / (RenderPoints[2].Y - RenderPoints[0].Y) * (RenderPoints[2].V - RenderPoints[0].V);
                    float we = RenderPoints[0].W + (y - RenderPoints[0].Y) / (RenderPoints[2].Y - RenderPoints[0].Y) * (RenderPoints[2].W - RenderPoints[0].W);

                    if (x1 > x2)
                    {
                        int t = x1;
                        x1 = x2;
                        x2 = t;
                        float t1 = us;
                        us = ue;
                        ue = t1;
                        t1 = vs;
                        vs = ve;
                        ve = t1;
                        t1 = ws;
                        ws = we;
                        we = t1;
                    }
                    if (x2 > x1)
                    {
                        float u = us * texture.Width;
                        float ustep = (ue - us) / (x2 - x1) * texture.Width;
                        float v = vs * texture.Height;
                        float vstep = (ve - vs) / (x2 - x1) * texture.Height;
                        float w = ws;
                        float wstep = (we - ws) / (x2 - x1);

                        for (int j = 0; j <= x2-x1; j++)
                        {
                            int x = x1 + j;
                            u += ustep;
                            v += vstep;
                            w += wstep;

                            int pixel = (core.Width * y + x) * 4;
                            if (pixel < buffer.Length)
                            {
                                (byte, byte, byte, byte) clr = texture.GetPixel((long)(u/w), (long)(v/w));
                                buffer[pixel] = clr.Item4;
                                buffer[pixel + 1] = clr.Item3;
                                buffer[pixel + 2] = clr.Item2;
                                buffer[pixel + 3] = clr.Item1;
                            }
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

                    float us = RenderPoints[1].U + (y - RenderPoints[1].Y) / (RenderPoints[2].Y - RenderPoints[1].Y) * (RenderPoints[2].U - RenderPoints[1].U);
                    float vs = RenderPoints[1].V + (y - RenderPoints[1].Y) / (RenderPoints[2].Y - RenderPoints[1].Y) * (RenderPoints[2].V - RenderPoints[1].V);
                    float ws = RenderPoints[1].W + (y - RenderPoints[1].Y) / (RenderPoints[2].Y - RenderPoints[1].Y) * (RenderPoints[2].W - RenderPoints[1].W);

                    float ue = RenderPoints[0].U + (y - RenderPoints[0].Y) / (RenderPoints[2].Y - RenderPoints[0].Y) * (RenderPoints[2].U - RenderPoints[0].U);
                    float ve = RenderPoints[0].V + (y - RenderPoints[0].Y) / (RenderPoints[2].Y - RenderPoints[0].Y) * (RenderPoints[2].V - RenderPoints[0].V);
                    float we = RenderPoints[0].W + (y - RenderPoints[0].Y) / (RenderPoints[2].Y - RenderPoints[0].Y) * (RenderPoints[2].W - RenderPoints[0].W);

                    if (x1 > x2)
                    {
                        int t = x1;
                        x1 = x2;
                        x2 = t;
                        float t1 = us;
                        us = ue;
                        ue = t1;
                        t1 = vs;
                        vs = ve;
                        ve = t1;
                        t1 = ws;
                        ws = we;
                        we = t1;
                    }
                    if (x2 > x1)
                    {
                        float u = us * texture.Width;
                        float ustep = (ue - us) / (x2 - x1) * texture.Width;
                        float v = vs * texture.Height;
                        float vstep = (ve - vs) / (x2 - x1) * texture.Height;
                        float w = ws;
                        float wstep = (we - ws) / (x2 - x1);

                        for (int j = 0; j <= x2 - x1; j++)
                        {
                            int x = x1 + j;
                            u += ustep;
                            v += vstep;
                            w += wstep;
                            int pixel = (core.Width * y + x) * 4;
                            if (pixel < buffer.Length)
                            {
                                (byte, byte, byte, byte) clr = texture.GetPixel((long)(u/w), (long)(v/w));
                                buffer[pixel] = clr.Item4;
                                buffer[pixel + 1] = clr.Item3;
                                buffer[pixel + 2] = clr.Item2;
                                buffer[pixel + 3] = clr.Item1;
                            }
                        }
                    }
                }
            }
        }

        public void CalculateWorldPosition(Vector position)
        {
            for (int i = 0; i < WorldPoints.Length; i++)
                WorldPoints[i] = core.Scale(Vertices[i], ScaleVector);

            for (int i = 0; i < WorldPoints.Length; i++)
                WorldPoints[i] = core.Rotate(WorldPoints[i], Rotation);

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
