using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
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
    }
}
