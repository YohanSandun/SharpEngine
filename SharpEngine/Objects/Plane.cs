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

            Triangles = new Triangle[2];
            Triangles[0] = new Triangle(core, position, Vertices[0], Vertices[1], Vertices[3], Color.Red);
            Triangles[1] = new Triangle(core, position, Vertices[0], Vertices[3], Vertices[2], Color.Red);
            
            Rotate(Vector.Zero);
        }
    }
}
