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
    public partial class FrmStock : Form
    {
        public FrmStock()
        {
            InitializeComponent();
            dgvListe.DataSource = new Stock().GetTable(dtp_date.Value);
        }
    }
}
