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
        }

        public override void Render(Graphics graphics)
        {
            throw new NotImplementedException();
        }
    }
}
