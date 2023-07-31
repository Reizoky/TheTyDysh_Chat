namespace TheTyDysh
{
    partial class frmInventory
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbInventoryEq = new System.Windows.Forms.ListBox();
            this.lblStats = new System.Windows.Forms.Label();
            this.lbCharacterEq = new System.Windows.Forms.ListBox();
            this.btnUnEquip = new System.Windows.Forms.Button();
            this.btnEquip = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
            this.tableLayoutPanel1.Controls.Add(this.lbInventoryEq, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblStats, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.lbCharacterEq, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.btnUnEquip, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.btnEquip, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label4, 4, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnClose, 2, 6);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 110F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(715, 443);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // lbInventoryEq
            // 
            this.lbInventoryEq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbInventoryEq.FormattingEnabled = true;
            this.lbInventoryEq.Location = new System.Drawing.Point(3, 53);
            this.lbInventoryEq.Name = "lbInventoryEq";
            this.tableLayoutPanel1.SetRowSpan(this.lbInventoryEq, 4);
            this.lbInventoryEq.Size = new System.Drawing.Size(208, 314);
            this.lbInventoryEq.TabIndex = 0;
            this.lbInventoryEq.Tag = "inventory";
            this.lbInventoryEq.SelectedIndexChanged += new System.EventHandler(this.lbCharacterEq_SelectedIndexChanged);
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.BackColor = System.Drawing.SystemColors.Window;
            this.lblStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStats.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblStats.Location = new System.Drawing.Point(502, 50);
            this.lblStats.Name = "lblStats";
            this.tableLayoutPanel1.SetRowSpan(this.lblStats, 4);
            this.lblStats.Size = new System.Drawing.Size(210, 320);
            this.lblStats.TabIndex = 2;
            this.lblStats.Text = "Список\r\n";
            // 
            // lbCharacterEq
            // 
            this.lbCharacterEq.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCharacterEq.FormattingEnabled = true;
            this.lbCharacterEq.Location = new System.Drawing.Point(267, 53);
            this.lbCharacterEq.Name = "lbCharacterEq";
            this.tableLayoutPanel1.SetRowSpan(this.lbCharacterEq, 4);
            this.lbCharacterEq.Size = new System.Drawing.Size(209, 314);
            this.lbCharacterEq.TabIndex = 1;
            this.lbCharacterEq.Tag = "equipment";
            this.lbCharacterEq.SelectedIndexChanged += new System.EventHandler(this.lbCharacterEq_SelectedIndexChanged);
            // 
            // btnUnEquip
            // 
            this.btnUnEquip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnUnEquip.Location = new System.Drawing.Point(217, 213);
            this.btnUnEquip.Name = "btnUnEquip";
            this.btnUnEquip.Size = new System.Drawing.Size(44, 44);
            this.btnUnEquip.TabIndex = 4;
            this.btnUnEquip.Text = "<<";
            this.btnUnEquip.UseVisualStyleBackColor = true;
            this.btnUnEquip.Click += new System.EventHandler(this.btnUnEquip_Click);
            // 
            // btnEquip
            // 
            this.btnEquip.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnEquip.Location = new System.Drawing.Point(217, 163);
            this.btnEquip.Name = "btnEquip";
            this.btnEquip.Size = new System.Drawing.Size(44, 44);
            this.btnEquip.TabIndex = 3;
            this.btnEquip.Text = ">>";
            this.btnEquip.UseVisualStyleBackColor = true;
            this.btnEquip.Click += new System.EventHandler(this.btnEquip_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 50);
            this.label2.TabIndex = 5;
            this.label2.Text = "В сундуке";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Location = new System.Drawing.Point(267, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(209, 50);
            this.label3.TabIndex = 6;
            this.label3.Text = "Что на тебе";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Location = new System.Drawing.Point(502, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(210, 50);
            this.label4.TabIndex = 7;
            this.label4.Text = "Статы";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnClose.Location = new System.Drawing.Point(267, 393);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(209, 47);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Закрыть";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmInventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(715, 443);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "frmInventory";
            this.Text = "Инвентарь";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInventory_FormClosing);
            this.Load += new System.EventHandler(this.frmInventory_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox lbInventoryEq;
        private System.Windows.Forms.Label lblStats;
        private System.Windows.Forms.ListBox lbCharacterEq;
        private System.Windows.Forms.Button btnUnEquip;
        private System.Windows.Forms.Button btnEquip;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnClose;
    }
}