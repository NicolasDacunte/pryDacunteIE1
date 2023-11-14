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
        public string Usuario;
        public string perfil;

        public frmMenu(string varUsuario, string varPerfil)
        {
            InitializeComponent();
                          
            Usuario = varUsuario;
            perfil = varPerfil;
            if (perfil == "admin")
            {
                btnBuscar.Visible = true;
                btnCargar.Visible = true;
                btnSocios.Visible = true;
                btnModificar.Visible = true;
            }
            else
            {
                btnBuscar.Visible = true;
                btnSocios.Visible = true;
            }

            objLogs = new clsLogs();
            objLogs.ConectarBD();
            
            

            KeyPreview = true;
            this.KeyDown += CerrarFrm_KeyDown;
            this.Usuario = varUsuario;
            this.perfil = varPerfil;
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

            if (perfil == "admin")
            {
                btnBuscar.Visible = true;
                btnCargar.Visible = true;
                btnSocios.Visible = true;
                btnModificar.Visible = true;
            }
            else
            {
                btnBuscar.Visible = true;
                btnSocios.Visible = true;
            }
        }
        //buscar
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            string accion = "Ingresó a Buscar proveedores";
            objLogs.CargarLog(Usuario, fechaHora, accion);
            frmGrilla frmEntrar = new frmGrilla(Usuario,perfil);
            this.Hide();
            frmEntrar.Show();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            string accion = "Ingresó a Cargar un nuevo archivo";
            objLogs.CargarLog(Usuario, fechaHora, accion);
            frmCargar frmEntrar = new frmCargar(Usuario, perfil);
            this.Hide();
            frmEntrar.Show();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            DateTime fechaHora = DateTime.Now;
            string accion = "Ingresó a Modificar archivo";
            objLogs.CargarLog(Usuario, fechaHora, accion);
            frmModificar frmEntrar = new frmModificar(Usuario, perfil);
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


            frmSocios frmEntrar = new frmSocios(Usuario, perfil);
            this.Hide();
            frmEntrar.Show();
        }
    }
}
