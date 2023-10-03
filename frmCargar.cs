using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace pryDacunteIE1
{
    public partial class frmCargar : Form
    {
        public frmCargar()
        {
            InitializeComponent();
        }

       
        
        
        FolderBrowserDialog fbd = new FolderBrowserDialog();
        
        string rutaCarpeta;

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            //busca la carpeta
            fbd.SelectedPath = Application.StartupPath + "\\Proveedores";
            rutaCarpeta = string.Empty;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                rutaCarpeta = fbd.SelectedPath;
                txtNombre.Enabled = true;
                
            }
           
        }
        
        private void frmCargar_Load(object sender, EventArgs e)
        {

        }

        private void btnCargarArchivo_Click(object sender, EventArgs e)
        {
           
            string Linea;
            //crea/abre archivo 
            StreamWriter sw = new StreamWriter(rutaCarpeta + "/" + txtNombre.Text + ".csv", true);
            Linea = txtNro.Text + ";" + txtEntidad.Text + ";" + txtApertura.Text + ";" + txtNroExp.Text + ";" + txtJuzgado.Text + ";" + txtJuridiccion.Text + ";" + txtDireccion.Text + ";" + txtLiquidador.Text + ";";
            sw.WriteLine(Linea);
            sw.Close();
            sw.Dispose();
            MessageBox.Show("Se ha cargado la linea del archivo correctamente");
            //limpia y habilita los txt
            if (txtNombre.Text != "")
            {
                txtNro.Enabled = true;
                txtEntidad.Enabled = true;
                txtApertura.Enabled = true;
                txtNroExp.Enabled = true;
                txtJuzgado.Enabled = true;
                txtJuridiccion.Enabled = true;
                txtDireccion.Enabled = true;
                txtLiquidador.Enabled = true;
                txtNro.Text = "";
                txtEntidad.Text = "";
                txtApertura.Text = "";
                txtNroExp.Text = "";
                txtJuzgado.Text = "";
                txtJuridiccion.Text = "";
                txtDireccion.Text = "";
                txtLiquidador.Text = "";
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenu frmEntrar = new frmMenu();
            this.Hide();
            frmEntrar.Show();
        }
    }
}
