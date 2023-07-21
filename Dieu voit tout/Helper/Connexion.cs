
using MySqlConnector;
using System;
using System.Windows.Forms;

namespace Dieu_voit_tout.Helper
{
    //ouvre la connexion à la base de donnée
    public static class Connexion
    {
        public static MySqlConnection Con;
        
        public static bool OuvrirConnexion()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder()
            {
                Server = System.Configuration.ConfigurationManager.AppSettings["serverName"],
                Password = System.Configuration.ConfigurationManager.AppSettings["password"],
                UserID = System.Configuration.ConfigurationManager.AppSettings["userName"],
                Database = System.Configuration.ConfigurationManager.AppSettings["database"]
            };

            try
            {
                Con = new MySqlConnection(builder.ConnectionString);
                Con.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Echec de connexion au serveur", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        new FrmParametres().ShowDialog();

                        break;

                        case 1045:
                        MessageBox.Show("username or password incorrect", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;

                        case 1049:
                        MessageBox.Show("Base de données inconnue", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        new FrmParametres().ShowDialog();
                        break;

                        default:
                        MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                return false;
            }
        }

        public static long GetUserId(string login,string password)
        {
            OuvrirConnexion();
            var sql = "select ifnull(Id,0) Id from users where name=@p_name and password =sha1(@p_password)";
            using (MySqlCommand cmd=new MySqlCommand(sql,Connexion.Con))
            {
                MySqlParameter p_name = new MySqlParameter("@p_name", MySqlDbType.VarChar)
                {
                    Value = login
                };
                MySqlParameter p_password = new MySqlParameter("@p_password", MySqlDbType.VarChar)
                {
                    Value = password
                };

                cmd.Parameters.Add(p_name);
                cmd.Parameters.Add(p_password);
                try
                {
                    return Convert.ToInt64(cmd.ExecuteScalar());

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }catch(InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message);
                    new FrmParametres().ShowDialog();
                    return 0;
                }
            }
        }

        public static bool IsAdmin(string login,string password)
        {
            OuvrirConnexion();
            var sql = "select ifnull(isAdmin,false) IsAdmin from users where name=@p_name and password =sha1(@p_password)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_name = new MySqlParameter("@p_name", MySqlDbType.VarChar)
                {
                    Value = login
                };
                MySqlParameter p_password = new MySqlParameter("@p_password", MySqlDbType.VarChar)
                {
                    Value = password
                };

                cmd.Parameters.Add(p_name);
                cmd.Parameters.Add(p_password);
                try
                {
                    return Convert.ToBoolean(cmd.ExecuteScalar());

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
        }
    }

}