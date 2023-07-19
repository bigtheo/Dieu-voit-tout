using MySqlConnector;
using Helper.Core.Models;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lady_Miriam.Core.Models
{
    public class Abonnes
    {
        public long Id { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public decimal Quotite { get; set; } = 0;


       public bool Insert()
        {
            var sql = "INSERT INTO `abonnes` (`phone`, `quotite`) VALUES (@p_phone, @p_quotite)";
            using (MySqlCommand cmd=new MySqlCommand (sql,Connexion.Con))
            {
                MySqlParameter p_phone = new MySqlParameter("@p_phone", MySqlDbType.VarChar)
                {
                    Value = this.Phone
                };

                MySqlParameter p_quotite = new MySqlParameter("@p_quotite", MySqlDbType.Decimal)
                {
                    Value = this.Quotite
                };

                cmd.Parameters.Add(p_quotite);
                cmd.Parameters.Add(p_phone);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1452:
                            Client client = new Client()
                            {
                                Name =this.Name,
                                Phone = this.Phone
                            };

                            client.Insert();
                            this.Insert();
                            return true;
                           
                        case 1062:
                            this.AddQuotite();
                            return true;
                            
                        default:
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                            return false;
                            
                    }
                   
                }
            }
        }

        private bool AddQuotite()
        {
            var sql = "UPDATE `abonnes` set `quotite`=quotite + @p_quotite where phone=@p_phone";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_phone = new MySqlParameter("@p_phone", MySqlDbType.VarChar)
                {
                    Value = this.Phone
                };

                MySqlParameter p_quotite = new MySqlParameter("@p_quotite", MySqlDbType.Decimal)
                {
                    Value = this.Quotite
                };

                cmd.Parameters.Add(p_quotite);
                cmd.Parameters.Add(p_phone);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
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
