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

            KeyPreview = true;
            this.KeyDown += CerrarFrm_KeyDown;
        }

        public void CerrarFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit(); // Cierra la aplicación completa
            }
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

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmInicio frmEntrar = new frmInicio();
            this.Hide();
            frmEntrar.Show();
        }

        private void btnSocios_Click(object sender, EventArgs e)
        {
            frmSocios frmEntrar = new frmSocios();
            this.Hide();
            frmEntrar.Show();
        }
    }
}
