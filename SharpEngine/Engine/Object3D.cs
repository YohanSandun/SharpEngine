using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpEngine.Engine
{
    abstract class Object3D
    {
        protected Core core;
        public Vector Position { get; set; }
        public Vector[] Vertices { get; set; }
        protected Vector[] RenderPoints { get; set; }
        
        public Object3D(Core core, Vector position)
        {
            this.core = core;
            Position = position;
        }

        public abstract void Render(Graphics graphics);
        protected void PrepareForRender()
        {
            for (int i = 0; i < RenderPoints.Length; i++)
                RenderPoints[i] = core.Translate(RenderPoints[i], Position);

            for (int i = 0; i < RenderPoints.Length; i++)
                RenderPoints[i] = core.ApplyPerspective(RenderPoints[i]);

            for (int i = 0; i < RenderPoints.Length; i++)
                RenderPoints[i] = core.CenterScreen(RenderPoints[i]);
        }
        public void Rotate(Vector rotation)
        {
            for (int i = 0; i < RenderPoints.Length; i++)
                RenderPoints[i] = core.Rotate(Vertices[i], rotation);
        }
    }
}
