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

namespace pryDacunteIE1
{
    public partial class frmModificar : Form
    {


        public frmModificar()
        {
            InitializeComponent();
        }

        OpenFileDialog ofd = new OpenFileDialog();

        string rutaArchivo;
        
        // Obtiene el directorio actual de la aplicación
        string directorioActual = Application.StartupPath;              
                    
        

        private void btnSeleccionarArchivo_Click(object sender, EventArgs e)
        {
            
            //Retrocede dos carpetas 
            for (int i = 0; i < 2; i++)
            {
                directorioActual = System.IO.Directory.GetParent(directorioActual).FullName;
            }
            //al D.A lo guia hasta la carpeta "Resources" y luego a la "Proveedores"
            directorioActual = System.IO.Path.Combine(directorioActual, "Resources");//,"Proveedores");
            //marca donde se inicializa el OpenFileDialog
            ofd.InitialDirectory = directorioActual;
            //filtro para solo carpetas
            ofd.Filter = "Carpetas|*.folder";
            rutaArchivo = string.Empty;
            //para seleccionar el archivo
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                rutaArchivo = ofd.FileName;
            }

            btnEliminar.Enabled = true;
        }


        //borrar archivo seleccionado
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (File.Exists(rutaArchivo))
            {
                File.Delete(rutaArchivo);
            }
            btnEliminar.Enabled = false;
        }
    }
}
