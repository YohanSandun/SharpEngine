using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpEngine.Objects;

namespace SharpEngine.Engine
{
    class Object3D
    {
        protected Core core;
        public Vector Position { get; set; }
        public Vector[] Vertices { get; set; }
        public Triangle[] Triangles { get; set; }
        public Vector Rotation { get; set; } = Vector.Zero;
        public Vector ScaleVector { get; set; } = Vector.One;
        public Object3D(Core core, Vector position)
        {
            this.core = core;
            Position = position;
        }
        public void Rotate(Vector rotation)
        {
            Rotation = rotation;
            foreach (Triangle triangle in Triangles)
                triangle.Rotation = rotation;
        }

        public void Scale(Vector scale)
        {
            ScaleVector = scale;
            foreach (Triangle triangle in Triangles)
                triangle.ScaleVector = scale;
        }
    }
}
