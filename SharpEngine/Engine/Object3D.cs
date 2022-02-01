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
        
        public Object3D(Core core, Vector position)
        {
            this.core = core;
            Position = position;
        }

        public abstract void Render(Graphics graphics);
    }
}
