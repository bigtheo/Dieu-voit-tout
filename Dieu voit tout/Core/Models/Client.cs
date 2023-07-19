using MySqlConnector;
using Helper.Helper;
using System;
using System.Data;
using System.Windows.Forms;
using System.Xml;

namespace Helper.Core.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedTime { get; set; }

        public long Insert()
        {
            var sql = "insert into customers(name,phone)  values(@p_name,@p_phone) ";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_name = new MySqlParameter("@p_name", MySqlDbType.VarChar)
                {
                    Value = this.Name
                };

                MySqlParameter p_phone = new MySqlParameter("@p_phone", MySqlDbType.VarChar)
                {
                    Value = this.Phone
                };

                cmd.Parameters.Add(p_name);
                cmd.Parameters.Add(p_phone);

                try
                {
                    cmd.ExecuteNonQuery();

                    var lastinsertid = cmd.LastInsertedId;
                    return lastinsertid;
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1062:
                            this.Update(this.GetCustomerId(this.Phone));
                            return this.GetCustomerId(this.Phone);

                        default:
                            MessageBox.Show(ex.Message);
                            return 0;
                    }
                }
            }
        }

        private long Update(long id)
        {
            var sql = "update  customers set name=@p_name where id =@p_id";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_name = new MySqlParameter("@p_name", MySqlDbType.VarChar)
                {
                    Value = this.Name
                };

                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.VarChar)
                {
                    Value = id
                };

                cmd.Parameters.Add(p_name);
                cmd.Parameters.Add(p_id);

                try
                {
                    var lastinsertid = cmd.ExecuteNonQuery();

                    return lastinsertid;
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1062:
                            return this.GetCustomerId(this.Phone);

                        default:
                            MessageBox.Show(ex.Message);
                            return 0;
                    }
                }
            }
        }

        private long GetCustomerId(string phone)
        {
            var sql = "Select Id from customers where phone = @p_phone limit 1";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_phone = new MySqlParameter("@p_phone", MySqlDbType.VarChar)
                {
                    Value = this.Phone
                };

                cmd.Parameters.Add(p_phone);

                try
                {
                    return Convert.ToInt64(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return 0;
                }
            }
        }

        public void GetCustomerNameAndPhone(long invoice_id)
        {
            var sql = "select c.Name, c.phone from customers c inner join invoices i on i.customer_id = c.id where i.id = @p_id";
            using (MySqlCommand cmd =new MySqlCommand (sql,Connexion.Con))
            {
                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = invoice_id
                };
                cmd.Parameters.Add(p_id);

                try
                {
                    using (MySqlDataAdapter da=new MySqlDataAdapter (cmd))
                    {
                        DataTable table = new DataTable();
                        da.Fill(table);

                        foreach(DataRow row in table.Rows)
                        {
                            this.Name = row[0].ToString();
                            this.Phone = row[1].ToString();
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
        public string GetCustomerName(string phone)
        {
            var sql = "Select name from customers where phone = @p_phone";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_phone = new MySqlParameter("@p_phone", MySqlDbType.VarChar)
                {
                    Value = phone
                };

                cmd.Parameters.Add(p_phone);

                try
                {
                    return Convert.ToString(cmd.ExecuteScalar()); ;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return string.Empty;
                }
            }
        }
    }
}