using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryDacunteIE1
{
    public partial class frmInicio : Form
    {
        string varNom = "hola";
        string varContraseña = "123";
        string varNomIng;
        string varContraseñaIng;


        public frmInicio()
        {
            InitializeComponent();
        }

        private void frmInicio_Load(object sender, EventArgs e)
        {

        }

        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
        }

        private void btnAcceder_Click_1(object sender, EventArgs e)
        {
            varNomIng = txtNombre.Text;
            varContraseñaIng = txtContraseña.Text;
            if (varNomIng == varNom && varContraseñaIng == varContraseña)
            {
                frmMenu frmEntrar = new frmMenu();
                this.Hide();
                frmEntrar.Show();
            }
        }
    }
}
