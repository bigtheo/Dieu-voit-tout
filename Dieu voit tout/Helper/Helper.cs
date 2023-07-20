using Dieu_voit_tout;
using iTextSharp.text;
using MySqlConnector;
using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace Dieu_voit_toutHelperr
{
    public static class Helper
    {
        #region information de l'entité

        private static string nomEntite;

        public static string GetNomEntite()
        {
            return System.Configuration.ConfigurationManager.AppSettings["entrepriseName"];
        }

        private static void SetNomEntite(string value)
        {
            nomEntite = value;
        }

        public static string AdresseEntite { get; private set; }
        public static string RccmEntite { get; private set; }
        public static string Cordonnees { get; private set; }

        #endregion information de l'entité

        public static void AddEntete(Document document)
        {
            #region la police

            iTextSharp.text.Font police = FontFactory.GetFont("TIMES NEW ROMAN", 16, 1, new BaseColor(31, 50, 64));
            police.SetStyle(iTextSharp.text.Font.BOLD);

            #endregion la police

            ChargerInformationDeLentreprise();

            string entite = $"{GetNomEntite()}";
            string Adresse = AdresseEntite;

            Paragraph p_entete = new Paragraph(entite, police)
            {
                Alignment = Element.ALIGN_CENTER
            };

            police = FontFactory.GetFont("TIMES NEW ROMAN", 7, 1, new BaseColor(31, 50, 64));
            Chunk c_adresse = new Chunk(Adresse, police);
            c_adresse.SetUnderline(0.2f, -2f);

            Paragraph p_adresse = new Paragraph(c_adresse)
            {
                Alignment = Element.ALIGN_CENTER
            };
            iTextSharp.text.Font font = FontFactory.GetFont("TIMES NEW ROMAN", 8, 1, new BaseColor(31, 50, 64));

            document.Add(p_entete);
            document.Add(p_adresse);
        }

        private static void ChargerInformationDeLentreprise()
        {
            nomEntite = System.Configuration.ConfigurationManager.AppSettings["entrepriseName"];
            AdresseEntite = System.Configuration.ConfigurationManager.AppSettings["addresseEntreprise"];
            RccmEntite = System.Configuration.ConfigurationManager.AppSettings["Rccm"];
            Cordonnees = System.Configuration.ConfigurationManager.AppSettings["addresseEntreprise"];
        }

        #region la sauvergarde de la base des données

        public static void Backup()
        {
            
            string nom_du_fichier = DateTime.Now.Date.ToString("Invoices backup dd MMM yyyy HH mm") + ".sql";
            string file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + nom_du_fichier;

            using (MySqlCommand cmd = new MySqlCommand())
            {
                using (MySqlBackup mb = new MySqlBackup(cmd))
                {
                    Connexion.Connecter();
                    cmd.Connection = Connexion.Con;
                    mb.ExportToFile(file);
                    MessageBox.Show("Sauvegarde éffectuée avec succès !!!", "Infrmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion la sauvergarde de la base des données
    }
}