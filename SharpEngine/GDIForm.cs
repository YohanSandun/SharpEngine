using System;
using System.Windows.Forms;
using SharpEngine.Engine;
using SharpEngine.Objects;

namespace SharpEngine
{
    public partial class GDIForm : Form
    {
        private Core core;
        private Object3D obj, gizmo;
        private float rotation;

        public GDIForm()
        {
            InitializeComponent();
        }

        private void GDIForm_Load(object sender, EventArgs e)
        {
            core = new Core(Width, Height);
            obj = new Cube(core, Vector.Zero);
            gizmo = new Gizmo(core, Vector.Zero);
        }

        private void GDIForm_Paint(object sender, PaintEventArgs e)
        {
            obj.Render(e.Graphics);
            gizmo.Render(e.Graphics);
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            obj.Rotate(new Vector(0, rotation, 0));
            //gizmo.Rotate(Vector.Zero);
            rotation += 0.02f;
            Invalidate();
        }

        private void GDIForm_Resize(object sender, EventArgs e)
        {
            if (core == null)
                return;
            core.Height = this.Height;
            core.Width = this.Width;
        }
    }
}
