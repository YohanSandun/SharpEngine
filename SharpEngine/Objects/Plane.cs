using System;
using System.Drawing;
using SharpEngine.Engine;

namespace SharpEngine.Objects
{
    class Plane : Object3D
    {
        public Plane(Core core, Vector position) : base(core, position)
        {
            Vertices = new Vector[4];
            Vertices[0] = new Vector(-100, 100, 0);
            Vertices[1] = new Vector(100, 100, 0);
            Vertices[2] = new Vector(-100, -100, 0);
            Vertices[3] = new Vector(100, -100, 0);

            RenderPoints = new Vector[4];
            Rotate(Vector.Zero);
        }

        public override void Render(Graphics graphics)
        {
            PrepareForRender();

            graphics.DrawLine(Pens.Black, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[1].X, RenderPoints[1].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[2].X, RenderPoints[2].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[1].X, RenderPoints[1].Y, RenderPoints[3].X, RenderPoints[3].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[2].X, RenderPoints[2].Y, RenderPoints[3].X, RenderPoints[3].Y);
        }
    }
}
