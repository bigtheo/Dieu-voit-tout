namespace Helper
{
    partial class FrmLogin
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLogin));
            this.textBoxExt1 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnFermer = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxExt2 = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.BtnConnection = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt2)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxExt1
            // 
            this.textBoxExt1.BeforeTouchSize = new System.Drawing.Size(320, 34);
            this.textBoxExt1.Location = new System.Drawing.Point(45, 149);
            this.textBoxExt1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxExt1.Name = "textBoxExt1";
            this.textBoxExt1.Size = new System.Drawing.Size(320, 34);
            this.textBoxExt1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.panel1.Controls.Add(this.BtnFermer);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(433, 65);
            this.panel1.TabIndex = 1;
            // 
            // BtnFermer
            // 
            this.BtnFermer.FlatAppearance.BorderSize = 0;
            this.BtnFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFermer.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFermer.ForeColor = System.Drawing.Color.White;
            this.BtnFermer.Location = new System.Drawing.Point(377, 14);
            this.BtnFermer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnFermer.Name = "BtnFermer";
            this.BtnFermer.Size = new System.Drawing.Size(43, 39);
            this.BtnFermer.TabIndex = 101;
            this.BtnFermer.Text = "X";
            this.BtnFermer.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(26, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(85, 37);
            this.label3.TabIndex = 6;
            this.label3.Text = "Login";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(44, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(99, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "Username";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 183);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 28);
            this.label2.TabIndex = 4;
            this.label2.Text = "Password";
            // 
            // textBoxExt2
            // 
            this.textBoxExt2.BeforeTouchSize = new System.Drawing.Size(320, 34);
            this.textBoxExt2.Location = new System.Drawing.Point(45, 209);
            this.textBoxExt2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBoxExt2.Name = "textBoxExt2";
            this.textBoxExt2.PasswordChar = '●';
            this.textBoxExt2.Size = new System.Drawing.Size(320, 34);
            this.textBoxExt2.TabIndex = 2;
            this.textBoxExt2.UseSystemPasswordChar = true;
            // 
            // BtnConnection
            // 
            this.BtnConnection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.BtnConnection.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnConnection.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.BtnConnection.Location = new System.Drawing.Point(130, 262);
            this.BtnConnection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnConnection.Name = "BtnConnection";
            this.BtnConnection.Size = new System.Drawing.Size(147, 41);
            this.BtnConnection.TabIndex = 3;
            this.BtnConnection.Text = "Connecter";
            this.BtnConnection.UseVisualStyleBackColor = false;
            this.BtnConnection.Click += new System.EventHandler(this.BtnConnection_Click);
            // 
            // FrmLogin
            // 
            this.AcceptButton = this.BtnConnection;
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 28F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(433, 345);
            this.Controls.Add(this.BtnConnection);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxExt2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBoxExt1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxExt2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt textBoxExt2;
        private System.Windows.Forms.Button BtnConnection;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnFermer;
    }
}

