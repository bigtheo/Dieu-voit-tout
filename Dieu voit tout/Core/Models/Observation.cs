using MySqlConnector;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lady_Miriam.Core.Models
{
    internal class Observation
    {

        public long Id { get; set; }
        public String Observations { get; set; }

        public bool Insert()
        {
            string sql = "Insert into Obervations(observation) values(@p_observation)";
            using (MySqlCommand cmd=new MySqlCommand (sql,Connexion.Con))
            {
                MySqlParameter p_obs = new MySqlParameter("@p_observation", MySqlDbType.Text)
                {
                    Value = this.Observations
                };
                cmd.Parameters.Add(p_obs);

                return Convert.ToInt32(cmd.ExecuteNonQuery()) > 0;
            }
        }

        public DataTable GetObsertionJournalieres()
        {
            var sql = " select id,Observation,created_time 'Date et heure' from Obervations where date(created_time)=date(now()) ";

            using (MySqlCommand cmd=new MySqlCommand(sql,Connexion.Con))
            {
                using (MySqlDataAdapter da=new MySqlDataAdapter (cmd))
                {
                    DataTable table = new DataTable();
                    da.Fill(table);

                    return table;
                }

            }
        }

    }
}
