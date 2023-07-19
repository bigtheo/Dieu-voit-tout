using MySqlConnector;
using Helper.Helper;
using System;
using System.Data;
using System.Windows.Forms;

namespace Helper.Core.Models
{
    public class Facture
    {
        public long Id { get; set; }
        public Client Customer { get; set; }
        public DateTime CreatedTime { get; set; }
        public long CustomerId { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }

        public DateTime Date_debut { get; set; }
        public DateTime Date_fin { get; set; }

        public long Insert()
        {
            var sql = "Insert into invoices(customer_id,description,user_id)  values(@p_customer_id,@p_description,@p_user_id)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_customer_id = new MySqlParameter("@p_customer_id", MySqlDbType.Int64)
                {
                    Value = this.CustomerId
                };

                MySqlParameter p_description = new MySqlParameter("@p_description", MySqlDbType.VarChar)
                {
                    Value = this.Description
                };

                MySqlParameter p_user_id = new MySqlParameter("@p_user_id", MySqlDbType.Int64)
                {
                    Value = this.UserId
                };

                cmd.Parameters.Add(p_customer_id);
                cmd.Parameters.Add(p_description);
                cmd.Parameters.Add(p_user_id);

                try
                {
                    int rowAffected = cmd.ExecuteNonQuery();
                    var factureId = cmd.LastInsertedId;

                    return factureId;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        public DataTable GetFactureBoutiqueTable(DateTime date)
        {
            var sql = "ps_JournalFacturationBoutqiue";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter p_date = new MySqlParameter("@p_date", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = date
                };

                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public DataTable GetFacturePressingTable(DateTime date)
        {
            var sql = "ps_JournalFacturationPressing";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter p_date = new MySqlParameter("@p_date", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = date
                };

                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public DataTable GetFacturePressingTable(long invoice)
        {
            var sql = "ps_JournalFacturationPressingById";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                MySqlParameter p_date = new MySqlParameter("@p_invoice_id", MySqlDbType.Int64)
                {
                    Direction = ParameterDirection.Input,
                    Value = invoice
                };

                cmd.Parameters.Add(p_date);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public decimal GetTotalMontantFacture()
        {
            var sql = "Select ifnull(sum(negociated_price * amount),0) from detailsfacturePressing where invoice_id =@p_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = this.Id
                };
                cmd.Parameters.Add(p_id);

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

        public string GetCustomerName()
        {
            var sql = "Select c.name from customers c  inner join invoices i on i.customer_id = c.id  where i.id =@p_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = this.Id
                };
                cmd.Parameters.Add(p_id);

                try
                {
                    return Convert.ToString(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        #region methode Service UN

       
        public DataTable ObtenirLesFactureJournalieresServiceUn(DateTime d1, DateTime d2)
        {
            var sql = "select  a.i 'N°',c.name 'Nom Client',c.phone 'Télephone',sum(d.amount*d.negociated_price) Total ,a.montant 'Payé', sum(d.amount*d.negociated_price) - a.montant 'Reste'\r\nFROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  inner join detailsfacturepressing d on d.invoice_id= a.i \r\ninner join invoices i on i.id = a.i \r\ninner join customers c on c.id = i.customer_id\r\nwhere date(i.created_time)>=date(@p_date_debut) and date(i.created_time)<=date(@p_date_fin) and hour(i.created_time) < 17\r\ngroup by a.i";
            using (MySqlCommand cmd=new MySqlCommand(sql,Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = d1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = d2
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);

                using (MySqlDataAdapter da=new MySqlDataAdapter (cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }

        }
       
        public DataTable GetDaylyInvoicesServiceUN()
        {
            var sql = "select  a.i 'N°',c.name 'Nom Client',c.phone 'Télephone',sum(d.amount*d.negociated_price) Total ,a.montant 'Payé', sum(d.amount*d.negociated_price) - a.montant 'Reste' FROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  inner join detailsfacturepressing d on d.invoice_id= a.i inner join invoices i on i.id = a.i inner join customers c on c.id = i.customer_id where date(i.created_time) between(@p_date_debut and @p_date_fin) and hour(i.created_time) < 17 group by a.i";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = Date_debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = Date_fin
                };
                cmd.Parameters.Add(p_date);
                cmd.Parameters.Add(p_date_fin);

                try
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        return table;
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }

        public DataTable GetDettesPayéesServiceUN(DateTime debut,DateTime fin)
        {
            var sql = "select i.id Facture, c.name,c.phone 'Date', a.montant from accompte a \r\ninner join invoices i on i.id= a.invoice_id \r\ninner join customers c on c.id =i.customer_id \r\nwhere date(a.created_time)>=date(@p_date_debut)  and date(a.created_time)<=date(@p_date_fin)  and i.IsPayed=true and a.isSold=true and hour(a.created_time)<17 and (i.created_time)< date(a.created_time)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public DataTable GetDettesNONPayéesServiceUN(DateTime debut, DateTime fin)
        { 
            var sql = "select  a.i 'N°',c.name 'Nom Client',c.phone 'Télephone', sum(d.amount*d.negociated_price) - a.montant 'Montant Dettes'\r\nFROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  inner join detailsfacturepressing d on d.invoice_id= a.i \r\ninner join invoices i on i.id = a.i \r\ninner join customers c on c.id = i.customer_id\r\nwhere date(i.created_time) >= date(@p_date_debut) and date(i.created_time) <=date(@p_date_fin) and hour(i.created_time) < 17 and i.IsPayed=false\r\ngroup by a.i";
           
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public decimal GetTotalDettesNonPayeesService1(DateTime debut, DateTime fin)
        {
            var sql = "select ifnull(sum(a.montant),0) Total from accompte a where date(a.created_time)>=date(@p_date_debut) and date(a.created_time)<=date(@p_date_fin)  and a.isSold=false and hour(a.created_time)<17";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = debut
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);
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

        public DataTable GetJournalEcheanceTable(DateTime d1, DateTime d2)
        {
            var sql = "ps_journal_echeance";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = d1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = d2
                };

                MySqlParameter p_index = new MySqlParameter("@p_index", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Input,
                    Value = 2
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_index);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public decimal GetTotalFactureServiceUN(DateTime d1, DateTime d2)
        {
            string sql = "select ifnull(sum(amount * negociated_price),0) 'Total facturé'  from detailsfacturepressing  where date(created_time) between date(@p_date_debut) and date(@p_date_fin) ";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = d1
                };
                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = d2
                };
                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);

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

        public decimal GetTotalDettesPayeesServiceUN(DateTime debut,DateTime fin)
        {
            var sql = "select ifnull(sum(a.montant),0) Total from accompte a inner join invoices i on i.id= a.invoice_id where date(a.created_time)=date(@p_date_debut) and a.isSold=true and hour(a.created_time)< 17 and date(a.created_time)>=date(@p_date_debut) and date(a.created_time)<=date(@p_date_fin) and (i.created_time)< date(a.created_time)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = fin
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);
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

        #endregion methode Service UN


        #region methode Service DEUX


        public DataTable ObtenirLesFactureJournalieresServiceDEUX(DateTime d1, DateTime d2)
        {
            var sql = "select  a.i 'N°',c.name 'Nom Client',c.phone 'Télephone',sum(d.amount*d.negociated_price) Total ,a.montant 'Payé', sum(d.amount*d.negociated_price) - a.montant 'Reste'\r\nFROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  inner join detailsfacturepressing d on d.invoice_id= a.i \r\ninner join invoices i on i.id = a.i \r\ninner join customers c on c.id = i.customer_id\r\nwhere date(i.created_time)>=date(@p_date_debut) and date(i.created_time)<=date(@p_date_fin) and hour(i.created_time) >= 17\r\ngroup by a.i";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = d1
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = d2
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }

        }

        public DataTable GetDaylyInvoicesServiceDEUX()
        {
            var sql = "select  a.i 'N°',c.name 'Nom Client',c.phone 'Télephone',sum(d.amount*d.negociated_price) Total ,a.montant 'Payé', sum(d.amount*d.negociated_price) - a.montant 'Reste' FROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  inner join detailsfacturepressing d on d.invoice_id= a.i inner join invoices i on i.id = a.i inner join customers c on c.id = i.customer_id where date(i.created_time) between(@p_date_debut and @p_date_fin) and hour(i.created_time) >= 17 group by a.i";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = Date_debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = Date_fin
                };
                cmd.Parameters.Add(p_date);
                cmd.Parameters.Add(p_date_fin);

                try
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        return table;
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }


        public decimal GetResteApayerServiceUN(DateTime debut, DateTime fin)
        {
            var sql = "select  sum(d.amount*d.negociated_price) - a.montant 'Montant Dettes' \r\nFROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  \r\ninner join detailsfacturepressing d on d.invoice_id= a.i \r\ninner join invoices i on i.id = a.i \r\ninner join customers c on c.id = i.customer_id \r\nwhere date(i.created_time) >= date(@p_date_debut) \r\nand date(i.created_time) <=date(@p_date_fin) \r\nand hour(i.created_time)<17 and i.IsPayed=false group by a.i";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };
                cmd.Parameters.Add(p_date);
                cmd.Parameters.Add(p_date_fin);

                try
                {
                    decimal total = 0;
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);
                        
                         foreach(DataRow dr in table.Rows)
                        {
                            total =total +Convert.ToDecimal(dr[0]);
                        }

                        return total;
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }

        public decimal GetResteApayerServiceDEUX(DateTime debut, DateTime fin)
        {
            var sql = "select  sum(d.amount*d.negociated_price) - a.montant 'Montant Dettes' FROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  inner join detailsfacturepressing d on d.invoice_id= a.i inner join invoices i on i.id = a.i inner join customers c on c.id = i.customer_id where date(i.created_time) >= date(@p_date_debut) and date(i.created_time) <=date(@p_date_fin) and hour(i.created_time)>=17 and i.IsPayed=false group by a.i";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };
                cmd.Parameters.Add(p_date);
                cmd.Parameters.Add(p_date_fin);

                try
                {
                    decimal total = 0;
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);

                        foreach (DataRow dr in table.Rows)
                        {

                            total =total+ Convert.ToDecimal(dr[0]);
                        }

                        return total;
                    }
                }
                catch (Exception)
                {
                    return default;
                }
            }
        }

        public DataTable GetDettesPayéesServiceDEUX(DateTime debut, DateTime fin)
        {
            var sql = "select i.id Facture, c.name,c.phone 'Date', a.montant from accompte a \r\ninner join invoices i on i.id= a.invoice_id \r\ninner join customers c on c.id =i.customer_id \r\nwhere date(a.created_time)>=date(@p_date_debut)  and date(a.created_time)<=date(@p_date_fin)  and i.IsPayed=true and a.isSold=true and hour(a.created_time)>=17 and (i.created_time)< date(a.created_time) ";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public DataTable GetDettesNONPayéesServiceDEUX(DateTime debut, DateTime fin)
        {
            var sql = "select  a.i 'N°',c.name 'Nom Client',c.phone 'Télephone', sum(d.amount*d.negociated_price) - a.montant 'Montant Dettes'\r\nFROM (SELECT invoice_id i, sum(montant) montant from accompte group by i  ) a  inner join detailsfacturepressing d on d.invoice_id= a.i \r\ninner join invoices i on i.id = a.i \r\ninner join customers c on c.id = i.customer_id\r\nwhere date(i.created_time) >= date(@p_date_debut) and date(i.created_time) <=date(@p_date_fin) and hour(i.created_time)>=17 and i.IsPayed=false\r\ngroup by a.i";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = fin
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public decimal GetTotalDettesNonPayeesService2(DateTime debut, DateTime fin)
        {
            var sql = "select ifnull(sum(d.negociated_price * d.amount),0) - ifnull(a.montant,0) 'Dette' from\r\ndetailsfacturepressing d\r\ninner join invoices i on i.id = d.invoice_id\r\ninner join accompte a on a.invoice_id = i.id\r\ninner join customers c on c.id = i.customer_id where date(i.created_time) between date(@p_date_debut) and date(@p_date_fin) and i.IsPayed=false and hour(i.created_time) >=17;\r\n";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = debut
                };
                cmd.Parameters.Add(p_date_fin);
                cmd.Parameters.Add(p_date_debut);
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


        public decimal GetTotalFactureServiceDEUX(DateTime d1, DateTime d2)
        {
            string sql = "select ifnull(sum(amount * negociated_price),0) 'Total facturé'  from detailsfacturepressing  where date(created_time) between date(@p_date_debut) and date(@p_date_fin) AND Hour(created_time)>= 17 ";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Value = d1
                };
                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Value = d2
                };
                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);

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

        public decimal GetTotalDettesPayeesServiceDEUX(DateTime debut, DateTime fin)
        {
            var sql = "select ifnull(sum(a.montant),0) total from accompte a inner join invoices i on i.id= a.invoice_id inner join customers c on c.id =i.customer_id where date(a.created_time)>=date(@p_date_debut)  and date(a.created_time)<=date(@p_date_fin)  and i.IsPayed=true and a.isSold=true and hour(a.created_time)>=17 and (i.created_time)< date(a.created_time) ";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {

                MySqlParameter p_date_debut = new MySqlParameter("@p_date_debut", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = debut
                };

                MySqlParameter p_date_fin = new MySqlParameter("@p_date_fin", MySqlDbType.Date)
                {
                    Direction = ParameterDirection.Input,
                    Value = fin
                };

                cmd.Parameters.Add(p_date_debut);
                cmd.Parameters.Add(p_date_fin);
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
       
        #endregion methode Service UN


    }
}