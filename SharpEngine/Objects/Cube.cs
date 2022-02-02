using System.Drawing;
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

            Triangles = new Triangle[12];
            Triangles[0] = new Triangle(core, position, Vertices[0], Vertices[1], Vertices[3], Color.Red);
            Triangles[1] = new Triangle(core, position, Vertices[0], Vertices[3], Vertices[2], Color.Red);

            Triangles[2] = new Triangle(core, position, Vertices[7], Vertices[5], Vertices[4], Color.Green);
            Triangles[3] = new Triangle(core, position, Vertices[7], Vertices[4], Vertices[6], Color.Green);

            Triangles[4] = new Triangle(core, position, Vertices[5], Vertices[1], Vertices[0], Color.Blue);
            Triangles[5] = new Triangle(core, position, Vertices[5], Vertices[0], Vertices[4], Color.Blue);

            Triangles[6] = new Triangle(core, position, Vertices[2], Vertices[3], Vertices[7], Color.Pink);
            Triangles[7] = new Triangle(core, position, Vertices[2], Vertices[7], Vertices[6], Color.Pink);

            Triangles[8] = new Triangle(core, position, Vertices[4], Vertices[0], Vertices[2], Color.Teal);
            Triangles[9] = new Triangle(core, position, Vertices[4], Vertices[2], Vertices[6], Color.Teal);

            Triangles[10] = new Triangle(core, position, Vertices[3], Vertices[1], Vertices[5], Color.Gray);
            Triangles[11] = new Triangle(core, position, Vertices[3], Vertices[5], Vertices[7], Color.Gray);

            Rotate(Vector.Zero);
        }

    }
}
