using MySqlConnector;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lady_Miriam.Core.Models
{
    public class Accompte
    {
        public long Id  { get; set; }
        public decimal Montant { get; set; }
        public long InvoiceId { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool IsSold { get; internal set; }

        public bool Insert()
        {
            var sql = "Insert into accompte(montant,invoice_id,IsSold) values(@p_montant,@p_invoice_id,@p_IsSold)";
            using (MySqlCommand cmd=new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_montant = new MySqlParameter("@p_montant", MySqlDbType.Decimal)
                {
                    Value = this.Montant
                };

                MySqlParameter p_invoice_id = new MySqlParameter("@p_invoice_id", MySqlDbType.Int64)
                {
                    Value = this.InvoiceId
                };

                MySqlParameter p_IsSold = new MySqlParameter("@p_IsSold", MySqlDbType.Bool)
                {
                    Value = this.IsSold
                };

                cmd.Parameters.Add(p_montant);
                cmd.Parameters.Add(p_invoice_id);
                cmd.Parameters.Add(p_IsSold);

                try
                {

                    return cmd.ExecuteNonQuery()>0;
                }
                catch (MySqlException ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return default;
                }
            }
            

        }

        public decimal GetMontantAccompte()
        {
            var sql = "select ifnull(sum(montant),0) from accompte where invoice_id =@p_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = this.InvoiceId
                            
                };
                cmd.Parameters.Add(p_id);

                try
                {
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }


            }
        }


    }
}
