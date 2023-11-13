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
using pryDacunteIE1.Properties;
using System.Security.Cryptography;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace pryDacunteIE1
{
    public partial class frmModificar : Form
    {
        string usuario;

        public frmModificar(string varUsuario)
        {
            InitializeComponent();
            string usuario = varUsuario;
            KeyPreview = true;
            this.KeyDown += CerrarFrm_KeyDown;
            this.usuario = usuario;
        }

        public void CerrarFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit(); // Cierra la aplicación completa
            }
        }

        OpenFileDialog ofd = new OpenFileDialog();

        string rutaArchivo;


        // Obtiene el directorio actual de la aplicación
        string directorioActual = Application.StartupPath;

        string nombreArch;
        string leerLinea;
        string[] separarDatos;

        private void btnSeleccionarArchivo_Click(object sender, EventArgs e)
        {
            nombreArch = string.Empty;
            //Retrocede dos carpetas 
            for (int i = 0; i < 2; i++)
            {
                directorioActual = System.IO.Directory.GetParent(directorioActual).FullName;
            }
            //al D.A lo guia hasta la carpeta "Resources" y luego a la "Proveedores"
            directorioActual = System.IO.Path.Combine(directorioActual, "Resources");//,"Proveedores");
            //marca donde se inicializa el OpenFileDialog
            ofd.InitialDirectory = directorioActual;

            //ofd.Filter = "Carpetas|*.folder";
            rutaArchivo = string.Empty;
            //para seleccionar el archivo

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = ofd.FileName;
                nombreArch = ofd.SafeFileName;

                dgv1.Rows.Clear();
                dgv1.Columns.Clear();

                try
                {
                    string Archivo = Convert.ToString(rutaArchivo);
                    string NombreArchivo = nombreArch;

                    StreamReader sr = new StreamReader(Archivo);

                    leerLinea = sr.ReadLine();
                    separarDatos = leerLinea.Split(';');

                    for (int indice = 0; indice < separarDatos.Length; indice++)
                    {
                        dgv1.Columns.Add(separarDatos[indice], separarDatos[indice]);
                    }

                    while (sr.EndOfStream == false)
                    {
                        leerLinea = sr.ReadLine();
                        separarDatos = leerLinea.Split(';');
                        dgv1.Rows.Add(separarDatos);
                    }

                    sr.Close();

                }
                catch (Exception)
                {
                    MessageBox.Show("Seleccione un archivo");
                }

            }
            btnIngresar.Enabled = true;
            txtNro.Enabled = true;
            btnEliminar.Enabled = true;

            lblNomArch.Text = nombreArch;
        }


        //borrar archivo seleccionado
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea borrar este archivo?", "Borrar Archivo", MessageBoxButtons.YesNo);
            if (respuesta == DialogResult.Yes)
            {
                if (File.Exists(rutaArchivo))
                {
                    File.Delete(rutaArchivo);
                }
                btnEliminar.Enabled = false;
                MessageBox.Show("Se ha eliminado el archivo correctamente.");

            }

        }




        private void btnAgregarReg_Click(object sender, EventArgs e)
        {

            StreamWriter sw = new StreamWriter(rutaArchivo, true);

            //como controlo que el numero no este ingresado??

            string linea = txtNro.Text + ";" + txtEntidad.Text + ";" + txtApertura.Text + ";" + txtNroExp.Text + ";" + txtJuzgado.Text + ";" + txtJuridiccion.Text + ";" + txtDireccion.Text + ";" + txtLiquidador.Text;
            sw.WriteLine(linea);
            sw.Close();
            sw.Dispose();
            MessageBox.Show("Nuevo registro agregado con exito.");

            btnAgregarReg.Enabled = false;
            btnEditarReg.Enabled = false;
            btnEditarReg.Enabled = false;
            txtEntidad.Enabled = false;
            txtApertura.Enabled = false;
            txtNroExp.Enabled = false;
            txtJuzgado.Enabled = false;
            txtJuridiccion.Enabled = false;
            txtDireccion.Enabled = false;
            txtLiquidador.Enabled = false;

            dgv1.Rows.Clear();
            dgv1.Columns.Clear();

            try
            {
                string Archivo = Convert.ToString(rutaArchivo);
                string NombreArchivo = nombreArch;

                StreamReader sr = new StreamReader(Archivo);

                leerLinea = sr.ReadLine();
                separarDatos = leerLinea.Split(';');

                for (int indice = 0; indice < separarDatos.Length; indice++)
                {
                    dgv1.Columns.Add(separarDatos[indice], separarDatos[indice]);
                }

                while (sr.EndOfStream == false)
                {
                    leerLinea = sr.ReadLine();
                    separarDatos = leerLinea.Split(';');
                    dgv1.Rows.Add(separarDatos);
                }

                sr.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione un archivo");
            }


            txtNro.Focus();

        }


        string[] parametros = new string[8];

        private void btnEditarReg_Click(object sender, EventArgs e)
        {

            //usuario ingresa el nro de registro a modificar
            string nro = txtNro.Text;

            //Es una lista
            List<string> lineasArchivo = new List<string>();

            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                // Lee la primer linea
                string linea = sr.ReadLine();
                //mientras la linea sea distinto de un valor nulo
                while (linea != null)
                {
                    // Procesa la línea - separa cada campo de un registro
                    parametros = linea.Split(';');

                    //Copia todas las lineas que no coincide con el nro para sobreescribir el archivo sin la linea que quiero borrar
                    if (parametros[0] != nro)
                    {
                        //a la lista le agrega el resto de las lineas
                        lineasArchivo.Add(linea);
                    }
                    // sino si el numero coincide me agrega la linea con los campos editados
                    else
                    {

                        string nuevaLinea = txtNro.Text + ";" + txtEntidad.Text + ";" + txtApertura.Text + ";" + txtNroExp.Text + ";" + txtJuzgado.Text + ";" + txtJuridiccion.Text + ";" + txtDireccion.Text + ";" + txtLiquidador.Text + ";";
                        lineasArchivo.Add(nuevaLinea);
                    }

                    linea = sr.ReadLine();
                }
            }
            //agrega al archivo la lista llamada lineasArchivo
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                foreach (string lineaSingular in lineasArchivo)
                {
                    sw.WriteLine(lineaSingular); // Escribe cada elemento en una línea del archivo
                }
            }

            MessageBox.Show("El registro fue modificado correctamente.");

            btnAgregarReg.Enabled = false;
            btnEditarReg.Enabled = false;
            btnEditarReg.Enabled = false;
            txtEntidad.Enabled = false;
            txtApertura.Enabled = false;
            txtNroExp.Enabled = false;
            txtJuzgado.Enabled = false;
            txtJuridiccion.Enabled = false;
            txtDireccion.Enabled = false;
            txtLiquidador.Enabled = false;

            dgv1.Rows.Clear();
            dgv1.Columns.Clear();

            try
            {
                string Archivo = Convert.ToString(rutaArchivo);
                string NombreArchivo = nombreArch;

                StreamReader sr = new StreamReader(Archivo);

                leerLinea = sr.ReadLine();
                separarDatos = leerLinea.Split(';');

                for (int indice = 0; indice < separarDatos.Length; indice++)
                {
                    dgv1.Columns.Add(separarDatos[indice], separarDatos[indice]);
                }

                while (sr.EndOfStream == false)
                {
                    leerLinea = sr.ReadLine();
                    separarDatos = leerLinea.Split(';');
                    dgv1.Rows.Add(separarDatos);
                }

                sr.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione un archivo");
            }
        }

        

        //BOTON INGRESAR
        private void btnModificar_Click(object sender, EventArgs e)
        {
            
            if (txtNro.Text != "")
            {
                
                txtEntidad.Enabled = true;
                txtApertura.Enabled = true;
                txtNroExp.Enabled = true;
                txtJuzgado.Enabled = true;
                txtJuridiccion.Enabled = true;
                txtDireccion.Enabled = true;
                txtLiquidador.Enabled = true;
                               
                
                
                

            }
            if (!string.IsNullOrEmpty(txtNro.Text))//si lo contrario de nulo o vacio del txtnro
            {
                string nroBuscado = txtNro.Text.Trim(); // Valor a buscar en txtNro
                
                using (StreamReader sr = new StreamReader(rutaArchivo))
                {
                    string linea;

                    while ((linea = sr.ReadLine()) != null)
                    {
                        // Divide la línea en sus campos
                        string[] parametros = linea.Split(';');

                        // Compara el valor en el campo correspondiente (asumiendo que el número de registro está en el índice 0)
                        if (parametros.Length > 0 && parametros[0] == nroBuscado)
                        {
                            // Asigna los valores a las cajas de texto
                            txtEntidad.Text = parametros[1];
                            txtApertura.Text = parametros[2];
                            txtNroExp.Text = parametros[3];
                            txtJuzgado.Text = parametros[4];
                            txtJuridiccion.Text = parametros[5];
                            txtDireccion.Text = parametros[6];
                            txtLiquidador.Text = parametros[7];

                            btnAgregarReg.Enabled = false;
                            btnEditarReg.Enabled = true;
                            btnEliminarReg.Enabled = true;
                            // Termina el bucle ya que se encontró el registro
                            break;
                        }
                        else
                        {
                            //si no existe el registro avisa y limpia los txt

                            txtEntidad.Text = "";
                            txtApertura.Text = "";
                            txtNroExp.Text = "";
                            txtJuzgado.Text = "";
                            txtJuridiccion.Text = "";
                            txtDireccion.Text = "";
                            txtLiquidador.Text = "";
                            

                            btnAgregarReg.Enabled = true;
                            btnEditarReg.Enabled = false;
                            btnEliminarReg.Enabled = false;
                            

                        }
                    }
                }
                
            }                
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            frmMenu frmEntrar = new frmMenu(usuario);
            this.Hide();
            frmEntrar.Show();
        }

        private void frmModificar_Load(object sender, EventArgs e)
        {

        }

        private void btnEliminarReg_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show("¿Está seguro de que desea borrar este registro?", "Borrar Proveedor", MessageBoxButtons.YesNo);

            //usuario ingresa el nro de registro a modificar
            string nro = txtNro.Text;

            //Es una lista
            List<string> lineasArchivo = new List<string>();

            using (StreamReader sr = new StreamReader(rutaArchivo))
            {
                // Lee la primer linea
                string linea = sr.ReadLine();
                //mientras la linea sea distinto de un valor nulo
                while (linea != null)
                {
                    // Procesa la línea - separa cada campo de un registro
                    parametros = linea.Split(';');

                    //Copia todas las lineas que no coincide con el nro para sobreescribir el archivo sin la linea que quiero borrar
                    if (parametros[0] != nro)
                    {
                        //a la lista le agrega el resto de las lineas
                        lineasArchivo.Add(linea);
                    }
                    // sino si el numero coincide me agrega la linea con los campos editados
                    else
                    {
                        lineasArchivo.Remove(linea); //borra la linea que es igual al nro ingresado por txt                        
                        txtEntidad.Text = "";
                        txtApertura.Text = "";
                        txtNroExp.Text = "";
                        txtJuzgado.Text = "";
                        txtJuridiccion.Text = "";
                        txtDireccion.Text = "";
                        txtLiquidador.Text = "";
                        txtEntidad.Enabled = false;
                        txtApertura.Enabled = false;
                        txtNroExp.Enabled = false;
                        txtJuzgado.Enabled = false;
                        txtJuridiccion.Enabled = false;
                        txtDireccion.Enabled = false;
                        txtLiquidador.Enabled = false;

                        btnAgregarReg.Enabled = false;
                        btnEditarReg.Enabled = false;
                        btnEliminarReg.Enabled = false;
                        MessageBox.Show("Se ha eliminado el registro correctamente.");
                    }

                    linea = sr.ReadLine();
                }
            }
            //agrega al archivo la lista llamada lineasArchivo
            using (StreamWriter sw = new StreamWriter(rutaArchivo))
            {
                foreach (string lineaSingular in lineasArchivo)
                {
                    sw.WriteLine(lineaSingular); // Escribe cada elemento en una línea del archivo
                }
            }

            dgv1.Rows.Clear();
            dgv1.Columns.Clear();

            try
            {
                string Archivo = Convert.ToString(rutaArchivo);
                string NombreArchivo = nombreArch;

                StreamReader sr = new StreamReader(Archivo);

                leerLinea = sr.ReadLine();
                separarDatos = leerLinea.Split(';');

                for (int indice = 0; indice < separarDatos.Length; indice++)
                {
                    dgv1.Columns.Add(separarDatos[indice], separarDatos[indice]);
                }

                while (sr.EndOfStream == false)
                {
                    leerLinea = sr.ReadLine();
                    separarDatos = leerLinea.Split(';');
                    dgv1.Rows.Add(separarDatos);
                }

                sr.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Seleccione un archivo");
            }
        }

        private void lblNomArch_Click(object sender, EventArgs e)
        {

        }
    }
    
}
