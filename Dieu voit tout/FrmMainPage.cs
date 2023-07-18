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
    public partial class FrmMainPage : Form
    {
        private Color defaultButtonBg = Color.FromArgb(245, 245, 245);
        private Color activeButtonBg = Color.FromArgb(220, 220, 220);
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

            //others bouton

            BtnStock.BackColor=defaultButtonBg ;
            BtnVente.BackColor=defaultButtonBg;            
            BtnRapports.BackColor=defaultButtonBg;
        }

        private void BtnStock_Click(object sender, EventArgs e)
        {

           BtnStock.BackColor = activeButtonBg;

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

            //others bouton

            BtnArticle.BackColor = defaultButtonBg;
            BtnStock.BackColor = defaultButtonBg;
            BtnVente.BackColor = defaultButtonBg;
        }
    }
}
