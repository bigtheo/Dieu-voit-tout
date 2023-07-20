using Dieu_voit_tout;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dieu_voit_tout.Common
{
    public class Invoice
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public long CustomerId { get; set; }

        public long IsInserted()
        {
            string sql = "Insert into invoice(user_id,client_id) values(@p_user_id,@p_customer_id)";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_user_id = new MySqlParameter("@p_user_id", MySqlDbType.Int64)
                {
                    Value = this.UserId
                };

                MySqlParameter p_customer_id = new MySqlParameter("@p_customer_id", MySqlDbType.Int64)
                {
                    Value = this.CustomerId
                };


                cmd.Parameters.Add(p_user_id);
                cmd.Parameters.Add(p_customer_id);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return cmd.LastInsertedId;

                }
                catch (MySqlException ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }
    }
}
