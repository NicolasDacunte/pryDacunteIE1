using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDacunteIE1
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (progressBarLogo.Value < 100)
            {
                progressBarLogo.Value++;
            }

            if (progressBarLogo.Value == 100)
            {
                timer1.Enabled = false;
                frmGrilla frmBuscar = new frmGrilla();
                this.Hide();
                frmBuscar.Show();
            }
        }
    }
}
