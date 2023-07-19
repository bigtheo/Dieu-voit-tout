using MySqlConnector;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Helper.Core.Models
{
    public class Tarif
    {
        public long Id { get; set; }
        public string Designation { get; set; }
        public string Code { get; set; }
        public decimal UnityPrice { get; set; }
        public DateTime CreatedTime { get; set; }

        public void Insert()
        {
            var sql = "replace into tarifs(Designation,code,unity_price) values(@p_designation,@p_code,@p_unity_price)";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_designation = new MySqlParameter("@p_designation", MySqlDbType.VarChar)
                {
                    Value = this.Designation
                };

                MySqlParameter p_unity = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = this.Code
                };

                MySqlParameter p_unity_price = new MySqlParameter("@p_unity_price", MySqlDbType.Decimal)
                {
                    Value = this.UnityPrice
                };
                cmd.Parameters.Add(p_designation);
                cmd.Parameters.Add(p_unity);
                cmd.Parameters.Add(p_unity_price);

                try
                {
                    int rowsCount = cmd.ExecuteNonQuery();
                    MessageBox.Show("Enregistrement effectué avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
            }
        }

        public void Upade(long id)
        {
            var sql = "Update  tarifs set Designation=@p_designation,Code=@p_code,unity_price=@p_unity_price where Id = @p_id";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_designation = new MySqlParameter("@p_designation", MySqlDbType.VarChar)
                {
                    Value = this.Designation
                };

                MySqlParameter p_unity = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = this.Code
                };

                MySqlParameter p_unity_price = new MySqlParameter("@p_unity_price", MySqlDbType.Decimal)
                {
                    Value = this.UnityPrice
                };

                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = id
                }; ;

                cmd.Parameters.Add(p_designation);
                cmd.Parameters.Add(p_unity);
                cmd.Parameters.Add(p_unity_price);
                cmd.Parameters.Add(p_id);

                try
                {
                    int rowsCount = cmd.ExecuteNonQuery();
                    MessageBox.Show("Modification effectuée avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
            }
        }

        public void Delete(long id)
        {
            var sql = "Delete from tarifs where Id = @p_id";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = id
                };
                cmd.Parameters.Add(p_id);

                try
                {
                    int rowsCount = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Suppression effectuée avec succès.\n{rowsCount} ligne(s) affectée(s)", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error); ;
                }
            }
        }

        public DataTable GetTarifsTable()
        {
            var sql = "Select Id,Designation 'Désignation',Code 'Code',Unity_price 'Prix unitaire',created_time 'Date et heure' from Tarifs";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    da.Fill(table);

                    return table;
                }
            }
        }

        public List<string> GetTarifNameList()
        {
            List<string> list = new List<string>();
            var sql = "Select Upper(Designation) from Tarifs";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();

                    try
                    {
                        da.Fill(table);

                        foreach (DataRow row in table.Rows)
                        {
                            string tarif = (string)row[0];
                            list.Add(tarif);
                        }
                        return list;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        return default;
                    }
                }
            }
        }

        public void GetTarifInfos()
        {
            var sql = "Select designation,unity_price from Tarifs where code=@p_code";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_code = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = this.Code
                };
                cmd.Parameters.Add(p_code);

           

                try
                {
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable tale = new DataTable();
                        da.Fill(tale);
                        foreach(DataRow row in tale.Rows)
                        {
                            this.Designation = (string)row[0];
                            this.UnityPrice = Convert.ToDecimal(row[1]);
                        }
                    }
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    
                }
            }
        }
        public decimal GetTarifPrice(string tarifName)
        {
            var sql = "Select unity_price from Tarifs where designation=@p_designation";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_designation = new MySqlParameter("@p_designation", MySqlDbType.VarChar)
                {
                    Value = tarifName
                };
                cmd.Parameters.Add(p_designation);

                try
                {
                    return Convert.ToDecimal(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        public long GetTarifId(string tarifName)
        {
            var sql = "Select id from Tarifs where designation=@p_designation";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_designation = new MySqlParameter("@p_designation", MySqlDbType.VarChar)
                {
                    Value = tarifName
                };
                cmd.Parameters.Add(p_designation);

                try
                {
                    return Convert.ToInt64(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        public long GetTarifIdByCode(string code)
        {
            var sql = "Select id from Tarifs where Code=@p_code";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_code = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = code
                };
                cmd.Parameters.Add(p_code);

                try
                {
                    return Convert.ToInt64(cmd.ExecuteScalar());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }
    }
}
