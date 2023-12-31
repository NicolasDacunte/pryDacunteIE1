﻿using System;
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
        string usuario;
        string perfil;
        public frmSocios(string varUsuario, string varPerfil)
        {
            InitializeComponent();
            string usuario = varUsuario;
            string perfil = varPerfil;
            KeyPreview = true;
            this.KeyDown += CerrarFrm_KeyDown;
            this.usuario = usuario;
            this.perfil = perfil;
        }

        public void CerrarFrm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Application.Exit(); // Cierra la aplicación completa
            }
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
            frmMenu frmEntrar = new frmMenu(usuario,perfil);
            this.Hide();
            frmEntrar.Show();
        }
        Int32 ID;
        private void button1_Click(object sender, EventArgs e)
        {
            ID = Convert.ToInt32(txtID.Text);
            objBD.ModificarEstado(ID);
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            objBD.TraerDatos(dgv);
            txtID.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
