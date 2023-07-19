using MySqlConnector;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Helper.Core.Models
{
    public class FactureDetailsBoutique
    {
        public long Id { get; set; }
        public long ProductId { get; set; } = 0;
        public long InvoicesId { get; set; }
        public long Amount { get; set; }
        public decimal NegociatedPrice { get; set; }
        public DateTime CreatedTime { get; set; }

        public void Insert()
        {
            var sql = "Insert into detailsfactureboutique(product_id,invoice_id,amount,negociated_price) values (@p_product_id,@p_invoice_id,@p_amount,@p_negociated_price)";
            using (MySqlCommand cmd=new MySqlCommand (sql,Connexion.Con))
            {
                MySqlParameter p_p_product_id = new MySqlParameter("@p_product_id", MySqlDbType.Int64)
                {
                    Value = this.ProductId
                };

                MySqlParameter p_invoice_id = new MySqlParameter("@p_invoice_id", MySqlDbType.Int64)
                {
                    Value = this.InvoicesId
                };

                MySqlParameter p_amount = new MySqlParameter("@p_amount", MySqlDbType.Int64)
                {
                    Value = this.Amount
                };

                MySqlParameter p_negociated_price = new MySqlParameter("@p_negociated_price", MySqlDbType.Decimal)
                {
                    Value = this.NegociatedPrice
                };

                cmd.Parameters.Add(p_p_product_id);
                cmd.Parameters.Add(p_invoice_id);
                cmd.Parameters.Add(p_negociated_price);
                cmd.Parameters.Add(p_amount);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex )
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
