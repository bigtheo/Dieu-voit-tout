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
    public class FactureDetailsPressing
    {
        public long Id { get; set; }
        public long TarifId { get; set; } = 0;
        public long InvoicesId { get; set; }
        public long Amount { get; set; }
        public decimal NegociatedPrice { get; set; }
        public DateTime CreatedTime { get; set; }

        public void Insert()
        {
            var sql = "Insert into detailsfacturepressing(tarif_id,invoice_id,amount,negociated_price) values (@p_tarif_id,@p_invoice_id,@p_amount,@p_negociated_price)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_tarif_id = new MySqlParameter("@p_tarif_id", MySqlDbType.Int64)
                {
                    Value = this.TarifId
                };

                MySqlParameter p_invoice_id = new MySqlParameter("@p_invoice_id", MySqlDbType.Int64)
                {
                    Value = this.InvoicesId
                };

                MySqlParameter p_amount = new MySqlParameter("@p_amount", MySqlDbType.Int64)
                {
                    Value = this.Amount
                };

                MySqlParameter p_negociated_price = new MySqlParameter("@p_negociated_price", MySqlDbType.Decimal)
                {
                    Value = this.NegociatedPrice
                };

                cmd.Parameters.Add(p_tarif_id);
                cmd.Parameters.Add(p_invoice_id);
                cmd.Parameters.Add(p_negociated_price);
                cmd.Parameters.Add(p_amount);

                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #region SERVICE 1

        public DataTable GetFacturePressingDetailsTable(long id)
        {
            var sql = "select t.designation, d.amount,negociated_price,d.amount*negociated_price total from detailsfacturepressing d inner join tarifs t on t.id =d.tarif_id  where invoice_id = @p_id";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = id
                };
                cmd.Parameters.Add(p_id);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);


                    return table;
                }
            }
        }

        internal decimal ObtenirTotalAPayerService1(DateTime date1, DateTime date2)
        {
            var sql = "select ifnull(sum(amount*negociated_price),0) 'Total à payer ' from detailsfacturepressing where date(created_time) >= date(@p_date_debut) and date(created_time) <= date(@p_date_fin) and  hour(created_time)<17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = date1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = date2
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);

                return Convert.ToDecimal(cmd.ExecuteScalar());

            }
        }

        internal decimal ObtenirTotalPayeService1(DateTime date1, DateTime date2)
        {
            var sql = "select  a.montant 'Payé' FROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a \r\n inner join detailsfacturepressing d on d.invoice_id= a.i \r\n inner join invoices i on i.id = a.i \r\n inner join customers c on c.id = i.customer_id\r\nwhere date(i.created_time)>=date(@p_date_debut) and date(i.created_time)<=date(@p_date_fin) and hour(i.created_time) < 17 group by a.i";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = date1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = date2
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);
                decimal total = 0;
                using (MySqlDataAdapter da=new MySqlDataAdapter (cmd))
                {
                    DataTable table = new DataTable();
                    da.Fill(table);

                    foreach(DataRow dr in table.Rows)
                    {
                        total = total + Convert.ToDecimal(dr[0]);
                    }

                    return total;
                }

            }
        }

        internal long ObtenirTotalNombreHabillesService1(DateTime date1, DateTime date2)
        {
            var sql = " select count(*) 'Nombre habits'from detailsfacturepressing where date(created_time) >= date(@p_date_debut) and date(created_time) <= date(@p_date_fin) and hour(created_time)<17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = date1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = date2
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);

                return Convert.ToInt64(cmd.ExecuteScalar());

            }
        }
        #endregion

        #region SERVICE 2


        internal decimal ObtenirTotalAPayerService2(DateTime date1, DateTime date2)
        {
            var sql = "select ifnull(sum(amount*negociated_price),0) 'Total à payer ' from detailsfacturepressing where date(created_time) >= date(@p_date_debut) and date(created_time) <= date(@p_date_fin) and hour(created_time)>=17;";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = date1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = date2
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);

                return Convert.ToDecimal(cmd.ExecuteScalar());

            }
        }

        internal decimal ObtenirTotalPayeService2(DateTime date1, DateTime date2)
        {
            var sql = "select  a.montant 'Payé' FROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a \r\n inner join detailsfacturepressing d on d.invoice_id= a.i \r\n inner join invoices i on i.id = a.i \r\n inner join customers c on c.id = i.customer_id\r\nwhere date(i.created_time)>=date(@p_date_debut) and date(i.created_time)<=date(@p_date_fin) and hour(i.created_time) >= 17 group by a.i";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = date1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = date2
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);
                decimal total = 0;
                using (MySqlDataAdapter da=new MySqlDataAdapter (cmd))
                {
                    DataTable table = new DataTable();
                    da.Fill(table);

                    foreach (DataRow dr in table.Rows)
                    {
                        total = total + Convert.ToDecimal(dr[0]);
                    }

                    return total;
                }

            }
        }

        internal long ObtenirTotalNombreHabillesService2(DateTime date1, DateTime date2)
        {
            var sql = " select count(*) 'Nombre habits'from detailsfacturepressing where date(created_time) >= date(@p_date_debut) and date(created_time) <= date(@p_date_fin) and hour(created_time)>=17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = date1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = date2
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);

                return Convert.ToInt64(cmd.ExecuteScalar());

            }
        }
        #endregion
    }
}
