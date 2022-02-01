using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEngine.Engine;

namespace SharpEngine.Objects
{
    class Cube : Object3D
    {
        public Cube(Core core, Vector position) : base(core, position)
        {
            Vertices = new Vector[8];
            Vertices[0] = new Vector(-200, 200, -200);
            Vertices[1] = new Vector(200, 200, -200);
            Vertices[2] = new Vector(-200, -200, -200);
            Vertices[3] = new Vector(200, -200, -200);
            Vertices[4] = new Vector(-200, 200, 200);
            Vertices[5] = new Vector(200, 200, 200);
            Vertices[6] = new Vector(-200, -200, 200);
            Vertices[7] = new Vector(200, -200, 200);

            RenderPoints = new Vector[8];
            Rotate(Vector.Zero);
        }

        public override void Render(Graphics graphics)
        {
            PrepareForRender();

            graphics.DrawLine(Pens.Black, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[1].X, RenderPoints[1].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[2].X, RenderPoints[2].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[1].X, RenderPoints[1].Y, RenderPoints[3].X, RenderPoints[3].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[2].X, RenderPoints[2].Y, RenderPoints[3].X, RenderPoints[3].Y);

            graphics.DrawLine(Pens.Black, RenderPoints[4].X, RenderPoints[4].Y, RenderPoints[5].X, RenderPoints[5].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[4].X, RenderPoints[4].Y, RenderPoints[6].X, RenderPoints[6].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[5].X, RenderPoints[5].Y, RenderPoints[7].X, RenderPoints[7].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[6].X, RenderPoints[6].Y, RenderPoints[7].X, RenderPoints[7].Y);

            graphics.DrawLine(Pens.Black, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[4].X, RenderPoints[4].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[1].X, RenderPoints[1].Y, RenderPoints[5].X, RenderPoints[5].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[2].X, RenderPoints[2].Y, RenderPoints[6].X, RenderPoints[6].Y);
            graphics.DrawLine(Pens.Black, RenderPoints[3].X, RenderPoints[3].Y, RenderPoints[7].X, RenderPoints[7].Y);
        }
    }
}
