using MySqlConnector;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lady_Miriam.Core.Models
{
    internal class Reduction
    {
        public long Id { get; set; }
        public double Taux { get; set; } 
        public DateTime CreatedTime { get; set; }
        public decimal Montant { get; set; }
        public long UserId { get; set; } = 0;
        public long InvoiceId { get; set; } = 0;

        public bool InsertSimpleReduction()
        {
            var sql = "insert into Reductions(Montant,invoice_id,taux) values(@p_montant,@p_invoice_id,@p_taux)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_rate = new MySqlParameter("@p_taux", MySqlDbType.Double)
                {
                    Value = this.Taux
                };

                MySqlParameter p_montant = new MySqlParameter("@p_montant", MySqlDbType.Int64)
                {
                    Value = this.Montant
                };
                MySqlParameter p_invoice_id = new MySqlParameter("@p_invoice_id", MySqlDbType.Int64)
                {
                    Value = this.InvoiceId
                };

                cmd.Parameters.Add(p_rate);
                cmd.Parameters.Add(p_montant);
                cmd.Parameters.Add(p_invoice_id);

                try
                {
                    return Convert.ToInt64(cmd.ExecuteNonQuery()) > 0;
                }
                catch (MySqlException ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return false;
                }

            }
        }

        public DataTable GetReductionJournalierServiceUNTable(DateTime debut,DateTime fin)
        {
            var sql = "select i.id 'N°', c.name 'Client', c.phone 'Téléphone',r.Montant, r.taux 'Taux de réduction', (r.Montant* r.Taux) 'Montant Réduction' , r.Montant - (r.Montant* r.Taux) Solde \r\nfrom reductions r\r\ninner join invoices i on i.id = r.invoice_id \r\ninner join customers c on c.id = i.customer_id where date(r.created_time) between date(@p_date_debut) and date(@p_date_fin) and hour(r.created_time)<17";
            using (MySqlCommand cmd=new MySqlCommand (sql,Connexion.Con))
            {
                using (MySqlDataAdapter da=new MySqlDataAdapter (cmd))
                {

                    MySqlParameter p_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                    {
                        Value = debut
                    };

                    MySqlParameter p_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                    {
                        Value = fin
                    };

                    cmd.Parameters.Add(p_fin);
                    cmd.Parameters.Add(p_debut);
                    try
                    {
                        DataTable table = new DataTable();

                        da.Fill(table);

                        return table;
                    }
                    catch (MySqlException ex)
                    {

                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        return default;
                    }
                }
            }
            
        }

        public decimal GetTotalApayerJournalierServiceUN(DateTime debut, DateTime fin)
        {
            var sql = "select ifnull(sum(montant),0) Total from reductions where date(created_time) between date(@p_date_debut) and date(@p_date_fin) and hour(created_time)<17";
            using (MySqlCommand cmd= new MySqlCommand(sql,Connexion.Con))
            {

                MySqlParameter p_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };

                cmd.Parameters.Add(p_fin);
                cmd.Parameters.Add(p_debut);
                try
                {
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        public decimal GetTotalReductionJournalierServiceUN(DateTime debut, DateTime fin)
        {
            var sql = "select ifnull(sum(montant*taux),0) Total from reductions where date(created_time) between  date(@p_date_debut) and date(@p_date_fin) and hour(created_time)<17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };

                cmd.Parameters.Add(p_fin);
                cmd.Parameters.Add(p_debut);
                try
                {
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        public void GetCustomerStatus(ref string IsAvailable,ref double rate)
        {
            var sql = "select  rate from customersraterecuctions c where phone = @p_phone";
            using (MySqlCommand cmd=new MySqlCommand(sql,Connexion.Con))
            {
                MySqlParameter p_phone = new MySqlParameter("@p_phone", MySqlDbType.VarChar)
                {
                    Value = this.Montant

                };
                cmd.Parameters.Add(p_phone);

                using (MySqlDataReader reader=cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        IsAvailable = "Oui";
                        rate = reader.GetDouble(0);    
                    }
                }
            }
        }

        public decimal GetTotalApayerJournalierServiceDEUX(DateTime debut, DateTime fin)
        {
            var sql = "select ifnull(sum(montant),0) Total from reductions where date(@p_date_debut) and date(@p_date_fin) and hour(created_time)>=17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                 
                MySqlParameter p_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };

                cmd.Parameters.Add(p_fin);
                cmd.Parameters.Add(p_debut);
                try
                {
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }
       
        public DataTable GetReductionJournalierServiceDEUXTable(DateTime debut, DateTime fin)
        {
            var sql = "select i.id 'N°', c.name 'Client', c.phone 'Téléphone',r.Montant, r.taux 'Taux de réduction', (r.Montant* r.Taux ) 'Montant Réduction' , r.Montant - (r.Montant* r.Taux) Solde \r\nfrom reductions r\r\ninner join invoices i on i.id = r.invoice_id \r\ninner join customers c on c.id = i.customer_id where date(r.created_time) between date(@p_date_debut) and date(@p_date_fin) and hour(r.created_time)>=17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {

                    MySqlParameter p_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                    {
                        Value = debut
                    };

                    MySqlParameter p_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                    {
                        Value = fin
                    };

                    cmd.Parameters.Add(p_fin);
                    cmd.Parameters.Add(p_debut);
                    try
                    {
                        DataTable table = new DataTable();

                        da.Fill(table);

                        return table;
                    }
                    catch (MySqlException ex)
                    {

                        System.Windows.Forms.MessageBox.Show(ex.Message);
                        return default;
                    }
                }
            }

        }

        public decimal GetTotalReductionJournalierServiceDEUX(DateTime debut, DateTime fin)
        {
            var sql = " select ifnull(sum(taux* montant),0) Total from reductions where date(@p_date_debut) and date(@p_date_fin) and hour(created_time)>=17; ";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };

                cmd.Parameters.Add(p_fin);
                cmd.Parameters.Add(p_debut);
                try
                {
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {

                    System.Windows.Forms.MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

       
    }
}
