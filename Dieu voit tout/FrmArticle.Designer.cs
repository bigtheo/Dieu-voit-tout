namespace Helper
{
    partial class FrmArticle
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.BtnNouveau = new System.Windows.Forms.Button();
            this.BtnModifier = new System.Windows.Forms.Button();
            this.BtnImprimer = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.dgvListe = new Syncfusion.WinForms.DataGrid.SfDataGrid();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.Silver;
            this.panel1.Location = new System.Drawing.Point(21, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(595, 1);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Articles";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.BtnImprimer);
            this.panel2.Controls.Add(this.BtnModifier);
            this.panel2.Controls.Add(this.BtnNouveau);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 484);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(679, 51);
            this.panel2.TabIndex = 4;
            // 
            // BtnNouveau
            // 
            this.BtnNouveau.Location = new System.Drawing.Point(22, 14);
            this.BtnNouveau.Name = "BtnNouveau";
            this.BtnNouveau.Size = new System.Drawing.Size(83, 26);
            this.BtnNouveau.TabIndex = 5;
            this.BtnNouveau.Text = "Nouveau";
            this.BtnNouveau.UseVisualStyleBackColor = true;
            this.BtnNouveau.Click += new System.EventHandler(this.BtnNouveau_Click);
            // 
            // BtnModifier
            // 
            this.BtnModifier.Location = new System.Drawing.Point(114, 14);
            this.BtnModifier.Name = "BtnModifier";
            this.BtnModifier.Size = new System.Drawing.Size(83, 26);
            this.BtnModifier.TabIndex = 6;
            this.BtnModifier.Text = "Modifier";
            this.BtnModifier.UseVisualStyleBackColor = true;
            // 
            // BtnImprimer
            // 
            this.BtnImprimer.Location = new System.Drawing.Point(203, 14);
            this.BtnImprimer.Name = "BtnImprimer";
            this.BtnImprimer.Size = new System.Drawing.Size(83, 26);
            this.BtnImprimer.TabIndex = 7;
            this.BtnImprimer.Text = "Imprimer";
            this.BtnImprimer.UseVisualStyleBackColor = true;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(525, 9);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(91, 25);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // dgvListe
            // 
            this.dgvListe.AccessibleName = "Table";
            this.dgvListe.AllowEditing = false;
            this.dgvListe.AllowFiltering = true;
            this.dgvListe.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvListe.Location = new System.Drawing.Point(22, 73);
            this.dgvListe.Name = "dgvListe";
            this.dgvListe.Size = new System.Drawing.Size(620, 373);
            this.dgvListe.TabIndex = 6;
            this.dgvListe.Text = "sfDataGrid1";
            // 
            // FrmArticle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(679, 535);
            this.Controls.Add(this.dgvListe);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmArticle";
            this.Text = "FrmArticle";
            this.Load += new System.EventHandler(this.FrmArticle_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvListe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button BtnModifier;
        private System.Windows.Forms.Button BtnNouveau;
        private System.Windows.Forms.Button BtnImprimer;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private Syncfusion.WinForms.DataGrid.SfDataGrid dgvListe;
    }
}