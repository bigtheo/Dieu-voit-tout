using MySqlConnector;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper.Core.Models
{
    public class Stock
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedTime { get; set; }

        public decimal Amount { get; set; }

        public TypeOperation Operation { get; set; }
        public long ProductId { get; set; } = 0;

        public void Insert()
        {
            var sql = "insert into stock(amount,operation,product_id) values(@p_amount,@p_operation,@p_product_id);";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_amount = new MySqlParameter("@p_amount", MySqlDbType.Int64)
                {
                    Value = this.Amount
                };

                MySqlParameter p_operation = new MySqlParameter("@p_operation", MySqlDbType.Enum)
                {
                    Value = "in"
                };

                MySqlParameter p_product_id = new MySqlParameter("@p_product_id", MySqlDbType.Int64)
                {
                    Value = this.ProductId
                };
                cmd.Parameters.Add(p_amount);
                cmd.Parameters.Add(p_operation);
                cmd.Parameters.Add(p_product_id);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement effectué avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }

        public DataTable GetStockList()
        {
            List<Product> products = new List<Product>();
            var sql = "select p.id,p.code_barre 'Code', p.designation 'Désignation',s.created_time 'Dernière modifcation',\r\nsum(s.amount) 'Quantité Disponible' from stock s\r\ninner join products p on p.id = s.product_id\r\n\r\ngroup by s.product_id";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Helper.Connexion.Con))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
            }
        }

    }


    public enum TypeOperation
    {
        In,
        Out
    }
}
