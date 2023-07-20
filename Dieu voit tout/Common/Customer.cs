using Dieu_voit_tout;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dieu_voit_tout.Common
{
    public class Customer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; } = "+243";


        public long IsInserted()
        {
            var sql = "Insert into Customer(name,telephone) values(@p_name,@p_phone)";
            using (MySqlCommand cmd=new MySqlCommand(sql,Connexion.Con))
            {
                MySqlParameter p_name = new MySqlParameter("@p_name", MySqlDbType.VarChar)
                {
                    Value = this.Name
                };

                MySqlParameter p_phone = new MySqlParameter("@p_phone", MySqlDbType.VarChar)
                {
                    Value = this.Phone
                };


                cmd.Parameters.Add(p_name);
                cmd.Parameters.Add(p_phone);

                try
                {
                    int rowsAffected = cmd.ExecuteNonQuery() ;
                    this.Id = cmd.LastInsertedId;
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
