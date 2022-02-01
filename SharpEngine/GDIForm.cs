using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SharpEngine.Engine;

namespace SharpEngine
{
    public partial class GDIForm : Form
    {
        private Core core;

        public GDIForm()
        {
            InitializeComponent();
        }

        private void GDIForm_Load(object sender, EventArgs e)
        {
            core = new Core(Width, Height);
        }

        private void GDIForm_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
