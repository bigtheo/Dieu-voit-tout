using MySqlConnector;
using Helper.Helper;
using Syncfusion.Windows.Forms.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Helper.Core.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }


        public bool Insert()
        {
            var sql = "Insert into users(name,password,IsAdmin) values(@p_name,@p_password,@p_IsAdmin)";
            using (MySqlCommand cmd =new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_name = new MySqlParameter("@p_name", MySqlDbType.VarChar)
                {
                    Value = this.Name
                };

                MySqlParameter p_passord = new MySqlParameter("@p_password", MySqlDbType.VarChar)
                {
                    Value = this.Password
                };

                MySqlParameter p_IsAdmin = new MySqlParameter("@p_IsAdmin", MySqlDbType.Bool)
                {
                    Value = this.IsAdmin
                };
                cmd.Parameters.Add(p_IsAdmin);
                cmd.Parameters.Add(p_name);
                cmd.Parameters.Add(p_passord);


                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1062:
                            MessageBox.Show("Cet utilisateur existe déjà ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                            break;
                        default:
                            MessageBox.Show(ex.Message);
                            break;
                    }
                    return false;
                }
            }
        }

    }
}
