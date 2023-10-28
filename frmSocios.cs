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
    public partial class frmSocios : Form
    {
        clsSocios objBD;

        public frmSocios()
        {
            InitializeComponent();
            
        }

        private void frmSocios_Load(object sender, EventArgs e)
        {
            objBD = new clsSocios();
            objBD.ConectarBD();

            lblConectado.Text = objBD.EstadoConexion;
            lblConectado.BackColor = Color.Green;

            objBD.TraerDatos(dgv);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            objBD.BuscarPorApellido(txtBuscar.Text, dgv);
            btnRegresar.Enabled = true;
            btnBuscar.Enabled = false;
            txtBuscar.Enabled = false;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            objBD = new clsSocios();
            objBD.ConectarBD();

            lblConectado.Text = objBD.EstadoConexion;
            lblConectado.BackColor = Color.Green;

            objBD.TraerDatos(dgv);
            btnRegresar.Enabled = false;
            btnBuscar.Enabled = true;
            txtBuscar.Enabled = true;
            txtBuscar.Focus();
            txtBuscar.Text = "";
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenu frmEntrar = new frmMenu();
            this.Hide();
            frmEntrar.Show();
        }
    }
}
