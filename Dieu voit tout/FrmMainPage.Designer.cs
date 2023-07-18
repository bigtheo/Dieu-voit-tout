namespace Dieu_voit_tout
{
    partial class FrmMainPage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainPage));
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnArticle = new System.Windows.Forms.Button();
            this.BtnStock = new System.Windows.Forms.Button();
            this.BtnVente = new System.Windows.Forms.Button();
            this.BtnRapports = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panelContainer = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.BtnRapports);
            this.panel1.Controls.Add(this.BtnVente);
            this.panel1.Controls.Add(this.BtnStock);
            this.panel1.Controls.Add(this.BtnArticle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 574);
            this.panel1.TabIndex = 0;
            // 
            // BtnArticle
            // 
            this.BtnArticle.FlatAppearance.BorderSize = 0;
            this.BtnArticle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnArticle.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnArticle.Image = ((System.Drawing.Image)(resources.GetObject("BtnArticle.Image")));
            this.BtnArticle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnArticle.Location = new System.Drawing.Point(22, 152);
            this.BtnArticle.Name = "BtnArticle";
            this.BtnArticle.Size = new System.Drawing.Size(161, 43);
            this.BtnArticle.TabIndex = 0;
            this.BtnArticle.Text = "Article";
            this.BtnArticle.UseVisualStyleBackColor = true;
            this.BtnArticle.Click += new System.EventHandler(this.BtnArticle_Click);
            // 
            // BtnStock
            // 
            this.BtnStock.FlatAppearance.BorderSize = 0;
            this.BtnStock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStock.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnStock.Image = ((System.Drawing.Image)(resources.GetObject("BtnStock.Image")));
            this.BtnStock.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnStock.Location = new System.Drawing.Point(22, 214);
            this.BtnStock.Name = "BtnStock";
            this.BtnStock.Size = new System.Drawing.Size(161, 43);
            this.BtnStock.TabIndex = 1;
            this.BtnStock.Text = "Stock";
            this.BtnStock.UseVisualStyleBackColor = true;
            this.BtnStock.Click += new System.EventHandler(this.BtnStock_Click);
            // 
            // BtnVente
            // 
            this.BtnVente.FlatAppearance.BorderSize = 0;
            this.BtnVente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnVente.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnVente.Image = ((System.Drawing.Image)(resources.GetObject("BtnVente.Image")));
            this.BtnVente.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnVente.Location = new System.Drawing.Point(22, 273);
            this.BtnVente.Name = "BtnVente";
            this.BtnVente.Size = new System.Drawing.Size(161, 43);
            this.BtnVente.TabIndex = 2;
            this.BtnVente.Text = "Vente";
            this.BtnVente.UseVisualStyleBackColor = true;
            this.BtnVente.Click += new System.EventHandler(this.BtnVente_Click);
            // 
            // BtnRapports
            // 
            this.BtnRapports.FlatAppearance.BorderSize = 0;
            this.BtnRapports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnRapports.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnRapports.Image = ((System.Drawing.Image)(resources.GetObject("BtnRapports.Image")));
            this.BtnRapports.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnRapports.Location = new System.Drawing.Point(22, 336);
            this.BtnRapports.Name = "BtnRapports";
            this.BtnRapports.Size = new System.Drawing.Size(161, 43);
            this.BtnRapports.TabIndex = 3;
            this.BtnRapports.Text = "Rapports";
            this.BtnRapports.UseVisualStyleBackColor = true;
            this.BtnRapports.Click += new System.EventHandler(this.BtnRapports_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Image = ((System.Drawing.Image)(resources.GetObject("button6.Image")));
            this.button6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button6.Location = new System.Drawing.Point(22, 502);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(161, 43);
            this.button6.TabIndex = 5;
            this.button6.Text = "Settings";
            this.button6.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 97);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 22);
            this.label1.TabIndex = 6;
            this.label1.Text = "Dieu voit Software";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(59, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(74, 56);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.panel2.Location = new System.Drawing.Point(23, 488);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(148, 1);
            this.panel2.TabIndex = 8;
            // 
            // panelContainer
            // 
            this.panelContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContainer.Location = new System.Drawing.Point(202, 0);
            this.panelContainer.Name = "panelContainer";
            this.panelContainer.Size = new System.Drawing.Size(695, 574);
            this.panelContainer.TabIndex = 1;
            // 
            // FrmMainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(897, 574);
            this.Controls.Add(this.panelContainer);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmMainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu principal";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnArticle;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button BtnRapports;
        private System.Windows.Forms.Button BtnVente;
        private System.Windows.Forms.Button BtnStock;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panelContainer;
    }
}