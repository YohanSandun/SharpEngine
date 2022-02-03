using System;
using System.Windows.Forms;
using SharpEngine.Engine;
using SharpEngine.Objects;

namespace SharpEngine
{
    public partial class GDIForm : Form
    {
        private Core core;
        //private Object3D obj;
        private float rotation;

        public GDIForm()
        {
            InitializeComponent();
            MouseWheel += GDIForm_MouseWheel;
        }

        private void GDIForm_MouseWheel(object sender, MouseEventArgs e)
        {
            //core.FOV += e.Delta/30;
        }

        private void GDIForm_Load(object sender, EventArgs e)
        {
            core = new Core(Width, Height);
            core.Objects.Add(new Cube(core, new Vector(0,0,1000), new Texture(Resources.wood)));
        }

        private void GDIForm_Paint(object sender, PaintEventArgs e)
        {
            core.Render(e.Graphics);
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            core.Objects[0].Rotate(new Vector(rotation, rotation, 0));
            rotation += 0.02f;
            Invalidate();
        }

        private void GDIForm_Scroll(object sender, ScrollEventArgs e)
        {
            
        }

        private void GDIForm_Resize(object sender, EventArgs e)
        {
            if (core == null)
                return;
            core.Height = this.Height;
            core.Width = this.Width;
            core.Initialize();
        }
    }
}
