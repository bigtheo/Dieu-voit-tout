﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dieu_voit_tout
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void BtnConnection_Click(object sender, EventArgs e)
        {
            if (Connexion.Connecter())
            {
                MessageBox.Show("Connexion réussie");
            }
            else
            {
                MessageBox.Show("Echec de connexion");
            }
        }
    }
}
