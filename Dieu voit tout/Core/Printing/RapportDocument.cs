using Helper.Core.Printing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Lady_Miriam.Core.Models;
using Helper.Core.Models;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using DataRow = System.Data.DataRow;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;

namespace Helper.Core.Printing
{
    public class RapportDocument
    {
        public string TitreRapport { get; set; }
        private readonly Font font = new Font(Font.FontFamily.HELVETICA, 10);
        private Document Document { get; set; }
        private readonly Font police_entete_rapport = FontFactory.GetFont("TIMES NEW ROMAN", 18, 1);
        private readonly Font police_titre_tableau = FontFactory.GetFont("TIMES NEW ROMAN", 13, 1);
        private readonly Font police_cellule_tableau = FontFactory.GetFont("TIMES NEW ROMAN", 10, 0);

        public void GenererRapportSerice1(DateTime date1, DateTime date2)
        {
            Document doc = new Document();
            // ajout de l'en-tête
            CreerEnteteDocument(ref doc);

            //création du tableau vente journalère
            CreerTableauVenteJournaliereUN(doc, date1, date2);

            // création du tableau dettes payées
            CreerTableauDettesPayeesUN(doc, date1, date2);

            // création du tableau dettes non payées
            CreerTableauDettesNonPayeesUN(doc, date1, date2);

            // création du tableau depenses journalières
            CreerTableauDepensesUN(doc, date1, date2);

            //création du tableau réduction
            CreerTableauReductionUN(doc, date1, date2);
            //création du tableau syhtèse
            CreerTableauSyntheseUN(doc, date1, date2);

            //Créer le tableau des observations
            CreerTableauObservation(doc);

            //ajout pied de la page
            CreerPiedDocument(doc);
        }

        public void GenererRapportSerice2(DateTime date1, DateTime date2)
        {
            Document doc = new Document();
            // ajout de l'en-tête
            CreerEnteteDocument(ref doc);

            //création du tableau vente journalère
            CreerTableauVenteJournaliereDEUX(doc, date1, date2);

            // création du tableau dettes payées
            CreerTableauDettesPayeesDEUX(doc, date1, date2);

            // création du tableau dettes non payées
            CreerTableauDettesNonPayeesDEUX(doc, date1, date2);

            // création du tableau depenses journalières
            CreerTableauDepensesDEUX(doc, date1, date2);

            //Créer le tableau réduction
            CreerTableauReductionDEUX(doc, date1, date2);

            //création du tableau syhtèse
            CreerTableauSyntheseDEUX(doc, date1, date2);

            //Créer le tableau des observations
            CreerTableauObservation(doc);

            //ajout pied de la page
            CreerPiedDocument(doc);
        }

        #region En-tête et pied de la page

        private void CreerPiedDocument(Document doc)
        {
            doc.Close();

            new FrmApercuAvantImpression().ShowDialog();
        }

        private void CreerEnteteDocument(ref Document doc)
        {
            #region Création du document

            Rectangle taille = new Rectangle(new Rectangle(PageSize.A4));
            doc = new Document(taille);

            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "invoices.pdf");
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                PdfWriter.GetInstance(doc, fs);
                doc.Open(); //ouverture du document pour y écrire

                Paragraph p_titre = new Paragraph(TitreRapport, police_entete_rapport)
                {
                    Alignment = 1
                };

                Dieu_voit_toutHelperr.Helper.AddEntete(doc);

                doc.Add(p_titre);
                doc.Add(new Phrase(Environment.NewLine));
            }
            catch (IOException ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion Création du document
        }

        #endregion En-tête et pied de la page

        #region LES METHODES DES SERVICES

        #region SERVICE 1

