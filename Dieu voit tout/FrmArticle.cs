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
    public partial class FrmArticle : Form
    {
        public FrmArticle()
        {
            InitializeComponent();
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            new FrmNewArticle().ShowDialog();
        }

        private void FrmArticle_Load(object sender, EventArgs e)
        {
            Article article = new Article();
            dgvListe.DataSource = article.GetTable();
        }
    }
}
