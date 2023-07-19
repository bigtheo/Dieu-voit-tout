using Helper.Common;
using Helper.Core.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Lady_Miriam;
using Lady_Miriam.Core.Models;
using Helper.Core.Models;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DataRow = System.Data.DataRow;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace Helper
{
    public class FactureDocument
    {
        #region Propriétés

        private readonly Font font = new Font(Font.FontFamily.HELVETICA, 10);
        private Phrase phrase_order;
        private Phrase phrase_price;
        private Phrase phrase_quantite;
        private Phrase phrase_total;
        private readonly Font police_entete = FontFactory.GetFont("TIMES NEW ROMAN", 12, 1);
        public decimal MontantPaye { get; set; }
        public decimal MontantReste { get; set; }
        public string NomClient { get; set; }
        public Int64? NumeroFacture { get; set; }
        public string Titre { get; set; }
        public decimal TotalGeneral { get; set; }
        private string MentionTermeCondition { get; set; }

        #endregion Propriétés


        public void CreateInvoiceAccompteServiceUN(System.Collections.ObjectModel.ObservableCollection<Order> order_collection)
        {
            #region Création du document

            float width = Convert.ToSingle(58 * 2.54);
            Rectangle taille = new Rectangle(new Rectangle(width, 1000)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille, 0f, 0f, 0f, 0f);

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
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 60, 20, 9, 25 });
            PdfPCell cell_designation = new PdfPCell(new Phrase("Désignation", police_entete));
            PdfPCell cell_quantite = new PdfPCell(new Phrase("P.U.", police_entete));
            PdfPCell cell_prix_unitaire = new PdfPCell(new Phrase("QT.", police_entete));
            PdfPCell cell_prix_total = new PdfPCell(new Phrase("P.T.", police_entete));

            Paragraph p_numero_fatcure = new Paragraph($"Fact. N° {NumeroFacture}")
            {
                Alignment = 1
            };

            Paragraph p_nom_client = new Paragraph($"Client. : {NomClient}", police_entete)
            {
                Alignment = 1
            };

            Paragraph p_Titre = new Paragraph(this.Titre)
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

            foreach (Order order in order_collection)
            {
                phrase_order = new Phrase(order.Name, font);
                cell_designation.Phrase = phrase_order;
                table.AddCell(cell_designation);

                phrase_price = new Phrase(order.Price.ToString(), font);
                cell_prix_unitaire.Phrase = phrase_price;
                table.AddCell(cell_prix_unitaire);

                phrase_quantite = new Phrase(Convert.ToString(order.Amount), font);
                cell_quantite.Phrase = phrase_quantite;
                table.AddCell(cell_quantite);

                phrase_total = new Phrase(order.Total.ToString(), font);
                cell_prix_total.Phrase = phrase_total;
                table.AddCell(cell_prix_total);
            }

            // en-tête de la facturee
            PdfPCell cell_prix_total_general = new PdfPCell(new Phrase(TotalGeneral.ToString("c"), police_entete))
            {
                Colspan = 3,
                HorizontalAlignment = 2
            };
            PdfPCell cell_montant_paye = new PdfPCell(new Phrase(MontantPaye.ToString("c"), police_entete))
            {
                Colspan = 3,
                HorizontalAlignment = 2
            };

            PdfPCell cell_reste_paye = new PdfPCell(new Phrase(MontantReste.ToString("c"), police_entete))
            {
                Colspan = 3,
                HorizontalAlignment = 2
            };

            table.AddCell(new Paragraph("Total à payer", font));
            table.AddCell(cell_prix_total_general);

            table.AddCell(new Paragraph("Montant Payé", font));
            table.AddCell(cell_montant_paye);

            table.AddCell(new Paragraph("Dette à Payer", font));
            table.AddCell(cell_reste_paye);

            Paragraph passerLigne = new Paragraph(Environment.NewLine);

            #endregion tableau principle

            MentionTermeCondition = System.Configuration.ConfigurationManager.AppSettings["mention"];
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

            doc.Add(new Paragraph($"Fait à lubumbashi le {DateTime.Now}"));

            doc.Add(new Paragraph(passerLigne));
            doc.Add(new Paragraph($"Sceau et Signature.............."));
            doc.Add(new Paragraph($"Validée par {FrmLogin.Login}", font));

            //on ferme le document après écriture
            doc.Close();
            try
            {
                new FrmApercuAvantImpression().ShowDialog();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
      
        public void CreateJournalFacturation(DataGridView tableDesDonnees)
        {
            #region Création du document

            Rectangle taille = new Rectangle(new Rectangle(PageSize.A4)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille);

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

            Font police_entete = FontFactory.GetFont("TIMES NEW ROMAN", 15);
            police_entete.SetStyle(1);

            #endregion les polices utilisées

            #region tableau principle

            PdfPTable table = new PdfPTable(7)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 30, 12, 15, 7, 10, 12 }); ;
            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_entete));
            PdfPCell cell_designation = new PdfPCell(new Phrase("Désignation", police_entete));
            PdfPCell cell_Client = new PdfPCell(new Phrase("Client", police_entete));
            PdfPCell cell_date = new PdfPCell(new Phrase("Date", police_entete));
            PdfPCell cell_quantite = new PdfPCell(new Phrase("P.U.", police_entete));
            PdfPCell cell_prix_unitaire = new PdfPCell(new Phrase("QT.", police_entete));
            PdfPCell cell_prix_total = new PdfPCell(new Phrase("P.T.", police_entete));
            Paragraph p_Titre = new Paragraph(this.Titre)
            {
                Alignment = 1
            };

            Font font = new Font(Font.FontFamily.HELVETICA, 14, 1);

            // en-tête de la facturee
            table.AddCell(cell_numero);
            table.AddCell(cell_designation);
            table.AddCell(cell_Client);
            table.AddCell(cell_date);
            table.AddCell(cell_quantite);
            table.AddCell(cell_prix_unitaire);
            table.AddCell(cell_prix_total);

            //ajout des détails à la facture

            foreach (DataGridViewRow row in tableDesDonnees.Rows)
            {
                table.AddCell(row.Cells[0].Value.ToString());
                table.AddCell(row.Cells[1].Value.ToString());
                table.AddCell(row.Cells[2].Value.ToString());
                table.AddCell(row.Cells[3].Value.ToString());
                table.AddCell(row.Cells[4].Value.ToString());
                table.AddCell(row.Cells[5].Value.ToString());
                table.AddCell(row.Cells[6].Value.ToString());
            }

            // en-tête de la facturee
            PdfPCell cell_prix_total_general = new PdfPCell(new Phrase(TotalGeneral.ToString(), police_entete));

            table.AddCell("TOTAL");
            table.AddCell("-");
            table.AddCell("-");
            table.AddCell(cell_prix_total_general);

            Paragraph passerLigne = new Paragraph(Environment.NewLine);

            #endregion tableau principle

            Paragraph paragraphs = new Paragraph(MentionTermeCondition, font)
            {
                Alignment = Element.ALIGN_CENTER
            };
            Paragraph p_mention = paragraphs;

            /*ajaout de l'en-tête du bordereau */
            Dieu_voit_toutHelperr.Helper.AddEntete(doc);

            doc.Add(p_Titre);

            Paragraph p_passeLine = new Paragraph(new Paragraph("-------------------------------------", font))
            {
                Alignment = 1,
            };
            doc.Add(p_passeLine);
            doc.Add(passerLigne);
            doc.Add(table);
            doc.Add(p_mention);

            //on ferme le document après écriture
            doc.Close();
            try
            {
                new FrmApercuAvantImpression().ShowDialog();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateRapportDetailleServiceUN(DateTime debut, DateTime fin)
        {
            #region Création du document

            Rectangle taille = new Rectangle(new Rectangle(PageSize.A4)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille);

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

            AjouterEntete(doc);

            #region tableau principle

            PdfPTable table = new PdfPTable(10)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 6, 16, 12, 10, 9, 9, 14, 8, 5, 10 });

            //ajout des détails à la facture
            AjouterDetailsFacture(doc, debut, fin);

            // ajoute des éléments solde journalier

            PdfPCell TotalSolde_cell_span = new PdfPCell()
            {
                Colspan = 10,
                HorizontalAlignment = 1,
                Phrase = new Phrase("Dettes payées")
            };

            table.AddCell(TotalSolde_cell_span);

            //ajouter les éléments du solde
            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_entete));
            PdfPCell cell_nom_client = new PdfPCell(new Phrase("Nom client", police_entete));
            PdfPCell cell_date = new PdfPCell(new Phrase("Date", police_entete));

            PdfPCell cell_solde_span = new PdfPCell()
            {
                Colspan = 7,
                HorizontalAlignment = 1,
                Phrase = new Phrase("Montant")
            };

            table.AddCell(cell_numero);
            table.AddCell(cell_nom_client);
            table.AddCell(cell_date);
            table.AddCell(cell_solde_span);

            foreach (DataRow row in new Facture().GetDettesPayéesServiceUN(debut,fin).Rows)
            {
                cell_solde_span = new PdfPCell()
                {
                    Colspan = 7,
                    HorizontalAlignment = 1,
                    Phrase = new Phrase(Convert.ToString(row[3]))
                };
                cell_numero = new PdfPCell(new Phrase(Convert.ToString(row[0]), font));
                table.AddCell(cell_numero);
                cell_nom_client = new PdfPCell(new Phrase(Convert.ToString(row[1]), font));

                table.AddCell(cell_nom_client);
                cell_date = new PdfPCell(new Phrase(Convert.ToDateTime(row[2]).ToString("dd-MM-yy"), font));

                table.AddCell(cell_date);
                cell_solde_span = new PdfPCell(new Phrase(Convert.ToDecimal(row[3]).ToString("c"), police_entete))
                {
                    Colspan = 7,
                    HorizontalAlignment = 1
                };
                table.AddCell(cell_solde_span);
            }

            AjoutAccomptePayeServiceUN(table, debut, fin);

            #endregion tableau principle

            AjouterRapportDetailsDepensesServiceUN(doc, debut);


            AjouterPiedTableau(doc, table, debut, fin);
        }

        #region Functions and Meethode service UN

        public void CreateJournalDettesServiceUN(DataTable dataTable)
        {
            #region Création du document

            Rectangle taille = new Rectangle(new Rectangle(PageSize.A4)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille);

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

            Font police_entete = FontFactory.GetFont("TIMES NEW ROMAN", 15);
            police_entete.SetStyle(1);

            #endregion les polices utilisées

            #region tableau principle

            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 5, 20, 12, 10 }); ;
            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_entete));
            PdfPCell cell_name = new PdfPCell(new Phrase("Client", police_entete));
            PdfPCell cell_phone = new PdfPCell(new Phrase("Phone", police_entete));
            PdfPCell cell_montant = new PdfPCell(new Phrase("Montant Dette", police_entete));


            Paragraph p_Titre = new Paragraph(this.Titre)
            {
                Alignment = 1
            };

            Font font = new Font(Font.FontFamily.HELVETICA, 14, 1);

            // en-tête de la facturee
            table.AddCell(cell_numero);
            table.AddCell(cell_name);
            table.AddCell(cell_phone);
            table.AddCell(cell_montant);

            //ajout des détails à la facture
            decimal totalDette = 0;
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                table.AddCell(row[0].ToString());
                table.AddCell(row[1].ToString());
                table.AddCell(row[2].ToString());
                table.AddCell(Convert.ToDecimal(row[5]).ToString("c"));
                totalDette += Convert.ToDecimal(row[5]);
            }
            table.AddCell("-");
            table.AddCell("Total");
            table.AddCell("--");
            table.AddCell(totalDette.ToString("c"));

            Paragraph passerLigne = new Paragraph(Environment.NewLine);

            #endregion tableau principle

            Paragraph paragraphs = new Paragraph(MentionTermeCondition, font)
            {
                Alignment = Element.ALIGN_CENTER
            };
            Paragraph p_mention = paragraphs;

            /*ajaout de l'en-tête du bordereau */
            Dieu_voit_toutHelperr.Helper.AddEntete(doc);

            doc.Add(p_Titre);

            Paragraph p_passeLine = new Paragraph(new Paragraph("-------------------------------------", font))
            {
                Alignment = 1,
            };
            doc.Add(p_passeLine);
            doc.Add(passerLigne);
            doc.Add(table);
            doc.Add(p_mention);

            //on ferme le document après écriture
            doc.Close();
            try
            {
                new FrmApercuAvantImpression().ShowDialog();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CreateJournalFacturationServiceUN(DataTable tableDesDonnees)
        {
            #region Création du document

            Rectangle taille = new Rectangle(new Rectangle(PageSize.A4)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille);

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

            Font police_entete = FontFactory.GetFont("TIMES NEW ROMAN", 15);
            police_entete.SetStyle(1);

            #endregion les polices utilisées

            #region tableau principle

            PdfPTable table = new PdfPTable(6)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 5, 20, 12, 13, 10, 5 }); ;
            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_entete));
            PdfPCell cell_name = new PdfPCell(new Phrase("Client", police_entete));
            PdfPCell cell_phone = new PdfPCell(new Phrase("Phone", police_entete));
            PdfPCell cell_date = new PdfPCell(new Phrase("Date", police_entete));
            PdfPCell cell_montant = new PdfPCell(new Phrase("Montant", police_entete));
            PdfPCell cell_paye = new PdfPCell(new Phrase("Payé", police_entete));
            
  

            Paragraph p_Titre = new Paragraph(this.Titre)
            {
                Alignment = 1
            };

            Font font = new Font(Font.FontFamily.HELVETICA, 14, 1);

            // en-tête de la facturee
            table.AddCell(cell_numero);
            table.AddCell(cell_name);
            table.AddCell(cell_phone);
            table.AddCell(cell_date);
            table.AddCell(cell_montant);
            table.AddCell(cell_paye);

            //ajout des détails à la facture

            foreach (System.Data.DataRow row in tableDesDonnees.Rows)
            {
                table.AddCell(row[0].ToString());
                table.AddCell(row[1].ToString());
                table.AddCell(row[2].ToString());
                table.AddCell(row[3].ToString());
                table.AddCell(row[4].ToString());
                table.AddCell(row[5].ToString());
            }

            // en-tête de la facturee
            PdfPCell cell_prix_total_general = new PdfPCell(new Phrase(TotalGeneral.ToString(), police_entete));

            table.AddCell("TOTAL");
            table.AddCell("-");
            table.AddCell("-");
            table.AddCell(cell_prix_total_general);

            Paragraph passerLigne = new Paragraph(Environment.NewLine);

            #endregion tableau principle

            Paragraph paragraphs = new Paragraph(MentionTermeCondition, font)
            {
                Alignment = Element.ALIGN_CENTER
            };
            Paragraph p_mention = paragraphs;

            /*ajaout de l'en-tête du bordereau */
            Dieu_voit_toutHelperr.Helper.AddEntete(doc);

            doc.Add(p_Titre);

            Paragraph p_passeLine = new Paragraph(new Paragraph("-------------------------------------", font))
            {
                Alignment = 1,
            };
            doc.Add(p_passeLine);
            doc.Add(passerLigne);
            doc.Add(table);
            doc.Add(p_mention);

            //on ferme le document après écriture
            doc.Close();
            try
            {
                new FrmApercuAvantImpression().ShowDialog();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public PdfPTable GetTableSyntheseServiceUN(DateTime debut, DateTime fin)
        {
            PdfPTable table = new PdfPTable(3);
            table.SetWidths(new float[] { 10, 50, 30 });
            table.WidthPercentage = 60;
            PdfPCell p_numero = new PdfPCell(new Phrase("N°", police_entete));
            PdfPCell p_designation = new PdfPCell(new Phrase("Intitulé", police_entete));
            PdfPCell p_montant = new PdfPCell(new Phrase("Montant", police_entete));

            //ajout en-tête tableau
            table.AddCell(p_numero);
            table.AddCell(p_designation);
            table.AddCell(p_montant);

            //ajout des élément du table

            p_numero = new PdfPCell(new Phrase("01", font));
            p_designation = new PdfPCell(new Phrase("Total à Enregistré", font));
            p_montant = new PdfPCell(new Phrase(new Facture().GetTotalFactureServiceUN(debut, fin).ToString("c"), font));

            table.AddCell(p_numero);
            table.AddCell(p_designation);
            table.AddCell(p_montant);

            p_numero = new PdfPCell(new Phrase("02", font));
            p_designation = new PdfPCell(new Phrase("Dettes payées", font));
            p_montant = new PdfPCell(new Phrase(new Facture().GetTotalDettesPayeesServiceUN(debut,fin).ToString("c"), font));

            table.AddCell(p_numero);
            table.AddCell(p_designation);
            table.AddCell(p_montant);

            p_numero = new PdfPCell(new Phrase("03", font));
            p_designation = new PdfPCell(new Phrase("Vente Journalière", font));
            p_montant = new PdfPCell(new Phrase("0", font));

            table.AddCell(p_numero);
            table.AddCell(p_designation);
            table.AddCell(p_montant);

            p_numero = new PdfPCell(new Phrase("05", font));
            p_designation = new PdfPCell(new Phrase("Total dépenses", font));
            p_montant = new PdfPCell(new Phrase(new Dépenses().GetTotalJournalierDepenseServiceUN(debut,fin).ToString("c"), font));

            table.AddCell(p_numero);
            table.AddCell(p_designation);
            table.AddCell(p_montant);

            p_numero = new PdfPCell(new Phrase("06", font));
            p_designation = new PdfPCell(new Phrase("Réduction", font));

            table.AddCell(p_numero);
            table.AddCell(p_designation);
            table.AddCell(p_montant);

            p_numero = new PdfPCell(new Phrase("07", police_entete));
            p_designation = new PdfPCell(new Phrase("Vente Jour + Dettes payé - Dépenses - Réduction", police_entete));
            p_montant = new PdfPCell(new Phrase("0", police_entete));

            table.AddCell(p_numero);
            table.AddCell(p_designation);
            table.AddCell(p_montant);

            return table;
        }

        internal void CreateInvoiceDettesPayeesServiceUN(System.Collections.ObjectModel.ObservableCollection<Order> order_collection)
        {
            #region Création du document

            float width = Convert.ToSingle(58 * 2.54);
            Rectangle taille = new Rectangle(new Rectangle(width, 1000)); // le format(longueur et largueur) du récu
            Document doc = new Document(taille, 0f, 0f, 0f, 0f);

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
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 60, 20, 9, 25 });
            PdfPCell cell_designation = new PdfPCell(new Phrase("Désignation", police_entete));
            PdfPCell cell_quantite = new PdfPCell(new Phrase("P.U.", police_entete));
            PdfPCell cell_prix_unitaire = new PdfPCell(new Phrase("QT.", police_entete));
            PdfPCell cell_prix_total = new PdfPCell(new Phrase("P.T.", police_entete));

            Paragraph p_numero_fatcure = new Paragraph($"Fact. N° {NumeroFacture}")
            {
                Alignment = 1
            };

            Paragraph p_nom_client = new Paragraph($"Client. : {NomClient}", police_entete)
            {
                Alignment = 1
            };

            Paragraph p_Titre = new Paragraph(this.Titre)
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

            foreach (Order order in order_collection)
            {
                phrase_order = new Phrase(order.Name, font);
                cell_designation.Phrase = phrase_order;
                table.AddCell(cell_designation);

                phrase_price = new Phrase(order.Price.ToString(), font);
                cell_prix_unitaire.Phrase = phrase_price;
                table.AddCell(cell_prix_unitaire);

                phrase_quantite = new Phrase(Convert.ToString(order.Amount), font);
                cell_quantite.Phrase = phrase_quantite;
                table.AddCell(cell_quantite);

                phrase_total = new Phrase(order.Total.ToString(), font);
                cell_prix_total.Phrase = phrase_total;
                table.AddCell(cell_prix_total);
            }

            // en-tête de la facturee
            PdfPCell cell_prix_total_general = new PdfPCell(new Phrase(TotalGeneral.ToString("c"), police_entete))
            {
                HorizontalAlignment = 2,
                Colspan = 4
            };

            table.AddCell("Solde");
            table.AddCell(cell_prix_total_general);

            Paragraph passerLigne = new Paragraph(Environment.NewLine);

            #endregion tableau principle

            MentionTermeCondition = System.Configuration.ConfigurationManager.AppSettings["mention"];
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

            doc.Add(new Paragraph($"Fait à lubumbashi le {DateTime.Now}"));

            doc.Add(new Paragraph(passerLigne));
            doc.Add(new Paragraph($"Sceau et Signature.............."));
            doc.Add(new Paragraph($"Validée par {FrmLogin.Login}", font));

            //on ferme le document après écriture
            doc.Close();
            try
            {
                new FrmApercuAvantImpression().ShowDialog();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AjoutAccomptePayeServiceUN(PdfPTable table, DateTime debut, DateTime fin)
        {
            //ajout du total soldé aujourd'hui
            PdfPCell cell_solde_total = new PdfPCell(new Phrase(new Facture().GetTotalDettesPayeesServiceUN(debut,fin).ToString("c"), police_entete))
            {
                Colspan = 3,
                HorizontalAlignment = 1
            };
            PdfPCell cell_solde_total_designation = new PdfPCell(new Phrase("Total dettes payées", police_entete))
            {
                Colspan = 7,
                HorizontalAlignment = 1
            };
            table.AddCell(cell_solde_total_designation);
            table.AddCell(cell_solde_total);

            //ajout du montant payé

            PdfPCell TotalPaye_cell_spanDes = new PdfPCell(new Phrase("Total Ventes journalières", police_entete))
            {
                Colspan = 7,
                HorizontalAlignment = 1,
            };

            PdfPCell TotalPaye_cell_span = new PdfPCell(new Phrase("0", police_entete))
            {
                Colspan = 3,
                HorizontalAlignment = 1,
            };

            table.AddCell(TotalPaye_cell_spanDes);
            table.AddCell(TotalPaye_cell_span);
        }

        private void AjouterDetailsFacture(Document doc, DateTime debut, DateTime fin)
        {
            PdfPTable table = new PdfPTable(8)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 6, 16, 12, 10, 14, 8, 5, 10 });
            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_entete));
            PdfPCell cell_nom_client = new PdfPCell(new Phrase("Nom Client", police_entete));
            PdfPCell cell_date = new PdfPCell(new Phrase("Date", police_entete));
            PdfPCell cell_a_payer = new PdfPCell(new Phrase("à Payer", police_entete));
            PdfPCell cell_article = new PdfPCell(new Phrase("Article", police_entete));
            PdfPCell cell_quantite = new PdfPCell(new Phrase("P.U.", police_entete));
            PdfPCell cell_prix_unitaire = new PdfPCell(new Phrase("QT.", police_entete));
            PdfPCell cell_prix_total = new PdfPCell(new Phrase("P.T.", police_entete));

            table.AddCell(cell_numero);
            table.AddCell(cell_nom_client);
            table.AddCell(cell_date);
            table.AddCell(cell_a_payer);
            table.AddCell(cell_article);
            table.AddCell(cell_quantite);
            table.AddCell(cell_prix_unitaire);
            table.AddCell(cell_prix_total);

            DataTable listeDesFacturesDuJour = new Facture() { Date_debut = debut, Date_fin = fin }.GetDaylyInvoicesServiceUN();

            foreach (DataRow Facture in listeDesFacturesDuJour.Rows)
            {
                DataTable tableDesDonnes = new FactureDetailsPressing().GetFacturePressingDetailsTable(Convert.ToInt64(Facture[0]));

                int nombreDesArticles = tableDesDonnes.Rows.Count;

                cell_numero = new PdfPCell
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(Facture[0].ToString(), font),
                };
                cell_date = new PdfPCell
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(Facture[1].ToString(), font)
                };
                cell_article = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(Convert.ToDateTime(Facture[2]).ToString("dd-MM-yy"), font),
                    VerticalAlignment = 1
                };

                cell_a_payer = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(Facture[3].ToString(), font),
                    VerticalAlignment = 1
                };

                table.AddCell(cell_numero);
                table.AddCell(cell_date);
                table.AddCell(cell_article);
                table.AddCell(cell_a_payer);

                foreach (DataRow row in tableDesDonnes.Rows)
                {
                    table.AddCell(new Phrase(Convert.ToString(row[0]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[2]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[1]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[3]), font));
                }
                tableDesDonnes.Rows.Clear();
            }

            // ajout du total facturé
            AjoutTotalFactureServiceUN(table, debut, fin);

            doc.Add(table);
        }

        private void AjouterEntete(Document doc)
        {
            Paragraph p_Titre = new Paragraph(this.Titre)
            {
                Alignment = 1
            };

            /*ajaout de l'en-tête du bordereau */
            Dieu_voit_toutHelperr.Helper.AddEntete(doc);
            doc.Add(p_Titre);

            Paragraph p_passeLine = new Paragraph(new Paragraph("-------------------------------------", font))
            {
                Alignment = 1,
            };
            doc.Add(p_passeLine);
            doc.Add(new Paragraph(Environment.NewLine));
        }

        private void AjouterPiedTableau(Document doc, PdfPTable table, DateTime debut, DateTime fin)
        {
            Paragraph paragraphs = new Paragraph(MentionTermeCondition, font)
            {
                Alignment = Element.ALIGN_CENTER
            };
            Paragraph p_mention = paragraphs;

            doc.Add(table);
            doc.Add(p_mention);

            //insertion de la paragraphe
            doc.Add(new Paragraph(Environment.NewLine));
            Paragraph p_synthese = new Paragraph("Rapport synthétique ", police_entete)
            {
                Alignment = Element.ALIGN_CENTER,
            };
            doc.Add(p_synthese);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(GetTableSyntheseServiceUN(debut, fin));

            //on ferme le document après écriture
            doc.Close();
            try
            {
                new FrmApercuAvantImpression().ShowDialog();
            }
            catch (System.ComponentModel.Win32Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AjouterRapportDetailsDepensesServiceUN(Document doc, DateTime date)
        {
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 6, 30, 12, 12 });
            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_entete));
            PdfPCell cell_Designation = new PdfPCell(new Phrase("Designation", police_entete));
            PdfPCell cell_date = new PdfPCell(new Phrase("Date", police_entete));
            PdfPCell cell_montant = new PdfPCell(new Phrase("Montant", police_entete));

            PdfPCell cell_entetee_tableau = new PdfPCell(new Phrase("Dépenses journalières", police_entete))
            {
                Colspan = 4,
                HorizontalAlignment = 1
            };

            table.AddCell(cell_entetee_tableau);

            table.AddCell(cell_numero);
            table.AddCell(cell_Designation);
            table.AddCell(cell_date);
            table.AddCell(cell_montant);

            DataTable listeDesDepenses = new Dépenses().GetDetailsDepensesTableServiceUN(date,date);

            foreach (DataRow Facture in listeDesDepenses.Rows)
            {
                cell_numero = new PdfPCell
                {
                    Phrase = new Phrase(Facture[0].ToString(), font),
                };

                cell_Designation = new PdfPCell
                {
                    Phrase = new Phrase(Facture[1].ToString(), font),
                };

                cell_date = new PdfPCell
                {
                    Phrase = new Phrase(Facture[2].ToString(), font)
                };

                cell_montant = new PdfPCell()
                {
                    Phrase = new Phrase(Facture[3].ToString(), font),
                    VerticalAlignment = 1
                };

                table.AddCell(cell_numero);
                table.AddCell(cell_Designation);
                table.AddCell(cell_date);
                table.AddCell(cell_montant);
            }

            doc.Add(table);
        }

        private void AjoutTotalFactureServiceUN(PdfPTable table, DateTime debut, DateTime fin)
        {
            PdfPCell TotalFacture_cell_span = new PdfPCell()
            {
                Colspan = 7,
                HorizontalAlignment = 1,
                Phrase = new Phrase("Total Facturé", police_entete)
            };

            PdfPCell TotalFactureMontant_cell_span = new PdfPCell()
            {
                Colspan = 3,
                HorizontalAlignment = 1,
                Phrase = new Phrase(new Facture().GetTotalFactureServiceUN(debut, fin).ToString("c"), police_entete)
            };

            table.AddCell(TotalFacture_cell_span);
            table.AddCell(TotalFactureMontant_cell_span);
        } 
        #endregion


    }
}