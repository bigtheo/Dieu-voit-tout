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
    public partial class FrmVente : Form
    {
        public FrmVente()
        {
            InitializeComponent();
        }

        private void BtnNouveau_Click(object sender, EventArgs e)
        {
            new FrmFacturation().ShowDialog();
        }
    }
}
