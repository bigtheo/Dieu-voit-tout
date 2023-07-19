namespace Helper
{
    partial class FrmNewArticle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmNewArticle));
            this.label1 = new System.Windows.Forms.Label();
            this.txt_stock = new Syncfusion.Windows.Forms.Tools.IntegerTextBox();
            this.txt_pu = new Syncfusion.Windows.Forms.Tools.CurrencyTextBox();
            this.txt_designation = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.txt_code = new Syncfusion.Windows.Forms.Tools.TextBoxExt();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.txt_stock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_pu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_designation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_code)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(77, 47);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(154, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remplissez le formulaire";
            // 
            // txt_stock
            // 
            this.txt_stock.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_stock.IntegerValue = ((long)(0));
            this.txt_stock.Location = new System.Drawing.Point(111, 186);
            this.txt_stock.Name = "txt_stock";
            this.txt_stock.Size = new System.Drawing.Size(188, 25);
            this.txt_stock.TabIndex = 3;
            this.txt_stock.Text = "0";
            this.txt_stock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_pu
            // 
            this.txt_pu.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_pu.DecimalValue = new decimal(new int[] {
            100,
            0,
            0,
            131072});
            this.txt_pu.Location = new System.Drawing.Point(112, 155);
            this.txt_pu.Name = "txt_pu";
            this.txt_pu.Size = new System.Drawing.Size(187, 25);
            this.txt_pu.TabIndex = 2;
            this.txt_pu.Text = "1,00 €";
            this.txt_pu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txt_designation
            // 
            this.txt_designation.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_designation.Location = new System.Drawing.Point(111, 124);
            this.txt_designation.Name = "txt_designation";
            this.txt_designation.Size = new System.Drawing.Size(188, 25);
            this.txt_designation.TabIndex = 1;
            // 
            // txt_code
            // 
            this.txt_code.BeforeTouchSize = new System.Drawing.Size(188, 25);
            this.txt_code.Location = new System.Drawing.Point(112, 93);
            this.txt_code.Name = "txt_code";
            this.txt_code.Size = new System.Drawing.Size(188, 25);
            this.txt_code.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 17);
            this.label2.TabIndex = 7;
            this.label2.Text = "Code barre";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 17);
            this.label3.TabIndex = 8;
            this.label3.Text = "Désignation";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Prix Unitaire";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(60, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Stock";
            // 
            // BtnSave
            // 
            this.BtnSave.Location = new System.Drawing.Point(218, 226);
            this.BtnSave.Name = "BtnSave";
            this.BtnSave.Size = new System.Drawing.Size(82, 33);
            this.BtnSave.TabIndex = 5;
            this.BtnSave.Text = "Enregistrer";
            this.BtnSave.UseVisualStyleBackColor = true;
            this.BtnSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // FrmNewArticle
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(332, 297);
            this.Controls.Add(this.BtnSave);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_code);
            this.Controls.Add(this.txt_designation);
            this.Controls.Add(this.txt_pu);
            this.Controls.Add(this.txt_stock);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmNewArticle";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Article";
            this.Load += new System.EventHandler(this.FrmNewArticle_Load);
            ((System.ComponentModel.ISupportInitialize)(this.txt_stock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_pu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_designation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_code)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Syncfusion.Windows.Forms.Tools.IntegerTextBox txt_stock;
        private Syncfusion.Windows.Forms.Tools.CurrencyTextBox txt_pu;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txt_designation;
        private Syncfusion.Windows.Forms.Tools.TextBoxExt txt_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnSave;
    }
}