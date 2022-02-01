using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpEngine
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnLaunch_Click(object sender, EventArgs e)
        {
            if (cmbRenderer.SelectedIndex == 0)
            {
                GDIForm form = new GDIForm();
                form.ShowDialog();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            cmbRenderer.SelectedIndex = 0;
        }
    }
}
