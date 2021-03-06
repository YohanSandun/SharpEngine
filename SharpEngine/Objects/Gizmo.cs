using System.Drawing;
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
        }
    }
}
