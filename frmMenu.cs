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
    public partial class frmMenu : Form
    {
        public frmMenu()
        {
            InitializeComponent();
        }

        private void frmMenu_Load(object sender, EventArgs e)
        {

        }
        //buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmGrilla frmEntrar = new frmGrilla();
            this.Hide();
            frmEntrar.Show();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            frmCargar frmEntrar = new frmCargar();
            this.Hide();
            frmEntrar.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            frmModificar frmEntrar = new frmModificar();
            this.Hide();
            frmEntrar.Show();
        }
    }
}
