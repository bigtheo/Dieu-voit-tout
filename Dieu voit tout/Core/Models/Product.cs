using MySqlConnector;
using Helper.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Helper.Core.Models
{
    public class Product
    {
        #region Propriétés

        public string CodeBarre { get; set; }
        public DateTime CreatedTime { get; set; }
        public string Designation { get; set; }
        public decimal Price { get; set; }
        public decimal BuyPrice { get; set; } = 0;
        public string unity { get; set; }
        public long Id { get; set; }

        #endregion Propriétés

        #region Méthodes

        public void AddProduct()
        {
            var sql = "Insert into Products(code_Barre,Designation,Unity,unit_price,Buy_price) values(@p_codebare,@p_Designation,@p_unity,@p_price,@p_Buy_price)";
            Helper.Connexion.OuvrirConnexion();
            using (MySqlCommand cmd = new MySqlCommand(sql, Helper.Connexion.Con))
            {
                MySqlParameter p_code_bare = new MySqlParameter("@p_codebare", MySqlDbType.VarChar)
                {
                    Value = this.CodeBarre
                };

                MySqlParameter p_designation = new MySqlParameter("p_designation", MySqlDbType.VarChar)
                {
                    Value = this.Designation
                };

                MySqlParameter p_unity = new MySqlParameter("@p_unity", MySqlDbType.Int64)
                {
                    Value = this.unity
                };

                MySqlParameter p_price = new MySqlParameter("@p_price", MySqlDbType.Decimal)
                {
                    Value = this.Price
                };


                MySqlParameter p_buy_price = new MySqlParameter("@p_Buy_price", MySqlDbType.Decimal)
                {
                    Value = this.BuyPrice
                };
                cmd.Parameters.Add(p_designation);
                cmd.Parameters.Add(p_unity);
                cmd.Parameters.Add(p_price);
                cmd.Parameters.Add(p_code_bare);
                cmd.Parameters.Add(p_buy_price);

                try
                {
                    int insertRows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Produit \t:{this.Designation}\nPrix \t:{this.Price.ToString("c")}\nunité \t:{this.unity}\n\n a été ajouté avec succès.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1146:
                            MessageBox.Show("Table existante", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        case 1062:
                          MessageBox.Show($"Un produit contenant le même code barre existe déjà", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                            break;

                        default:
                            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            };
        }

        public void DeleteProduct(Int64 Id)
        {
            var sql = "Delete from Product  where Id =@p_id";
            Helper.Connexion.OuvrirConnexion();
            using (MySqlCommand cmd = new MySqlCommand(sql, Helper.Connexion.Con))
            {
                MySqlParameter p_id = new MySqlParameter("@p_Id", MySqlDbType.Int64)
                {
                    Value = this.Id
                };
                cmd.Parameters.Add(p_id);

                try
                {
                    int insertRows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Produit \t: {this.Designation}\nPrix \t: {this.Price.ToString("c")}\nunité \t: {this.unity}\n\n a été supprimé avec succès.", "Suppression", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1146:
                            MessageBox.Show("Table existante", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        case 1062:
                            MessageBox.Show("Un produit contenant le même code barre existe déjà.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;

                        default:
                            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                    }
                }
            };
        }

        public Product GetProduct()
        {
            var sql = "select code_barre 'Code Barre',Designation 'Désignation', Unity 'Unité', created_time 'Date',unit_price 'Prix',Buy_Price from products where Id=@p_id";

            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_id = new MySqlParameter("@p_id", MySqlDbType.Int64)
                {
                    Value = this.Id
                };
                cmd.Parameters.Add(p_id);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    
                    DataTable table = new DataTable();

                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        this.CodeBarre = row[0].ToString();
                        this.Designation = row[1].ToString();
                        this.unity = Convert.ToString(row[2]);
                        this.CreatedTime = Convert.ToDateTime(row[3]);
                        this.Price = Convert.ToDecimal(row[4].ToString());
                        this.BuyPrice = Convert.ToDecimal(row[5].ToString());   
                    }
                    return this;
                }
            }
        }

        public Product GetProduct(string codeBare)
        {
            var sql = "select id,code_barre 'Code Barre',Designation 'Désignation', Unity 'Unité', created_time 'Date',unit_price 'Prix' from products where Code_barre=@p_code";

            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_code = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = codeBare
                };
                cmd.Parameters.Add(p_code);

                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    Product product = new Product();
                    DataTable table = new DataTable();

                    adapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        product = new Product()
                        {
                            Id=Convert.ToInt64(row[0].ToString()),
                            CodeBarre = row[1].ToString(),
                            Designation = row[2].ToString(),
                            unity = Convert.ToString(row[3]),
                            CreatedTime = Convert.ToDateTime(row[4]),
                            Price = Convert.ToDecimal(row[5].ToString())
                        };
                    }
                    return product;
                }
            }
        }

        public DataTable GetProducts()
        {
            List<Product> products = new List<Product>();
            var sql = "select Id, code_barre 'Code Barre',Designation 'Désignation', Unity 'Unité', created_time 'Date',buy_price 'Prix achat',unit_price 'Prix de vente' from products;";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Helper.Connexion.Con))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
            }
        }

        public DataTable GetTop10Products()
        {
            
            var sql = "select p.code_barre 'Code',p.Designation 'Désignation',sum(d.amount) Vendu from products p inner join detailsfactureboutique d on d.product_id = p.id group by p.id order by vendu desc limit 10";
            Connexion.Connecter();
            using (MySqlCommand cmd = new MySqlCommand(sql, Helper.Connexion.Con))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    return table;
                }
            }
        }

        public void UpdateProduct()
        {
            var sql = "update  Products set code_Barre=@p_codebare ,Designation=@p_Designation,Unity=@p_unity,unit_price=@p_price,buy_price=@p_buy_price where Id=@p_id";
            Helper.Connexion.OuvrirConnexion();
            using (MySqlCommand cmd = new MySqlCommand(sql, Helper.Connexion.Con))
            {
                MySqlParameter p_code_bare = new MySqlParameter("@p_codebare", MySqlDbType.VarChar)
                {
                    Value = this.CodeBarre
                };

                MySqlParameter p_designation = new MySqlParameter("p_designation", MySqlDbType.VarChar)
                {
                    Value = this.Designation
                };

                MySqlParameter p_unity = new MySqlParameter("@p_unity", MySqlDbType.Int64)
                {
                    Value = this.unity
                };


                MySqlParameter p_price = new MySqlParameter("@p_price", MySqlDbType.Decimal)
                {
                    Value = this.Price
                };

                MySqlParameter p_id = new MySqlParameter("@p_Id", MySqlDbType.Int64)
                {
                    Value = this.Id
                };


                MySqlParameter p_buy_prce = new MySqlParameter("@p_buy_price", MySqlDbType.Decimal)
                {
                    Value = this.BuyPrice
                };


                cmd.Parameters.Add(p_designation);
                cmd.Parameters.Add(p_id);
                cmd.Parameters.Add(p_unity);
                cmd.Parameters.Add(p_price);
                cmd.Parameters.Add(p_code_bare);
                cmd.Parameters.Add(p_buy_prce);

                try
                {
                    int insertRows = cmd.ExecuteNonQuery();
                    MessageBox.Show($"Produit \t:{this.Designation}\nPrix \t:{this.Price.ToString("c")}\nunité \t:{this.unity}\n\n a été modifié avec succès.", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1146:
                            MessageBox.Show("Table Inexistante, veillez la rédefinir", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 1062:
                            MessageBox.Show("Un produit contenant le même code barre existe déjà.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
            };
        }

        private void AddAmountToStock(string code_bare, long quantite)
        {
            var sql = "update  Products set Amout = Amount - @p_amount where p_codebare =@p_codebare";
            Helper.Connexion.OuvrirConnexion();
            using (MySqlCommand cmd = new MySqlCommand(sql, Helper.Connexion.Con))
            {
                MySqlParameter p_code_bare = new MySqlParameter("@p_codebare", MySqlDbType.VarChar)
                {
                    Value = code_bare

                };


                MySqlParameter p_amount = new MySqlParameter("@p_amount", MySqlDbType.Int64)
                {
                    Value = quantite
                };

                cmd.Parameters.Add(p_amount);
                cmd.Parameters.Add(p_code_bare);

                try
                {
                    int insertRows = cmd.ExecuteNonQuery();
                    if(insertRows > 0)
                    MessageBox.Show($"Produit Ajouté au stock avec succès.", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show($"le code barre {this.CodeBarre}n'est pas associé à un produit.", "Modification", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }
                catch (MySqlException ex)
                {
                    switch (ex.Number)
                    {
                        case 1146:
                            MessageBox.Show("Table Inexistante, veillez la rédefinir", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        case 1062:
                            MessageBox.Show("Un produit contenant le même code barre existe déjà.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;

                        default:
                            MessageBox.Show(ex.Message, "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
            };
        }

        #endregion Méthodes
    }
}