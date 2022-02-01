using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEngine.Engine;

namespace SharpEngine.Objects
{
    class Gizmo : Object3D
    {
        public Gizmo(Core core, Vector position) : base(core, position)
        {
            Vertices = new Vector[4];
            Vertices[0] = new Vector(0, 0, 0);
            Vertices[1] = new Vector(50, 0, 0);
            Vertices[2] = new Vector(0, 50, 0);
            Vertices[3] = new Vector(0, 0, 50);

            RenderPoints = new Vector[4];
        }

        public override void Render(Graphics graphics)
        {
            Rotate(Vector.Zero);
            PrepareForRender();
            graphics.DrawLine(Pens.Red, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[1].X, RenderPoints[1].Y);
            graphics.DrawLine(Pens.Green, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[2].X, RenderPoints[2].Y);
            graphics.DrawLine(Pens.Blue, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[3].X, RenderPoints[3].Y);
        }
    }
}
