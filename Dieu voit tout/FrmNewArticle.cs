using Dieu_voit_tout.Common;
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
    public partial class FrmNewArticle : Form
    {
        public FrmNewArticle()
        {
            InitializeComponent();
        }

        private void FrmNewArticle_Load(object sender, EventArgs e)
        {

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Vouvez-vous vraiment enregistrer ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            Article article = new Article()
            {
                Description=txt_designation.Text,
                CodeBarre=txt_code.Text,
                Price=txt_pu.DecimalValue,
                Stock=txt_stock.IntegerValue
            };

            if(result == DialogResult.Yes)
            {
                if (article.AddNewProduct())
                    MessageBox.Show("Article enregistré avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
        }
    }
}
