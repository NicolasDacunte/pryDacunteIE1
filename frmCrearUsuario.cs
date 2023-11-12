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
    public partial class frmCrearUsuario : Form
    {
        clsInicio objInicio;
        public frmCrearUsuario()
        {
            InitializeComponent();
            objInicio = new clsInicio();
            objInicio.ConectarBD();

        }

        private void frmCrearUsuario_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'eL_CLUBDataSet.Usuarios' Puede moverla o quitarla según sea necesario.
            this.usuariosTableAdapter.Fill(this.eL_CLUBDataSet.Usuarios);

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            
            // Obtener el objeto DataRowView seleccionado
            DataRowView selectedDataRowView = cmbPerfil.SelectedItem as DataRowView;

            if (selectedDataRowView != null)
            {
                if (txtUsuario.Text != "" && txtContraseña.Text != "")
                {
                    // Acceder al valor de la columna deseada (en este caso, "perfil
                    string valorSeleccionado = selectedDataRowView["Perfil"].ToString();
                    objInicio.CrearUsuario(txtUsuario.Text, txtContraseña.Text, valorSeleccionado);
                    txtUsuario.Focus();
                    txtUsuario.Text = "";
                    txtContraseña.Text = "";
                }
                else
                {
                    MessageBox.Show("NECESITA COMPLETAR LOS CAMPOS ´USUARIO´ Y ´CONTRASEÑA´");
                }
            }
        }
    }
}
