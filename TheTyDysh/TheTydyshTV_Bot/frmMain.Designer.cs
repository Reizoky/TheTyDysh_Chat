namespace TheTyDysh
{
    partial class frmMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnConnect = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.btnSendChatMsg = new System.Windows.Forms.Button();
            this.tbChatMsg = new System.Windows.Forms.TextBox();
            this.wbChat = new System.Windows.Forms.WebBrowser();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStripUser = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabControlChats = new System.Windows.Forms.TabControl();
            this.tpMainChat = new System.Windows.Forms.TabPage();
            this.tpBattle = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.wbFight = new System.Windows.Forms.WebBrowser();
            this.tpSystem = new System.Windows.Forms.TabPage();
            this.wbSystem = new System.Windows.Forms.WebBrowser();
            this.btnInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.btтInfoInvStat = new System.Windows.Forms.ToolStripMenuItem();
            this.btnPunchStreamer = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tabControlChats.SuspendLayout();
            this.tpMainChat.SuspendLayout();
            this.tpBattle.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tpSystem.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Silver;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnConnect,
            this.btnInfo});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnConnect
            // 
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(101, 20);
            this.btnConnect.Text = "Подключиться";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 150F));
            this.tableLayoutPanel1.Controls.Add(this.lbUsers, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.btnSendChatMsg, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tbChatMsg, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.wbChat, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(701, 425);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // lbUsers
            // 
            this.lbUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(554, 3);
            this.lbUsers.Name = "lbUsers";
            this.tableLayoutPanel1.SetRowSpan(this.lbUsers, 2);
            this.lbUsers.Size = new System.Drawing.Size(144, 379);
            this.lbUsers.TabIndex = 0;
            // 
            // btnSendChatMsg
            // 
            this.btnSendChatMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSendChatMsg.Enabled = false;
            this.btnSendChatMsg.Location = new System.Drawing.Point(3, 388);
            this.btnSendChatMsg.Name = "btnSendChatMsg";
            this.btnSendChatMsg.Size = new System.Drawing.Size(545, 34);
            this.btnSendChatMsg.TabIndex = 1;
            this.btnSendChatMsg.Text = "Отправить";
            this.btnSendChatMsg.UseVisualStyleBackColor = true;
            this.btnSendChatMsg.Click += new System.EventHandler(this.btnSendChatMsg_Click);
            // 
            // tbChatMsg
            // 
            this.tbChatMsg.BackColor = System.Drawing.SystemColors.Control;
            this.tbChatMsg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbChatMsg.Location = new System.Drawing.Point(3, 338);
            this.tbChatMsg.MaxLength = 200;
            this.tbChatMsg.Multiline = true;
            this.tbChatMsg.Name = "tbChatMsg";
            this.tbChatMsg.Size = new System.Drawing.Size(545, 44);
            this.tbChatMsg.TabIndex = 2;
            this.tbChatMsg.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbChatMsg_KeyUp);
            // 
            // wbChat
            // 
            this.wbChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbChat.Location = new System.Drawing.Point(3, 3);
            this.wbChat.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbChat.Name = "wbChat";
            this.wbChat.Size = new System.Drawing.Size(545, 329);
            this.wbChat.TabIndex = 3;
            this.wbChat.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbChats_DocumentCompleted);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Location = new System.Drawing.Point(554, 385);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 40);
            this.label1.TabIndex = 4;
            this.label1.Text = "Тут будут смайлы и тд";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStripUser
            // 
            this.contextMenuStripUser.Name = "contextMenuStripUser";
            this.contextMenuStripUser.Size = new System.Drawing.Size(61, 4);
            // 
            // tabControlChats
            // 
            this.tabControlChats.Controls.Add(this.tpMainChat);
            this.tabControlChats.Controls.Add(this.tpBattle);
            this.tabControlChats.Controls.Add(this.tpSystem);
            this.tabControlChats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlChats.Location = new System.Drawing.Point(0, 24);
            this.tabControlChats.Name = "tabControlChats";
            this.tabControlChats.SelectedIndex = 0;
            this.tabControlChats.Size = new System.Drawing.Size(715, 457);
            this.tabControlChats.TabIndex = 4;
            this.tabControlChats.SelectedIndexChanged += new System.EventHandler(this.tabControlChats_SelectedIndexChanged);
            // 
            // tpMainChat
            // 
            this.tpMainChat.BackColor = System.Drawing.Color.Transparent;
            this.tpMainChat.Controls.Add(this.tableLayoutPanel1);
            this.tpMainChat.Location = new System.Drawing.Point(4, 22);
            this.tpMainChat.Name = "tpMainChat";
            this.tpMainChat.Padding = new System.Windows.Forms.Padding(3);
            this.tpMainChat.Size = new System.Drawing.Size(707, 431);
            this.tpMainChat.TabIndex = 0;
            this.tpMainChat.Text = "Чат";
            // 
            // tpBattle
            // 
            this.tpBattle.Controls.Add(this.tableLayoutPanel2);
            this.tpBattle.Location = new System.Drawing.Point(4, 22);
            this.tpBattle.Name = "tpBattle";
            this.tpBattle.Padding = new System.Windows.Forms.Padding(3);
            this.tpBattle.Size = new System.Drawing.Size(707, 431);
            this.tpBattle.TabIndex = 1;
            this.tpBattle.Text = "Бои";
            this.tpBattle.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.wbFight, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(701, 425);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // wbFight
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.wbFight, 2);
            this.wbFight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbFight.Location = new System.Drawing.Point(3, 3);
            this.wbFight.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbFight.Name = "wbFight";
            this.tableLayoutPanel2.SetRowSpan(this.wbFight, 2);
            this.wbFight.Size = new System.Drawing.Size(695, 419);
            this.wbFight.TabIndex = 0;
            this.wbFight.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbChats_DocumentCompleted);
            // 
            // tpSystem
            // 
            this.tpSystem.Controls.Add(this.wbSystem);
            this.tpSystem.Location = new System.Drawing.Point(4, 22);
            this.tpSystem.Name = "tpSystem";
            this.tpSystem.Padding = new System.Windows.Forms.Padding(3);
            this.tpSystem.Size = new System.Drawing.Size(707, 431);
            this.tpSystem.TabIndex = 2;
            this.tpSystem.Text = "Системный";
            this.tpSystem.UseVisualStyleBackColor = true;
            // 
            // wbSystem
            // 
            this.wbSystem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wbSystem.Location = new System.Drawing.Point(3, 3);
            this.wbSystem.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbSystem.Name = "wbSystem";
            this.wbSystem.Size = new System.Drawing.Size(701, 425);
            this.wbSystem.TabIndex = 1;
            this.wbSystem.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.wbChats_DocumentCompleted);
            // 
            // btnInfo
            // 
            this.btnInfo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btтInfoInvStat,
            this.btnPunchStreamer});
            this.btnInfo.Enabled = false;
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(108, 20);
            this.btnInfo.Text = "Узнать о себе. . .";
            // 
            // btтInfoInvStat
            // 
            this.btтInfoInvStat.Name = "btтInfoInvStat";
            this.btтInfoInvStat.Size = new System.Drawing.Size(222, 22);
            this.btтInfoInvStat.Text = "Что по статам и шмоткам?";
            this.btтInfoInvStat.Click += new System.EventHandler(this.btтInfoInvStat_Click);
            // 
            // btnPunchStreamer
            // 
            this.btnPunchStreamer.Name = "btnPunchStreamer";
            this.btnPunchStreamer.Size = new System.Drawing.Size(222, 22);
            this.btnPunchStreamer.Text = "Пнуть стримера";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(715, 481);
            this.Controls.Add(this.tabControlChats);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(731, 519);
            this.MinimumSize = new System.Drawing.Size(731, 519);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TydyshTV";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tabControlChats.ResumeLayout(false);
            this.tpMainChat.ResumeLayout(false);
            this.tpBattle.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tpSystem.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem btnConnect;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripUser;
        public System.Windows.Forms.ListBox lbUsers;
        public System.Windows.Forms.Button btnSendChatMsg;
        public System.Windows.Forms.TextBox tbChatMsg;
        public System.Windows.Forms.WebBrowser wbChat;
        public System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabControl tabControlChats;
        private System.Windows.Forms.TabPage tpMainChat;
        private System.Windows.Forms.TabPage tpBattle;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.WebBrowser wbFight;
        private System.Windows.Forms.TabPage tpSystem;
        private System.Windows.Forms.WebBrowser wbSystem;
        private System.Windows.Forms.ToolStripMenuItem btnInfo;
        private System.Windows.Forms.ToolStripMenuItem btтInfoInvStat;
        private System.Windows.Forms.ToolStripMenuItem btnPunchStreamer;
    }
}

