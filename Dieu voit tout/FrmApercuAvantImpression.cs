using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dieu_voit_tout.Core.Printing
{
    public partial class FrmApercuAvantImpression : Form
    {
        public static string FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "invoices.pdf");

        public FrmApercuAvantImpression()
        {
            InitializeComponent();
            OpenDocument();
        }
        private void OpenDocument()
        {
            pdfViewerControl1.Load(FileName);
        }
    }
}
