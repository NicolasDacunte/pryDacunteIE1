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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pryDacunteIE1
{
    public partial class frmInicio : Form
    {
        clsInicio objInicio;


        //string varNom = "hola";
        //string varContraseña = "123";
        string varNomIng;
        string varContraseñaIng;


        public frmInicio()
        {
            InitializeComponent();
            objInicio = new clsInicio();
            objInicio.ConectarBD();


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

        private void frmInicio_Load(object sender, EventArgs e)
        {

        }



        private void txtContraseña_TextChanged(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
        }
        
        private void btnAcceder_Click_1(object sender, EventArgs e)
        {
            /*varNomIng = txtNombre.Text;
            varContraseñaIng = txtContraseña.Text;
            if (varNomIng == varNom && varContraseñaIng == varContraseña)
            {
                frmMenu frmEntrar = new frmMenu();
                this.Hide();
                frmEntrar.Show();
            }*/
            objInicio.IngresarUsuario(txtNombre.Text, txtContraseña.Text, this);
            txtNombre.Text = " ";
            txtContraseña.Text = "";
            txtNombre.Focus();
            
        }

        private void txtContraseña_TextChanged_1(object sender, EventArgs e)
        {
            txtContraseña.UseSystemPasswordChar = true;
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && e.KeyChar == 13)
            {
                txtContraseña.Focus();
                e.Handled = true;
            }
        }

        private void txtContraseña_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter) && e.KeyChar == 13)
            {
                btnAcceder_Click_1(sender, e);
                e.Handled = true;
            }
        }
    }
}
