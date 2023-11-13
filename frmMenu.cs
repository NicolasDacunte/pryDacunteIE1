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
        clsLogs objLogs;
        string Usuario;
        

        public frmMenu(string varUsuario)
        {
            InitializeComponent();
                        
            string Usuario = varUsuario;
            objLogs = new clsLogs();
            objLogs.ConectarBD();
            
            

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
            DateTime fechaHora = DateTime.Now;
            string accion = "Ingresó a Buscar proveedores";
            objLogs.CargarLog(Usuario, fechaHora, accion);
            frmGrilla frmEntrar = new frmGrilla(Usuario);
            this.Hide();
            frmEntrar.Show();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            string accion = "Ingresó a Cargar un nuevo archivo";
            objLogs.CargarLog(Usuario, fechaHora, accion);
            frmCargar frmEntrar = new frmCargar(Usuario);
            this.Hide();
            frmEntrar.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            string accion = "Ingresó a Modificar archivo";
            objLogs.CargarLog(Usuario, fechaHora, accion);
            frmModificar frmEntrar = new frmModificar(Usuario);
            this.Hide();
            frmEntrar.Show();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            string accion = "Cerró sesión";
            objLogs.CargarLog(Usuario, fechaHora, accion);
            frmInicio frmEntrar = new frmInicio();
            this.Hide();
            frmEntrar.Show();
        }

        private void btnSocios_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            string accion = "Ingreso a Ver Socios";
            objLogs.CargarLog(Usuario, fechaHora, accion);


            frmSocios frmEntrar = new frmSocios(Usuario);
            this.Hide();
            frmEntrar.Show();
        }
    }
}