        private void CreerTableauVenteJournaliereUN(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"1.\tVENTE JOURNALIERE DU {date1} ", police_titre_tableau);

            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(10)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 9, 26, 23, 15, 15, 15, 17, 8, 15, 15 });

            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_cellule_tableau));
            PdfPCell cell_client = new PdfPCell(new Phrase("Client", police_cellule_tableau));
            PdfPCell cell_telephone = new PdfPCell(new Phrase("Téléphone", police_cellule_tableau));
            PdfPCell cell_a_payer = new PdfPCell(new Phrase("à payer", police_cellule_tableau));
            PdfPCell cell_paye = new PdfPCell(new Phrase("Payé", police_cellule_tableau));
            PdfPCell cell_reste = new PdfPCell(new Phrase("Reste", police_cellule_tableau));
            PdfPCell cell_Article = new PdfPCell(new Phrase("Article", police_cellule_tableau));
            PdfPCell cell_quantite = new PdfPCell(new Phrase("QT", police_cellule_tableau));
            PdfPCell cell_prix_unitaire = new PdfPCell(new Phrase("PU", police_cellule_tableau));
            PdfPCell cell_prix_total = new PdfPCell(new Phrase("PT", police_cellule_tableau));

            //ajout en-tête document
            table.AddCell(cell_numero);
            table.AddCell(cell_client);
            table.AddCell(cell_telephone);
            table.AddCell(cell_a_payer);
            table.AddCell(cell_paye);
            table.AddCell(cell_reste);
            table.AddCell(cell_Article);
            table.AddCell(cell_quantite);
            table.AddCell(cell_prix_unitaire);
            table.AddCell(cell_prix_total);

            //parcours des factures
            var listeDesFacture = new Facture().ObtenirLesFactureJournalieresServiceUn(date1, date2);

            foreach (DataRow facture in listeDesFacture.Rows)
            {
                DataTable tableDesDonnes = new FactureDetailsPressing().GetFacturePressingDetailsTable(Convert.ToInt64(facture[0]));

                int nombreDesArticles = tableDesDonnes.Rows.Count;

                cell_numero = new PdfPCell
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[0].ToString(), font),
                    HorizontalAlignment = 1
                };
                cell_client = new PdfPCell
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[1].ToString(), font),
                };
                cell_telephone = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(Convert.ToString(facture[2]).ToString(), font),
                    VerticalAlignment = 1
                };
                cell_a_payer = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[3].ToString(), font),
                    VerticalAlignment = 1
                };
                cell_paye = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[4].ToString(), font),
                    VerticalAlignment = 1
                };
                cell_reste = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[5].ToString(), font),
                    VerticalAlignment = 1
                };

                table.AddCell(cell_numero);
                table.AddCell(cell_client);
                table.AddCell(cell_telephone);
                table.AddCell(cell_a_payer);
                table.AddCell(cell_paye);
                table.AddCell(cell_reste);

                foreach (DataRow row in tableDesDonnes.Rows)
                {
                    table.AddCell(new Phrase(Convert.ToString(row[0]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[1]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[2]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[3]), font));
                }
                tableDesDonnes.Rows.Clear();
            }
            //ajout les totaux

            decimal totalApayer = new FactureDetailsPressing().ObtenirTotalAPayerService1(date1, date2);
            decimal totalPaye = new FactureDetailsPressing().ObtenirTotalPayeService1(date1, date2);
            decimal totalReste = new Facture().GetResteApayerServiceUN(date1, date2);
            long nombreDesHabits = new FactureDetailsPressing().ObtenirTotalNombreHabillesService1(date1, date2);

            PdfPCell p_cell_des = new PdfPCell(new Phrase("Total ventes journalières", police_titre_tableau))
            {
                Colspan = 3
            };
            PdfPCell p_cell_totalApayer = new PdfPCell(new Phrase(totalApayer.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalPaye = new PdfPCell(new Phrase(totalPaye.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalReste = new PdfPCell(new Phrase(totalReste.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalQuantite = new PdfPCell(new Phrase(nombreDesHabits.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalPT = new PdfPCell(new Phrase(totalApayer.ToString(), police_titre_tableau));

            table.AddCell(p_cell_des);
            table.AddCell(p_cell_totalApayer);
            table.AddCell(p_cell_totalPaye);
            table.AddCell(p_cell_totalReste);
            table.AddCell(new Phrase("-"));
            table.AddCell(p_cell_totalQuantite);
            table.AddCell(new Phrase("-"));
            table.AddCell(p_cell_totalPT);

            doc.Add(new Paragraph(Environment.NewLine));

            doc.Add(table);
        }

        private void CreerTableauDettesPayeesUN(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"2.\tDETTES PAYEES DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 40, 30, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_client = new PdfPCell(new Phrase("Client", police_titre_tableau));
            PdfPCell p_cell_telephone = new PdfPCell(new Phrase("Téléphone", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_client);
            table.AddCell(p_cell_telephone);
            table.AddCell(p_cell_montant);
            // parcours les dettees

            foreach (DataRow row in new Facture().GetDettesPayéesServiceUN(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
                table.AddCell(new Phrase(Convert.ToString(row[3])));
            }

            doc.Add(new Paragraph(Environment.NewLine));
            decimal totalDettesPayees = new Facture().GetTotalDettesPayeesServiceUN(date1, date2);
            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total dettes payées", police_titre_tableau))
            {
                Colspan = 3
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalDettesPayees.ToString(), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauDettesNonPayeesUN(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"3.\tLES CLIENTS AYANT DES DETTES NON PAYEES DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 40, 30, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_client = new PdfPCell(new Phrase("Client", police_titre_tableau));
            PdfPCell p_cell_telephone = new PdfPCell(new Phrase("Téléphone", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant Dettes", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_client);
            table.AddCell(p_cell_telephone);
            table.AddCell(p_cell_montant);
            // parcours les dettees

            foreach (DataRow row in new Facture().GetDettesNONPayéesServiceUN(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
                table.AddCell(new Phrase(Convert.ToString(row[3])));
            }

            doc.Add(new Paragraph(Environment.NewLine));

            decimal totalReste = new Facture().GetResteApayerServiceUN(date1, date2);

            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total dettes non payées", police_titre_tableau))
            {
                Colspan = 3
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalReste.ToString("c"), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauDepensesUN(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"4.\tLES DEPENSES JOURNALIERES DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 60, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_designation = new PdfPCell(new Phrase("Désignation", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant ", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_designation);
            table.AddCell(p_cell_montant);
            // parcours les dettees

            foreach (DataRow row in new Dépenses().GetDetailsDepensesTableServiceUN(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
            }

            doc.Add(new Paragraph(Environment.NewLine));
            decimal totalDepense = new Dépenses().GetTotalJournalierDepenseServiceUN(date1, date2);
            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total Dépenses", police_titre_tableau))
            {
                Colspan = 2
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalDepense.ToString(), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauReductionUN(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"4.\tLES REDUCTIONS DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(7)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 30, 20, 15, 10, 15, 15 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_noms = new PdfPCell(new Phrase("Noms", police_titre_tableau));
            PdfPCell p_cell_telephone = new PdfPCell(new Phrase("Téléphone", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant ", police_titre_tableau));
            PdfPCell p_cell_taux = new PdfPCell(new Phrase("Taux ", police_titre_tableau));
            PdfPCell p_cell_montant_reduction = new PdfPCell(new Phrase("Réduction ", police_titre_tableau));
            PdfPCell p_cell_montant_solde = new PdfPCell(new Phrase("Montant solde ", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_noms);
            table.AddCell(p_cell_telephone);
            table.AddCell(p_cell_montant);

            table.AddCell(p_cell_taux);
            table.AddCell(p_cell_montant_reduction);
            table.AddCell(p_cell_montant_solde);

            // parcours les dettees

            foreach (DataRow row in new Reduction().GetReductionJournalierServiceUNTable(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
                table.AddCell(new Phrase(Convert.ToString(row[3])));
                table.AddCell(new Phrase(Convert.ToString(row[4])));
                table.AddCell(new Phrase(Convert.ToString(row[5])));
                table.AddCell(new Phrase(Convert.ToString(row[6])));
            }

            doc.Add(new Paragraph(Environment.NewLine));
            decimal totalApayer = new Reduction().GetTotalApayerJournalierServiceUN(date1, date2);
            decimal totalReduction = new Reduction().GetTotalReductionJournalierServiceUN(date1, date2);
            decimal totalSolde = totalApayer - totalReduction;

            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total Dépenses", police_titre_tableau))
            {
                Colspan = 3
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalApayer.ToString(), police_titre_tableau));
            table.AddCell(new Phrase("-", police_titre_tableau));
            table.AddCell(new Phrase(totalReduction.ToString(), police_titre_tableau));
            table.AddCell(new Phrase(totalSolde.ToString(), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauSyntheseUN(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"5.\tLA SYNTHESE DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 60, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_designation = new PdfPCell(new Phrase("Désignation", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant ", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_designation);
            table.AddCell(p_cell_montant);

            decimal total_vente_journaliere = new FactureDetailsPressing().ObtenirTotalAPayerService1(date1, date2);
            decimal total_argent_percu = new FactureDetailsPressing().ObtenirTotalPayeService1(date1, date2);
            decimal dette_paye = new Facture().GetTotalDettesPayeesServiceUN(date1, date2);
            decimal total_depense = new Dépenses().GetTotalJournalierDepenseServiceUN(date1, date2);

            decimal totalReduction = new Reduction().GetTotalReductionJournalierServiceUN(date1, date2);
            decimal solde = total_argent_percu + dette_paye - total_depense - totalReduction;

            table.AddCell(new Phrase("01"));
            table.AddCell(new Phrase("Ventes Journalières"));
            table.AddCell(new Phrase(total_vente_journaliere.ToString("c")));

            table.AddCell(new Phrase("02"));
            table.AddCell(new Phrase("Argent perçu"));
            table.AddCell(new Phrase(total_argent_percu.ToString("c")));

            table.AddCell(new Phrase("03"));
            table.AddCell(new Phrase("Dettes payées"));
            table.AddCell(new Phrase(dette_paye.ToString("c")));

            table.AddCell(new Phrase("04"));
            table.AddCell(new Phrase("Réduction accordée"));
            table.AddCell(new Phrase(totalReduction.ToString("c")));

            table.AddCell(new Phrase("05"));
            table.AddCell(new Phrase("Dépenses Journalières"));
            table.AddCell(new Phrase(total_depense.ToString("c")));

            table.AddCell(new Phrase("06"));
            table.AddCell(new Phrase("Solde "));
            table.AddCell(new Phrase(solde.ToString("c")));

            doc.Add(new Paragraph(Environment.NewLine));

            doc.Add(table);
        }

        #endregion SERVICE 1

        #region SERVICE 2

        private void CreerTableauVenteJournaliereDEUX(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"1.\tVENTE JOURNALIERE DU {date1} ", police_titre_tableau);

            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(10)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 9, 26, 23, 15, 15, 15, 17, 8, 15, 15 });

            PdfPCell cell_numero = new PdfPCell(new Phrase("N°", police_cellule_tableau));
            PdfPCell cell_client = new PdfPCell(new Phrase("Client", police_cellule_tableau));
            PdfPCell cell_telephone = new PdfPCell(new Phrase("Téléphone", police_cellule_tableau));
            PdfPCell cell_a_payer = new PdfPCell(new Phrase("à payer", police_cellule_tableau));
            PdfPCell cell_paye = new PdfPCell(new Phrase("Payé", police_cellule_tableau));
            PdfPCell cell_reste = new PdfPCell(new Phrase("Reste", police_cellule_tableau));
            PdfPCell cell_Article = new PdfPCell(new Phrase("Article", police_cellule_tableau));
            PdfPCell cell_quantite = new PdfPCell(new Phrase("QT", police_cellule_tableau));
            PdfPCell cell_prix_unitaire = new PdfPCell(new Phrase("PU", police_cellule_tableau));
            PdfPCell cell_prix_total = new PdfPCell(new Phrase("PT", police_cellule_tableau));

            //ajout en-tête document
            table.AddCell(cell_numero);
            table.AddCell(cell_client);
            table.AddCell(cell_telephone);
            table.AddCell(cell_a_payer);
            table.AddCell(cell_paye);
            table.AddCell(cell_reste);
            table.AddCell(cell_Article);
            table.AddCell(cell_quantite);
            table.AddCell(cell_prix_unitaire);
            table.AddCell(cell_prix_total);

            //parcours des factures
            var listeDesFacture = new Facture().ObtenirLesFactureJournalieresServiceDEUX(date1, date2);

            foreach (DataRow facture in listeDesFacture.Rows)
            {
                DataTable tableDesDonnes = new FactureDetailsPressing().GetFacturePressingDetailsTable(Convert.ToInt64(facture[0]));

                int nombreDesArticles = tableDesDonnes.Rows.Count;

                cell_numero = new PdfPCell
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[0].ToString(), font),
                    HorizontalAlignment = 1
                };
                cell_client = new PdfPCell
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[1].ToString(), font),
                };
                cell_telephone = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(Convert.ToString(facture[2]).ToString(), font),
                    VerticalAlignment = 1
                };
                cell_a_payer = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[3].ToString(), font),
                    VerticalAlignment = 1
                };
                cell_paye = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[4].ToString(), font),
                    VerticalAlignment = 1
                };
                cell_reste = new PdfPCell()
                {
                    Rowspan = nombreDesArticles,
                    Phrase = new Phrase(facture[5].ToString(), font),
                    VerticalAlignment = 1
                };

                table.AddCell(cell_numero);
                table.AddCell(cell_client);
                table.AddCell(cell_telephone);
                table.AddCell(cell_a_payer);
                table.AddCell(cell_paye);
                table.AddCell(cell_reste);

                foreach (DataRow row in tableDesDonnes.Rows)
                {
                    table.AddCell(new Phrase(Convert.ToString(row[0]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[1]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[2]), font));
                    table.AddCell(new Phrase(Convert.ToString(row[3]), font));
                }
                tableDesDonnes.Rows.Clear();
            }
            //ajout les totaux

            decimal totalApayer = new FactureDetailsPressing().ObtenirTotalAPayerService2(date1, date2);
            decimal totalPaye = new FactureDetailsPressing().ObtenirTotalPayeService2(date1, date2);
            decimal totalReste = new Facture().GetResteApayerServiceDEUX(date1, date2);
            long nombreDesHabits = new FactureDetailsPressing().ObtenirTotalNombreHabillesService2(date1, date2);

            PdfPCell p_cell_des = new PdfPCell(new Phrase("Total ventes journalières", police_titre_tableau))
            {
                Colspan = 3
            };
            PdfPCell p_cell_totalApayer = new PdfPCell(new Phrase(totalApayer.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalPaye = new PdfPCell(new Phrase(totalPaye.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalReste = new PdfPCell(new Phrase(totalReste.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalQuantite = new PdfPCell(new Phrase(nombreDesHabits.ToString(), police_titre_tableau));
            PdfPCell p_cell_totalPT = new PdfPCell(new Phrase(totalApayer.ToString(), police_titre_tableau));

            table.AddCell(p_cell_des);
            table.AddCell(p_cell_totalApayer);
            table.AddCell(p_cell_totalPaye);
            table.AddCell(p_cell_totalReste);
            table.AddCell(new Phrase("-"));
            table.AddCell(p_cell_totalQuantite);
            table.AddCell(new Phrase("-"));
            table.AddCell(p_cell_totalPT);

            doc.Add(new Paragraph(Environment.NewLine));

            doc.Add(table);
        }

        private void CreerTableauDettesPayeesDEUX(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"2.\tDETTES PAYEES DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 40, 30, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_client = new PdfPCell(new Phrase("Client", police_titre_tableau));
            PdfPCell p_cell_telephone = new PdfPCell(new Phrase("Téléphone", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_client);
            table.AddCell(p_cell_telephone);
            table.AddCell(p_cell_montant);
            // parcours les dettees

            foreach (DataRow row in new Facture().GetDettesPayéesServiceDEUX(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
                table.AddCell(new Phrase(Convert.ToString(row[3])));
            }

            doc.Add(new Paragraph(Environment.NewLine));
            decimal totalDettesPayees = new Facture().GetTotalDettesPayeesServiceDEUX(date1, date2);
            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total dettes payées", police_titre_tableau))
            {
                Colspan = 3
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalDettesPayees.ToString(), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauDettesNonPayeesDEUX(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"3.\tLES CLIENTS AYANT DES DETTES NON PAYEES DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(4)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 40, 30, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_client = new PdfPCell(new Phrase("Client", police_titre_tableau));
            PdfPCell p_cell_telephone = new PdfPCell(new Phrase("Téléphone", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant Dettes", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_client);
            table.AddCell(p_cell_telephone);
            table.AddCell(p_cell_montant);
            // parcours les dettees

            foreach (DataRow row in new Facture().GetDettesNONPayéesServiceDEUX(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
                table.AddCell(new Phrase(Convert.ToString(row[3])));
            }

            doc.Add(new Paragraph(Environment.NewLine));

            decimal totalDettesNonPayees = new Facture().GetResteApayerServiceDEUX(date1, date2);
            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total dettes non payées", police_titre_tableau))
            {
                Colspan = 3
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalDettesNonPayees.ToString("c"), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauDepensesDEUX(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"4.\tLES DEPENSES JOURNALIERES DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 60, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_designation = new PdfPCell(new Phrase("Désignation", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant ", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_designation);
            table.AddCell(p_cell_montant);
            // parcours les dettees

            foreach (DataRow row in new Dépenses().GetDetailsDepensesTableServiceDEUX(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
            }

            doc.Add(new Paragraph(Environment.NewLine));
            decimal totalDepense = new Dépenses().GetTotalJournalierDepenseServiceDEUX(date1, date2);
            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total Dépenses", police_titre_tableau))
            {
                Colspan = 2
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalDepense.ToString(), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauReductionDEUX(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"4.\tLES REDUCTIONS DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(7)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 30, 20, 15, 10, 15, 15 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_noms = new PdfPCell(new Phrase("Noms", police_titre_tableau));
            PdfPCell p_cell_telephone = new PdfPCell(new Phrase("Téléphone", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant ", police_titre_tableau));
            PdfPCell p_cell_taux = new PdfPCell(new Phrase("Taux ", police_titre_tableau));
            PdfPCell p_cell_montant_reduction = new PdfPCell(new Phrase("Réduction ", police_titre_tableau));
            PdfPCell p_cell_montant_solde = new PdfPCell(new Phrase("Montant solde ", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_noms);
            table.AddCell(p_cell_telephone);
            table.AddCell(p_cell_montant);

            table.AddCell(p_cell_taux);
            table.AddCell(p_cell_montant_reduction);
            table.AddCell(p_cell_montant_solde);

            // parcours les dettees

            foreach (DataRow row in new Reduction().GetReductionJournalierServiceDEUXTable(date1, date2).Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
                table.AddCell(new Phrase(Convert.ToString(row[3])));
                table.AddCell(new Phrase(Convert.ToString(row[4])));
                table.AddCell(new Phrase(Convert.ToString(row[5])));
                table.AddCell(new Phrase(Convert.ToString(row[6])));
            }

            doc.Add(new Paragraph(Environment.NewLine));
            decimal totalApayer = new Reduction().GetTotalApayerJournalierServiceDEUX(date1, date2);
            decimal totalReduction = new Reduction().GetTotalReductionJournalierServiceDEUX(date1, date2);
            decimal totalSolde = totalApayer - totalReduction;

            PdfPCell p_cell_total_description = new PdfPCell(new Phrase("Total Dépenses", police_titre_tableau))
            {
                Colspan = 3
            };

            table.AddCell(p_cell_total_description);
            table.AddCell(new Phrase(totalApayer.ToString(), police_titre_tableau));
            table.AddCell(new Phrase("-", police_titre_tableau));
            table.AddCell(new Phrase(totalReduction.ToString(), police_titre_tableau));
            table.AddCell(new Phrase(totalSolde.ToString(), police_titre_tableau));

            doc.Add(table);
        }

        private void CreerTableauSyntheseDEUX(Document doc, DateTime date1, DateTime date2)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"5.\tLA SYNTHESE DU {date1} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 10, 60, 20 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_designation = new PdfPCell(new Phrase("Désignation", police_titre_tableau));
            PdfPCell p_cell_montant = new PdfPCell(new Phrase("Montant ", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_designation);
            table.AddCell(p_cell_montant);

            decimal total_vente_journaliere = new FactureDetailsPressing().ObtenirTotalAPayerService2(date1, date2);

            decimal total_argent_percu = new FactureDetailsPressing().ObtenirTotalPayeService2(date1, date2); ;
            decimal dette_paye = new Facture().GetTotalDettesPayeesServiceDEUX(date1, date2);
            decimal total_depense = new Dépenses().GetTotalJournalierDepenseServiceDEUX(date1, date2);
            decimal totalReduction = new Reduction().GetTotalReductionJournalierServiceDEUX(date1, date2);

            decimal solde = total_argent_percu + dette_paye - total_depense - totalReduction;

            table.AddCell(new Phrase("01"));
            table.AddCell(new Phrase("Vente Journalier"));
            table.AddCell(new Phrase(total_vente_journaliere.ToString("c")));

            table.AddCell(new Phrase("02"));
            table.AddCell(new Phrase("Argent perçu"));
            table.AddCell(new Phrase(total_argent_percu.ToString("c")));

            table.AddCell(new Phrase("03"));
            table.AddCell(new Phrase("Dettes payées"));
            table.AddCell(new Phrase(dette_paye.ToString("c")));

            table.AddCell(new Phrase("04"));
            table.AddCell(new Phrase("Dépenses Journalières"));
            table.AddCell(new Phrase(total_depense.ToString("c")));

            table.AddCell(new Phrase("05"));
            table.AddCell(new Phrase("Réduction accordée"));
            table.AddCell(new Phrase(totalReduction.ToString("c")));

            table.AddCell(new Phrase("06"));
            table.AddCell(new Phrase("Solde= (02) - (04)"));
            table.AddCell(new Phrase(solde.ToString("c")));

            doc.Add(new Paragraph(Environment.NewLine));

            doc.Add(table);
        }

        #endregion SERVICE 2

        #endregion LES METHODES DES SERVICES

        #region TOUS LES DEUX SERVICES

        private void CreerTableauObservation(Document doc)
        {
            police_titre_tableau.SetFamily(FontFactory.TIMES);
            Paragraph p_titre = new Paragraph($"4.\tLES OBSERVATIONS DU {DateTime.Now} ", police_titre_tableau);

            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(new Paragraph(Environment.NewLine));
            doc.Add(p_titre);

            //créattion du tableau
            PdfPTable table = new PdfPTable(3)
            {
                WidthPercentage = 100
            };
            table.SetWidths(new float[] { 6, 45, 15 });

            PdfPCell p_cell_numero = new PdfPCell(new Phrase("N°", police_titre_tableau));
            PdfPCell p_cell_observation = new PdfPCell(new Phrase("Observations", police_titre_tableau));
            PdfPCell p_cell_date = new PdfPCell(new Phrase("Date et Heure", police_titre_tableau));

            table.AddCell(p_cell_numero);
            table.AddCell(p_cell_observation);
            table.AddCell(p_cell_date);
            // parcours les dettees

            foreach (DataRow row in new Observation().GetObsertionJournalieres().Rows)
            {
                table.AddCell(new Phrase(Convert.ToString(row[0])));
                table.AddCell(new Phrase(Convert.ToString(row[1])));
                table.AddCell(new Phrase(Convert.ToString(row[2])));
            }

            doc.Add(new Paragraph(Environment.NewLine));

            doc.Add(table);
        }

        #endregion TOUS LES DEUX SERVICES
    }
}