using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        public static object Login { get; internal set; }

        private void BtnConnection_Click(object sender, EventArgs e)
        {
            if (Connexion.Connecter())
            {
                this.Hide();
                new FrmMainPage().Show();

                
            }
            else
            {
                MessageBox.Show("Echec de connexion");
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
