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
    public class Retrait
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;


        public bool Insert()
        {
            var sql = "Insert into retrait(invoice_id) values(@p_id)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                try
                {
                    MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                    {
                        Value = InvoiceId
                    };
                    cmd.Parameters.Add(p_id);

                     return cmd.ExecuteNonQuery()>0;
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1062:
                            MessageBox.Show("ce colis a été déjà rétiré ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning); ;
                            break;
                        default:
                            MessageBox.Show(ex.Message);
                          break;
                    }
                    
                    return false;
                }
            }
        }
        public DataTable GetRetraitTable()
        {
            var sql = "ps_JournalRetrait";
            using (MySqlCommand cmd=new MySqlCommand (sql,Connexion.Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter p_date = new MySqlParameter("@p_date", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = CreatedTime
                };
                cmd.Parameters.Add(p_date);

               
                    using (MySqlDataAdapter da=new MySqlDataAdapter (cmd))
                    {
                        DataTable table = new DataTable();

                        da.Fill (table);

                        return table;
                    }
               
            }
        }

    }

  
}
