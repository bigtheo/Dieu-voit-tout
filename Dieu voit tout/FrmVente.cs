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
    public partial class FrmVente : Form
    {
        public FrmVente()
        {
            InitializeComponent();
            dgvListe.DataSource = new InvoiceLine().GetTable(dtp_date.Value);
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            new FrmFacturation().ShowDialog();
        }
    }
}
