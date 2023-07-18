namespace Dieu_voit_tout
{
    partial class FrmFacturation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFacturation));
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_code = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.txt_designation = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.txt_prix_unitaire = new Syncfusion.Windows.Forms.Tools.CurrencyTextBox();
            this.txt_quantite = new Syncfusion.Windows.Forms.Tools.IntegerTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_client = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.dgvListe = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_telephone = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.lbl_total_general = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txt_code)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_designation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_prix_unitaire)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_quantite)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_client)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_telephone)).BeginInit();
            this.SuspendLayout();
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(130, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 18;
            this.label5.Text = "Quantité";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(110, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Prix Unitaire";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(110, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 16;
            this.label3.Text = "Désignation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(112, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 15;
            this.label2.Text = "Code barre";
            // 
            // txt_code
            // 
            this.txt_code.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_code.Location = new System.Drawing.Point(200, 53);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(188, 25);
            this.txt_code.TabIndex = 11;
            this.txt_code.TextChanged += new System.EventHandler(this.Txt_code_TextChanged);
            // 
            // txt_designation
            // 
            this.txt_designation.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_designation.Enabled = false;
            this.txt_designation.Location = new System.Drawing.Point(199, 84);
            this.txt_designation.Name = "txt_designation";
            this.txt_designation.Size = new System.Drawing.Size(188, 25);
            this.txt_designation.TabIndex = 12;
            // 
            // txt_prix_unitaire
            // 
            this.txt_prix_unitaire.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_prix_unitaire.DecimalValue = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.txt_prix_unitaire.Location = new System.Drawing.Point(199, 144);
            this.txt_prix_unitaire.Name = "txt_prix_unitaire";
            this.txt_prix_unitaire.Size = new System.Drawing.Size(187, 25);
            this.txt_prix_unitaire.TabIndex = 14;
            this.txt_prix_unitaire.Text = "1,00 €";
            this.txt_prix_unitaire.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_quantite
            // 
            this.txt_quantite.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_quantite.IntegerValue = ((long)(1));
            this.txt_quantite.Location = new System.Drawing.Point(199, 115);
            this.txt_quantite.Name = "txt_quantite";
            this.txt_quantite.Size = new System.Drawing.Size(188, 25);
            this.txt_quantite.TabIndex = 13;
            this.txt_quantite.Text = "1";
            this.txt_quantite.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(610, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 20;
            this.label1.Text = "Client";
            // 
            // txt_client
            // 
            this.txt_client.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_client.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_client.Location = new System.Drawing.Point(656, 56);
            this.txt_client.Name = "txt_client";
            this.txt_client.Size = new System.Drawing.Size(188, 25);
            this.txt_client.TabIndex = 19;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Enabled = false;
            this.BtnAdd.Location = new System.Drawing.Point(418, 144);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(62, 26);
            this.BtnAdd.TabIndex = 21;
            this.BtnAdd.Text = "Ajouter";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // dgvListe
            // 
            this.dgvListe.BackgroundColor = System.Drawing.Color.White;
            this.dgvListe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvListe.Location = new System.Drawing.Point(53, 218);
            this.dgvListe.Name = "dgvListe";
            this.dgvListe.Size = new System.Drawing.Size(791, 321);
            this.dgvListe.TabIndex = 22;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(563, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 24;
            this.label6.Text = "N° Téléphone";
            // 
            // txt_telephone
            // 
            this.txt_telephone.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_telephone.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_telephone.Location = new System.Drawing.Point(656, 88);
            this.txt_telephone.Name = "txt_telephone";
            this.txt_telephone.Size = new System.Drawing.Size(188, 25);
            this.txt_telephone.TabIndex = 23;
            // 
            // lbl_total_general
            // 
            this.lbl_total_general.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_total_general.AutoSize = true;
            this.lbl_total_general.Location = new System.Drawing.Point(829, 557);
            this.lbl_total_general.Name = "lbl_total_general";
            this.lbl_total_general.Size = new System.Drawing.Size(15, 17);
            this.lbl_total_general.TabIndex = 25;
            this.lbl_total_general.Text = "0";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(656, 128);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(188, 59);
            this.button1.TabIndex = 26;
            this.button1.Text = "Imprimer";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // FrmFacturation
            // 
            this.AcceptButton = this.BtnAdd;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(905, 599);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbl_total_general);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_telephone);
            this.Controls.Add(this.dgvListe);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_client);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.txt_designation);
            this.Controls.Add(this.txt_prix_unitaire);
            this.Controls.Add(this.txt_quantite);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmFacturation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Facturation";
            ((System.ComponentModel.ISupportInitialize)(this.txt_code)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_designation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_prix_unitaire)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_quantite)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_client)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_telephone)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txt_code;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txt_designation;
        private Syncfusion.Windows.Forms.Tools.CurrencyTextBox txt_prix_unitaire;
        private Syncfusion.Windows.Forms.Tools.IntegerTextBox txt_quantite;
        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txt_client;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.DataGridView dgvListe;
        private System.Windows.Forms.Label label6;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txt_telephone;
        private System.Windows.Forms.Label lbl_total_general;
        private System.Windows.Forms.Button button1;
    }
}