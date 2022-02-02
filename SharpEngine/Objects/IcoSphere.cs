using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEngine.Engine;

namespace SharpEngine.Objects
{
    class IcoSphere : Object3D
    {
        public IcoSphere(Core core, Vector position) : base(core, position)
        {
            float t = (float)((1.0 + Math.Sqrt(5.0)) / 2.0)*100;

            Vertices = new Vector[12];
            Vertices[0] = new Vector(-100, t, 0);
            Vertices[1] = new Vector(100, t, 0);
            Vertices[2] = new Vector(-100, -t, 0);
            Vertices[3] = new Vector(100, -t, 0);

            Vertices[4] = new Vector(0, -100, t);
            Vertices[5] = new Vector(0, 100, t);
            Vertices[6] = new Vector(0, -100, -t);
            Vertices[7] = new Vector(0, 100, -t);

            Vertices[8] = new Vector(t, 0, -100);
            Vertices[9] = new Vector(t, 0, 100);
            Vertices[10] = new Vector(-t, 0, -100);
            Vertices[11] = new Vector(-t, 0, 100);

            RenderPoints = new Vector[12];
            Rotate(Vector.Zero);
        }

        public override void Render(Graphics graphics)
        {
            PrepareForRender();
            //graphics.DrawLine(Pens.Black, RenderPoints[0].X, RenderPoints[0].Y, RenderPoints[11].X, RenderPoints[11].Y);
            Triangle(graphics, 0, 11, 5);
            Triangle(graphics, 0, 5, 1);
            Triangle(graphics, 0, 1, 7);
            Triangle(graphics, 0, 7, 10);
            Triangle(graphics, 0, 10, 11);

            Triangle(graphics, 1, 5, 9);
            Triangle(graphics, 5, 11, 4);
            Triangle(graphics, 11, 10, 2);
            Triangle(graphics, 10, 7, 6);
            Triangle(graphics, 7, 1, 8);

            Triangle(graphics, 3, 9, 4);
            Triangle(graphics, 3, 4, 2);
            Triangle(graphics, 3, 2, 6);
            Triangle(graphics, 3, 6, 8);
            Triangle(graphics, 3, 8, 9);

            Triangle(graphics, 4, 9, 5);
            Triangle(graphics, 2, 4, 11);
            Triangle(graphics, 6, 2, 10);
            Triangle(graphics, 8, 6, 7);
            Triangle(graphics, 9, 8, 1);
        }

        private void Triangle(Graphics graphics, int i1, int i2, int i3)
        {
            graphics.DrawLine(Pens.Black, 
                RenderPoints[i1].X, RenderPoints[i1].Y, 
                RenderPoints[i2].X, RenderPoints[i2].Y);

            graphics.DrawLine(Pens.Black,
                RenderPoints[i2].X, RenderPoints[i2].Y,
                RenderPoints[i3].X, RenderPoints[i3].Y);

            graphics.DrawLine(Pens.Black,
                RenderPoints[i3].X, RenderPoints[i3].Y,
                RenderPoints[i1].X, RenderPoints[i1].Y);
        }
    }
}
