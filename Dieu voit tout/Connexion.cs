using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dieu_voit_tout
{
    public class Connexion
    {
        public static MySqlConnection Con;
             
        public static bool Connecter()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
            {
                Server = "localhost",
                UserID = "root",
                Password = "1993",
                Database = "DieuVoitTout"

            };


            try
            {
                Con = new MySqlConnection(builder.ConnectionString);

                Con.Open();

                if (Con.State == System.Data.ConnectionState.Open)
                {
                    return true;
                }
                else
                    return false;
            }
            catch (MySqlException ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);

                return default;
            }

            
        }

        public static bool IsConnected(string login, string password)
        {
            var sql = "select ifnull(id,0) from users where name=@p_name and password=sha1(@p_password)";

            using (MySqlCommand cmd=new MySqlCommand(sql,Con))
            {
                MySqlParameter p_name = new MySqlParameter("@p_name", MySqlDbType.VarChar)
                {
                    Value = login
                };
                MySqlParameter p_password = new MySqlParameter("@p_password", MySqlDbType.VarChar)
                {
                    Value = password
                };

                cmd.Parameters.Add(p_password);
                cmd.Parameters.Add(p_name);
                try
                {
                    return Convert.ToInt64(cmd.ExecuteScalar())>0;
                }
                catch (MySqlException ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return false;
                }

            }
        }
        
    }
}
