﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Sales_and_Inventory_System__Gadgets_Shop_
{
    public partial class frmSplash : Form
    {
        public frmSplash()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            frmLogin frm = new frmLogin();
            progressBar1.Visible = true;

            this.progressBar1.Value = this.progressBar1.Value + 2;
            if (this.progressBar1.Value == 10)
            {
                label3.Text = "Módulos de leitura..";
            }
            else if (this.progressBar1.Value == 20)
            {
                label3.Text = "Ativando módulos.";
            }
            else if (this.progressBar1.Value == 40)
            {
                label3.Text = "Módulos iniciando..";
            }
            else if (this.progressBar1.Value == 60)
            {
                label3.Text = "Carregando módulos..";
            }
            else if (this.progressBar1.Value == 80)
            {
                label3.Text = "Concluído Carregando módulos..";
            }
            else if (this.progressBar1.Value == 100)
            {
                frm.Show();
                timer1.Enabled = false;
                this.Hide();
            }
        }

        private void frmSplash_Load(object sender, EventArgs e)
        {
            progressBar1.Width = this.Width;
        }
    }
}
