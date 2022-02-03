using System;
using System.ComponentModel;
using System.Windows.Forms;
using SharpEngine.Engine;
using SharpEngine.Objects;
using SharpEngine.ImportExport;

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
            core.Objects.Add(Wavefront.ImportFromFile(core, new Vector(0, 0, 500), new Texture("e:\\car.png"), "e:\\test.obj"));
            core.Objects[0].Scale(new Vector(100, 100, 100));
            //core.Objects.Add(new Cube(core, new Vector(0, 0, 1000), new Texture(Resources.wood)));
        }

        private void GDIForm_Paint(object sender, PaintEventArgs e)
        {
            core.Render(e.Graphics);
        }

        private int fps = 0;
        private long start = 0;
        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            Invalidate();
            fps++;
            if ((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - start >= 1000)
            {
                Text = fps + " fps";
                start = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                fps = 0;
            }
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

        private void tmrAnimator_Tick(object sender, EventArgs e)
        {
            core.Objects[0].Rotate(new Vector(0, rotation, 0));
            rotation += 0.02f;
        }
    }
}
