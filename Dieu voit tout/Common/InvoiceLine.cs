using Dieu_voit_tout;
using Dieu_voit_tout.Core.Printing;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;

namespace Dieu_voit_tout.Common
{
    public class InvoiceLine
    {
        public long Id { get; set; }
        public long InvoiceId { get; set; }
        public long Amount { get; set; }
        public decimal NegociatedPrice { get; set; }
        public string CodeBarre { get; internal set; }
        public string Designation { get; internal set; }
        public decimal PrixTotal { get; internal set; }

        public bool IsInserted(ObservableCollection<InvoiceLine> lines,Int64 invoice_id)
        {
            MySqlTransaction transaction = Connexion.Con.BeginTransaction() ;
            try
            {
                
                using (MySqlCommand cmd=new MySqlCommand(Connexion.Con,transaction))
                {
                   
                    var sql = "Insert into invoiceline(code_barre,invoice_id,amount,negociated_price) values (@p_codebarre,@p_invoice_id,@p_amount,@p_negociated_price)";
                    cmd.CommandText=sql;
                    foreach (InvoiceLine order in lines)
                    {
                        MySqlParameter p_code_barre = new MySqlParameter("@p_codebarre", MySqlDbType.Int64)
                        {
                            Value = order.CodeBarre
                        };

                        MySqlParameter p_invoice_id = new MySqlParameter("@p_invoice_id", MySqlDbType.Int64)
                        {
                            Value = invoice_id
                        };

                        MySqlParameter p_amount = new MySqlParameter("@p_amount", MySqlDbType.Int64)
                        {
                            Value = order.Amount
                        };

                        MySqlParameter p_negociated_price = new MySqlParameter("@p_negociated_price", MySqlDbType.Decimal)
                        {
                            Value = order.NegociatedPrice
                        };

                        cmd.Parameters.Add(p_code_barre);
                        cmd.Parameters.Add(p_invoice_id);
                        cmd.Parameters.Add(p_negociated_price);
                        cmd.Parameters.Add(p_amount);

                        cmd.ExecuteNonQuery();

                        cmd.Parameters.Clear();
                    }

                    //validation des entregistrements...
                    transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return default;

            }

        }

        public void Print(ObservableCollection<InvoiceLine> order_collection,Int64 invoice_id,Customer customer,decimal total_general)
        {

            #region Création du document

            float width = Convert.ToSingle(88 * 2.54);//taill du papier
            Rectangle taille = new Rectangle(new Rectangle(width, 10000)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille);
            doc.SetMargins(doc.LeftMargin, doc.RightMargin, 1, doc.Bottom);

            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "invoices.pdf");
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                PdfWriter.GetInstance(doc, fs);
                doc.Open(); //ouverture du document pour y écrire
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion Création du document

            #region les polices utilisées

            Font police_entete = FontFactory.GetFont("TIMES NEW ROMAN", 7);
            police_entete.SetStyle(1);

            #endregion les polices utilisées

            #region tableau principle

            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 130
            };
            table.SetWidths(new float[] { 60, 20, 9, 25 });
            PdfPCell cell_designation = new PdfPCell(new Phrase("Désignation", police_entete));
            PdfPCell cell_quantite = new PdfPCell(new Phrase("P.U.", police_entete));
            PdfPCell cell_prix_unitaire = new PdfPCell(new Phrase("QT.", police_entete));
            PdfPCell cell_prix_total = new PdfPCell(new Phrase("P.T.", police_entete));

            Paragraph p_numero_fatcure = new Paragraph($"Fact. N° {invoice_id}")
            {
                Alignment = 1
            };

            Paragraph p_nom_client = new Paragraph($"Client. : {customer.Name}", police_entete)
            {
                Alignment = 1
            };

            Paragraph p_Titre = new Paragraph("Facture")
            {
                Alignment = 1
            };

            Font font = new Font(Font.FontFamily.HELVETICA, 7, 1);

            // en-tête de la facturee
            table.AddCell(cell_designation);
            table.AddCell(cell_quantite);
            table.AddCell(cell_prix_unitaire);
            table.AddCell(cell_prix_total);

            //ajout des détails à la facture

            Phrase phrase_order;
            Phrase phrase_price;
            Phrase phrase_quantite;
            Phrase phrase_total;
            foreach (InvoiceLine order in order_collection)
            {
                phrase_order = new Phrase(order.Designation, font);
                cell_designation.Phrase = phrase_order;
                table.AddCell(cell_designation);

                phrase_price = new Phrase(order.NegociatedPrice.ToString(), font);
                cell_prix_unitaire.Phrase = phrase_price;
                table.AddCell(cell_prix_unitaire);

                phrase_quantite = new Phrase(Convert.ToString(order.Amount), font);
                cell_quantite.Phrase = phrase_quantite;
                table.AddCell(cell_quantite);

                phrase_total = new Phrase(order.PrixTotal.ToString(), font);
                cell_prix_total.Phrase = phrase_total;
                table.AddCell(cell_prix_total);
            }


            Paragraph passerLigne = new Paragraph(Environment.NewLine);

            #endregion tableau principle

           var MentionTermeCondition = System.Configuration.ConfigurationManager.AppSettings["mention"];
            Paragraph paragraphs = new Paragraph(MentionTermeCondition, font)
            {
                Alignment = Element.ALIGN_LEFT
            };
            Paragraph p_mention = paragraphs;

            /*ajaout de l'en-tête du bordereau */
            Dieu_voit_toutHelperr.Helper.AddEntete(doc);

            doc.Add(p_Titre);

            Paragraph p_passeLine = new Paragraph(new Paragraph("-------------------------", font))
            {
                Alignment = 1,
            };
            doc.Add(p_passeLine);
            doc.Add(p_numero_fatcure);
            doc.Add(p_nom_client);
            doc.Add(passerLigne);
            doc.Add(table);
            doc.Add(p_mention);

            doc.Add(new Paragraph($"\n\tImprimée le {DateTime.Now}",font));

            doc.Add(new Paragraph(passerLigne));

            doc.Add(new Paragraph($"\n\tValidée par {FrmLogin.Login}", font));

            doc.Close();

            try
            {
                //on ferme le document après écriture
                
                new FrmApercuAvantImpression().ShowDialog();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable GetTable(DateTime date)
        {
            var sql = " Select l.invoice_id 'N° Facture',p.code_barre 'Code barre' ,p.designation,l.amount 'Quantité',l.negociated_price 'Prix de vente',l.amount*l.negociated_price 'Prix total' from invoiceline l inner join product p on  p.code_barre = l.code_barre where date(created_time) = date(@p_date) union Select 'Total','-','-','-','-',ifnull(sum(l.amount*l.negociated_price),0) from invoiceLine l  where date(created_time) = date(@p_date);";
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
