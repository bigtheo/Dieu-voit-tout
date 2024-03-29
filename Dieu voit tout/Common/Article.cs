﻿using MySqlConnector;
using System;
using System.Data;
using System.Windows.Forms;

namespace Dieu_voit_tout.Common
{
    public class Article
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public long Stock { get; set; } = 0;
        public string CodeBarre { get; set; }
        public DateTime DateExpiration { get; internal set; }
        public DateTime DateFabrication { get; internal set; }

        //permet d'ajouter un article dans la base de données
        public bool AddNewProduct()
        {
            var sql = "Insert into product(code_barre,Designation,pu,stock,date_fabrication,date_expiration) values(@p_code,@p_des,@p_pu,@p_stock,@p_date_fabrication,@p_date_expiration)";

            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_des = new MySqlParameter("@p_des", MySqlDbType.VarChar)
                {
                    Value = this.Description
                };

                MySqlParameter p_code = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = this.CodeBarre
                };

                MySqlParameter p_price = new MySqlParameter("@p_pu", MySqlDbType.Decimal)
                {
                    Value = this.Price
                };

                MySqlParameter p_stock = new MySqlParameter("@p_stock", MySqlDbType.Int64)
                {
                    Value = this.Stock
                };

                MySqlParameter p_date_fab = new MySqlParameter("@p_date_fabrication", MySqlDbType.Date)
                {
                    Value = this.DateFabrication
                };

                MySqlParameter p_date_exp = new MySqlParameter("@p_date_expiration", MySqlDbType.Date)
                {
                    Value = this.DateExpiration
                };

                cmd.Parameters.Add(p_des);
                cmd.Parameters.Add(p_price);
                cmd.Parameters.Add(p_stock);
                cmd.Parameters.Add(p_code);
                cmd.Parameters.Add(p_date_fab);
                cmd.Parameters.Add(p_date_exp);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        //permet de modifier le nom de l'article
        public bool RenameProductName(string newname)
        {
            var sql = "update product set designation=@p_newname where code_barre=@p_code";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                MySqlParameter p_code = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = newname
                };
                MySqlParameter p_newname = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                {
                    Value = newname
                };

                cmd.Parameters.Add(p_newname);
                cmd.Parameters.Add(p_code);

                try
                {
                    return cmd.ExecuteNonQuery() > 0;
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }

        //obtenir les article
        public DataTable GetTable()
        {
            var sql = "select id,code_barre 'Code barre',Designation,pu 'Prix Unitaire',stock 'Quantité' from product;";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
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
      

        //obtenir le nom de l'article par code
        public string GetProductName(string codebare)
        {
            var sql = "select Designation from product where code_barre=@p_code";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                try
                {
                    MySqlParameter p_code = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                    {
                        Value = codebare
                    };
                    cmd.Parameters.Add(p_code);

                    return Convert.ToString(cmd.ExecuteScalar());
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                    return default;
                }
            }
        }


        public decimal GetProductPrice(string codebare)
        {
            var sql = "select pu from product where code_barre=@p_code";
            using (MySqlCommand cmd = new MySqlCommand(sql, Connexion.Con))
            {
                try
                {
                    MySqlParameter p_code = new MySqlParameter("@p_code", MySqlDbType.VarChar)
                    {
                        Value = codebare
                    };
                    cmd.Parameters.Add(p_code);

                    return Convert.ToDecimal(cmd.ExecuteScalar());
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