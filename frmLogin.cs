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
    public partial class frmLogin : Form
    {
              

        public frmLogin()
        {
            InitializeComponent();
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
           
        }

        private void progressBarLogo_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (progressBarLogo.Value < 100)
            {
                progressBarLogo.Value++;
            }

            if (progressBarLogo.Value == 100)
            {
                timer1.Enabled = false;
                frmGrilla frmBuscar = new frmGrilla();
                this.Hide();
                frmBuscar.Show();
            }
        }
    }
}
