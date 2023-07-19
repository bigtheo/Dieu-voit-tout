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
    public partial class FrmMainPage : Form
    {
        private readonly Color defaultButtonBg = Color.FromArgb(245, 245, 245);
        private readonly Color activeButtonBg = Color.FromArgb(220, 220, 220);
        private Form activeForm;

        public FrmMainPage()
        {
            InitializeComponent();
        }


        private void OuvrirFormulaire(Form formulaireEnfant)
        {

            if (activeForm != null)
            {
                activeForm.Close();
            }
            activeForm = formulaireEnfant;
            formulaireEnfant.TopLevel = false;
            formulaireEnfant.FormBorderStyle = FormBorderStyle.None;
            formulaireEnfant.Dock = DockStyle.Fill;
            panelContainer.Controls.Add(formulaireEnfant);
            panelContainer.Tag = formulaireEnfant;
            formulaireEnfant.BringToFront();
            formulaireEnfant.Show();


        }

        private void BtnArticle_Click(object sender, EventArgs e)
        {

            BtnArticle.BackColor=activeButtonBg;
            OuvrirFormulaire(new FrmArticle());
            //others bouton

            BtnStock.BackColor=defaultButtonBg ;
            BtnVente.BackColor=defaultButtonBg;            
            BtnRapports.BackColor=defaultButtonBg;
        }

        private void BtnStock_Click(object sender, EventArgs e)
        {

           BtnStock.BackColor = activeButtonBg;
            OuvrirFormulaire(new FrmStock());
            //others bouton

            BtnArticle.BackColor = defaultButtonBg;
            BtnVente.BackColor = defaultButtonBg;
            BtnRapports.BackColor = defaultButtonBg;
        }

        private void BtnVente_Click(object sender, EventArgs e)
        {

            BtnVente.BackColor = activeButtonBg;

            OuvrirFormulaire(new FrmVente());

            //others bouton

            BtnArticle.BackColor = defaultButtonBg;
            BtnStock.BackColor = defaultButtonBg;
            BtnRapports.BackColor = defaultButtonBg;
        }

        private void BtnRapports_Click(object sender, EventArgs e)
        {
            BtnRapports.BackColor = activeButtonBg;
            OuvrirFormulaire(new FrmRapports());
            //others bouton

            BtnArticle.BackColor = defaultButtonBg;
            BtnStock.BackColor = defaultButtonBg;
            BtnVente.BackColor = defaultButtonBg;
        }

        private void FrmMainPage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
