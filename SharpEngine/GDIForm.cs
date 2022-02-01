using System;
using System.Windows.Forms;
using SharpEngine.Engine;
using SharpEngine.Objects;

namespace SharpEngine
{
    public partial class GDIForm : Form
    {
        private Core core;
        private Object3D obj;
        private float rotation;

        public GDIForm()
        {
            InitializeComponent();
        }

        private void GDIForm_Load(object sender, EventArgs e)
        {
            core = new Core(Width, Height);
            obj = new Plane(core, new Vector(0, 0, 200));
        }

        private void GDIForm_Paint(object sender, PaintEventArgs e)
        {
            obj.Render(e.Graphics);
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            obj.Rotate(new Vector(0, rotation, 0));
            rotation += 0.02f;
            Invalidate();
        }
    }
}
