using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dieu_voit_tout.Common
{
    public class Stock
    {
        public long Id { get; set; }
        public enum Operation { In=0, Out=1}
        public long ArticleId { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;

        public DataTable GetTable(DateTime date)
        {
            var sql = "select s.id,s.code_barre 'Code barre',p.designation,if(s.Operation = 'IN','Entrée','Sortie') 'Opération',s.quantite 'Quantité', s.created_time 'Date et Heure' from stock s inner join product p on p.code_barre=s.code_barre where date(s.created_time)=date(@p_date);";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    MySqlParameter p_date = new MySqlParameter("@p_date", MySqlDbType.Date)
                    {
                        Value = date
                    };
                    cmd.Parameters.Add(p_date);
                    try
                    {
                        DataTable table = new DataTable();

                        da.Fill(table);

                        return table;
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show(ex.Message);
                        return default;
                    }
                }
            }
        }

    }
}
