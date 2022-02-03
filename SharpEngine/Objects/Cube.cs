using System.Drawing;
using SharpEngine.Engine;

namespace SharpEngine.Objects
{
    class Cube : Object3D
    {
        public Cube(Core core, Vector position, Texture texture) : base(core, position)
        {
            Vertices = new Vector[8];
            Vertices[0] = new Vector(-200, 200, -200);  //0
            Vertices[1] = new Vector(200, 200, -200);   //1
            Vertices[2] = new Vector(-200, -200, -200); //2
            Vertices[3] = new Vector(200, -200, -200);  //3
            Vertices[4] = new Vector(-200, 200, 200);   //4
            Vertices[5] = new Vector(200, 200, 200);    //5
            Vertices[6] = new Vector(-200, -200, 200);  //6
            Vertices[7] = new Vector(200, -200, 200);   //7

            Triangles = new Triangle[12];
            Triangles[0] = new Triangle(core, position, Vertices[0].UV(0, 0), Vertices[1].UV(1, 0), Vertices[3].UV(1, 1), texture);
            Triangles[1] = new Triangle(core, position, Vertices[0].UV(0, 0), Vertices[3].UV(1, 1), Vertices[2].UV(0, 1), texture);

            Triangles[2] = new Triangle(core, position, Vertices[7].UV(1, 1), Vertices[5].UV(1, 0), Vertices[4].UV(0, 0), texture);
            Triangles[3] = new Triangle(core, position, Vertices[7].UV(1, 1), Vertices[4].UV(0, 0), Vertices[6].UV(0, 1), texture);

            Triangles[4] = new Triangle(core, position, Vertices[5].UV(1, 1), Vertices[1].UV(1, 0), Vertices[0].UV(0, 0), texture);
            Triangles[5] = new Triangle(core, position, Vertices[5].UV(1, 1), Vertices[0].UV(0, 0), Vertices[4].UV(0, 1), texture);

            Triangles[6] = new Triangle(core, position, Vertices[2].UV(0, 0), Vertices[3].UV(1, 0), Vertices[7].UV(1, 1), texture);
            Triangles[7] = new Triangle(core, position, Vertices[2].UV(0, 0), Vertices[7].UV(1, 1), Vertices[6].UV(0, 1), texture);

            Triangles[8] = new Triangle(core, position, Vertices[4].UV(1, 0), Vertices[0].UV(0, 0), Vertices[2].UV(0, 1), texture);
            Triangles[9] = new Triangle(core, position, Vertices[4].UV(1, 0), Vertices[2].UV(0, 1), Vertices[6].UV(1, 1), texture);

            Triangles[10] = new Triangle(core, position, Vertices[3].UV(1, 1), Vertices[1].UV(1, 0), Vertices[5].UV(0, 0), texture);
            Triangles[11] = new Triangle(core, position, Vertices[3].UV(1, 1), Vertices[5].UV(0, 0), Vertices[7].UV(0, 1), texture);

            Rotate(Vector.Zero);
        }

    }
}
