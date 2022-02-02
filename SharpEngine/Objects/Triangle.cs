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

        private Brush brush;

        public Vector Rotation = Vector.Zero;

        public Vector[] WorldPoints { get; set; }
        public float AverageZ { get; set; }
        public float NormalZ { get; set; }

        public Triangle(Core core, Vector position, Vector v1, Vector v2, Vector v3, Brush brush)
        {
            Position = position;
            this.core = core;
            this.brush = brush;

            Vertices = new Vector[3];
            Vertices[0] = v1;
            Vertices[1] = v2;
            Vertices[2] = v3;

            RenderPoints = new Vector[3];
            WorldPoints = new Vector[3];
        }

        public void Render(Graphics graphics)
        {
            //for (int i = 0; i < RenderPoints.Length; i++)
            //    RenderPoints[i] = core.Rotate(Vertices[i], Rotation);

            //for (int i = 0; i < RenderPoints.Length; i++)
            //    RenderPoints[i] = core.Translate(RenderPoints[i], Position);

            //for (int i = 0; i < RenderPoints.Length; i++)
            //    RenderPoints[i] = core.ApplyPerspective(RenderPoints[i]);

            //for (int i = 0; i < RenderPoints.Length; i++)
            //    RenderPoints[i] = core.CenterScreen(RenderPoints[i]);

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
