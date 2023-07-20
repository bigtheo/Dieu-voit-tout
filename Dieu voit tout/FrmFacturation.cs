using Dieu_voit_tout.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dieu_voit_tout
{
    public partial class FrmFacturation : Form
    {
        private object[] rows;
        private  decimal total = 0;

        public static ObservableCollection<InvoiceLine> order_collection = new ObservableCollection<InvoiceLine>();

        public FrmFacturation()
        {
            InitializeComponent();
        }

       
        private void AddProductToInvoice()
        {
            // Create an unbound DataGridView by declaring a column count.
            dgvListe.ColumnCount = 5;
            dgvListe.ColumnHeadersVisible = true;


            // Set the column header names.
            dgvListe.Columns[0].Name = "Code";
            dgvListe.Columns[1].Name = "Designation";
            dgvListe.Columns[2].Name = "Quantité";
            dgvListe.Columns[3].Name = "Prix Unitaire";
            dgvListe.Columns[4].Name = "Prix Total";

            // calculate total price

            decimal prix_total = txt_prix_unitaire.DecimalValue * Convert.ToDecimal(txt_quantite.IntegerValue);

            // get tarif id by name


            // Populate the rows.
            string[] row1 = new string[] { txt_code.Text, txt_designation.Text, txt_quantite.Text, txt_prix_unitaire.DecimalValue.ToString(), prix_total.ToString() };

            rows = new object[] { row1 };

            foreach (string[] rowArray in rows.Cast<string[]>())
            {
                dgvListe.Rows.Add(rowArray);
            }

           
        }

        private void CalculerTotalGeneral()
        {
            // calculate total price

            total = 0;
            foreach (DataGridViewRow row in dgvListe.Rows)
            {
                total += Convert.ToDecimal(row.Cells[4].Value);
            }

            lbl_total_general.Text = total.ToString();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddProductToInvoice();
            CalculerTotalGeneral();
            txt_code.Clear();
            txt_quantite.IntegerValue=1;
            txt_prix_unitaire.Clear();
            txt_code.Focus();


        }

        private void Txt_code_TextChanged(object sender, EventArgs e)
        {
            txt_designation.Text = new Article().GetProductName(txt_code.Text);
            txt_prix_unitaire.Text = new Article().GetProductPrice(txt_code.Text).ToString();

            if (String.IsNullOrEmpty(txt_designation.Text)||String.IsNullOrWhiteSpace(txt_designation.Text))
            {
                BtnAdd.Enabled=false;
            }
            else
            {
                BtnAdd.Enabled=true;
            }
        }

        private void BtnImprimer_Click(object sender, EventArgs e)
        {
            AddInvoiceToCollection();

            var invoiceline = new InvoiceLine();

            //saving the invoices 

            if (invoiceline.IsInserted(order_collection, 2))
            {
                MessageBox.Show("Fature enregistrée avec succès.","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }

            Customer customer = new Customer()
            {
                Name=txt_client.Text,
                Phone = txt_telephone.Text
            };
            customer.IsInserted();
            //printing invoice
            invoiceline.Print(order_collection,2,customer,Convert.ToDecimal(lbl_total_general.Text));

        }
        private void AddInvoiceToCollection()
        {
            order_collection.Clear();

            foreach (DataGridViewRow  row in dgvListe.Rows)
            {
               
                InvoiceLine order = new InvoiceLine()
                {
                    CodeBarre = row.Cells[0].Value.ToString(),
                    Designation= row.Cells[1].Value.ToString(),
                    Amount = Convert.ToInt64(row.Cells[2].Value),
                    NegociatedPrice = Convert.ToDecimal(row.Cells[3].Value),
                    PrixTotal= Convert.ToDecimal(row.Cells[4].Value)
                };
                order_collection.Add(order);
               
            }
            

        }

    }
}
