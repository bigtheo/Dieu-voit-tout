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
    public class Dépenses
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public decimal Montant { get; set; }
        public long UserId { get; set; }

        public  bool Insert()
        {
            var sql = "Insert into depenses(Designation,montant,user_Id) values(@p_designation,@p_montant,@p_userId)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_designation = new MySqlParameter("@p_designation", MySqlDbType.Text)
                {
                    Value = this.Description
                };

                MySqlParameter p_user_id = new MySqlParameter("@p_userId", MySqlDbType.Int64)
                {
                    Value = this.UserId
                };

                MySqlParameter p_montant = new MySqlParameter("@p_montant", MySqlDbType.Decimal)
                {
                    Value = this.Montant
                };

                cmd.Parameters.Add(p_designation);
                cmd.Parameters.Add(p_montant);
                cmd.Parameters.Add(p_user_id);

                try
                {
                    return cmd.ExecuteNonQuery()>0;
                    
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    return false;
                }
            }
            
        }

        public  bool Update(long id)
        {
            var sql = "Update depenses Designation=@p_designation,montant=@p_montant,user_Id=@p_userId,last_modified=current_timestamp where Id=@p_id";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_designation = new MySqlParameter("@p_designation", MySqlDbType.Text)
                {
                    Value = this.Description
                };

                MySqlParameter p_user_id = new MySqlParameter("@p_userId", MySqlDbType.Int64)
                {
                    Value =this.UserId
                };

                MySqlParameter p_id = new MySqlParameter("@p_Id", MySqlDbType.Int64)
                {
                    Value = id
                };

                cmd.Parameters.Add(p_designation);
                cmd.Parameters.Add(p_user_id);
                cmd.Parameters.Add(p_id);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    return false;
                }
            }

        }

        public bool Delete()
        {
            var sql = "Delete from depenses where id =@p_id";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
             

                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = UserId
                };

                cmd.Parameters.Add(p_id);

                try
                {
                    return cmd.ExecuteNonQuery()>0;

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                    return false;
                }
            }

        }

        public DataTable GetDetailsDepensesTableServiceUN(DateTime date1,DateTime date2)
        {
            var sql = "select Id,Designation,Montant from depenses where date(created_time) >= date(@p_date1) and date(created_time) <= date(@p_date2) and hour(created_time)<17 ";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date = new MySqlParameter("@p_date1", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = date1
                };

                MySqlParameter p_date2 = new MySqlParameter("@p_date2", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = date2
                };

                cmd.Parameters.Add(p_date);
                cmd.Parameters.Add(p_date2);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }  
      
        public decimal GetTotalJournalierDepenseServiceUN(DateTime date1, DateTime date2)
        {
            var sql = "Select ifnull(sum(montant),0) from depenses where date(created_time) >= date(@p_date1) and date(created_time) <= date(@p_date2) And hour(created_time)<17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_1 = new MySqlParameter("@p_date1",MySqlDbType.Date)
                {
                    Value = date1
                };
                MySqlParameter p_date_2 = new MySqlParameter("@p_date2", MySqlDbType.Date)
                {
                    Value = date2
                };
                cmd.Parameters.Add(p_date_1);
                cmd.Parameters.Add(p_date_2);

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

        public DataTable GetDetailsDepensesTableServiceDEUX(DateTime date1, DateTime date2)
        {
            var sql = "select Id,Designation,Montant from depenses where date(created_time) >= date(@p_date1) and date(created_time) <= date(@p_date2) and hour(created_time)>=17 ";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date = new MySqlParameter("@p_date1", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = date1
                };

                MySqlParameter p_date2 = new MySqlParameter("@p_date2", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = date2
                };

                cmd.Parameters.Add(p_date);
                cmd.Parameters.Add(p_date2);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }
      
        public decimal GetTotalJournalierDepenseServiceDEUX(DateTime date1, DateTime date2)
        {
            var sql = "Select ifnull(sum(montant),0) from depenses where date(created_time) >= date(@p_date1) and date(created_time) <= date(@p_date2) And hour(created_time) >=17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_1 = new MySqlParameter("@p_date1", MySqlDbType.Date)
                {
                    Value = date1
                };
                MySqlParameter p_date_2 = new MySqlParameter("@p_date2", MySqlDbType.Date)
                {
                    Value = date2
                };
                cmd.Parameters.Add(p_date_1);
                cmd.Parameters.Add(p_date_2);

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
