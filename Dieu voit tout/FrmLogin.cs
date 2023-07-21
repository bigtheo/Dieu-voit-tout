using System;
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

        public static string Login { get; internal set; }

        private void BtnConnection_Click(object sender, EventArgs e)
        {
            Login = txt_username.Text;
            
            if (Connexion.Connecter())
            {
                if (Connexion.IsConnected(Login, txt_password.Text))
                {
                    this.Hide();
                    new FrmMainPage().Show();
                }
                else
                {
                    MessageBox.Show("password/username incorrect.","Error");
                }


                
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
