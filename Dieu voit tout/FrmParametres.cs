using Syncfusion.WinForms.Input.Styles;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dieu_voit_tout
{
    public partial class FrmParametres : Form
    {
        public FrmParametres()
        {
            InitializeComponent();
            GetDateFromKeys();
        }

        private void GetDateFromKeys()
        {
            txt_phone_number.Text = System.Configuration.ConfigurationManager.AppSettings["cellPhone"];
            txt_society_name.Text = System.Configuration.ConfigurationManager.AppSettings["entrepriseName"];
            txt_society_adress.Text = System.Configuration.ConfigurationManager.AppSettings["addresseEntreprise"];
            txt_rccm.Text = System.Configuration.ConfigurationManager.AppSettings["Rccm"];
            txt_database.Text = System.Configuration.ConfigurationManager.AppSettings["database"];

            txt_server_dress.Text = System.Configuration.ConfigurationManager.AppSettings["serverName"];
            txt_port.Text = System.Configuration.ConfigurationManager.AppSettings["port"];
            txt_username.Text = System.Configuration.ConfigurationManager.AppSettings["userName"];
            txt_user_password.Text = System.Configuration.ConfigurationManager.AppSettings["password"];
            txt_mention.Text = System.Configuration.ConfigurationManager.AppSettings["mention"];
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        private void Save()
        {
            DialogResult result = MessageBox.Show("Vous êtes sur le point d'enregistrer le paramètres de configuration.\n\nVoulez-vous vrament continuer ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                AddUpdateAppSettings("port", txt_port.Text);
                AddUpdateAppSettings("serverName", txt_server_dress.Text);
                AddUpdateAppSettings("userName", txt_username.Text);
                AddUpdateAppSettings("Password", txt_user_password.Text);
                AddUpdateAppSettings("database", txt_database.Text);

                AddUpdateAppSettings("cellPhone", txt_phone_number.Text);
                AddUpdateAppSettings("entrepriseName", txt_society_name.Text);
                AddUpdateAppSettings("addresseEntreprise", txt_society_adress.Text);
                AddUpdateAppSettings("Rccm", txt_rccm.Text);
                AddUpdateAppSettings("mention", txt_mention.Text);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void BtnSauvegarder_Click(object sender, EventArgs e)
        {
            Dieu_voit_toutHelperr.Helper.Backup();
        }
    }
}
