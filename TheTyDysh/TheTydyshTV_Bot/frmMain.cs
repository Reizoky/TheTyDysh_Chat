using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Net.Sockets;
using System.Net;

namespace TheTyDysh
{
    public partial class frmMain : Form
    {
        public static frmMain frmMainWindow;
        Events events = new Events();
        public frmMain()
        {
            InitializeComponent();
            frmMainWindow = this;
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //twitchChat = new TwitchChat();
            //tabControlChats.DrawMode = TabDrawMode.OwnerDrawFixed; //для отрисовки изменения вкладок
        }

        //Временное
        //Убрать после того, как сделаешь регистрацию
        public static string userName;
        public static string userRole;
        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (!btnSendChatMsg.Enabled)
                {
                    frmLogin frmLogin = new frmLogin();
                    if (frmLogin.ShowDialog() == DialogResult.OK)
                        ConnectToServer();
                    LoadCharacterInfo();
                    btnConnect.Text = "Отключиться";

                    btnInfo.Enabled = true;
                }
                else
                {
                    btnConnect.Text = "Подключиться";
                    btnInfo.Enabled = false;
                    btnSendChatMsg.Enabled = false;
                    lbUsers.Items.Clear();
                    btnSendChatMsg.Enabled = false;
                    wbChat.DocumentText = "";
                    wbFight.DocumentText = "";
                    wbSystem.DocumentText = "";
                    Send("#endsession");
                    _serverSocket.Disconnect(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine 
                    + "Дабы исправить данную ошибку скопируйте и отправьте в дискорд пользователю Reizoky");
            }
        }
        
        private void btnSendChatMsg_Click(object sender, EventArgs e)
        {
            PreapereSendMessage(tbChatMsg.Text);
        }

        private void tbChatMsg_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                PreapereSendMessage(tbChatMsg.Text);
            }
        }
        private void PreapereSendMessage(string msg)
        {
            SendMsg(msg);
            tbChatMsg.Focus();
        }

        private void wbChats_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (((WebBrowser)(sender)) != null)
            if (yScroll + 450 >= ((WebBrowser)(sender)).Document.Body.ScrollRectangle.Height)
            {
                ((WebBrowser)(sender)).Document.Window.ScrollTo(0, ((WebBrowser)(sender)).Document.Body.ScrollRectangle.Height);
            }
            else
            {
                ((WebBrowser)(sender)).Document.Window.ScrollTo(0, yScroll);
            }
        }

        public Character character;
        private void LoadCharacterInfo()
        {
            character = new Character(userName);
        }

        #region Работа с сервером

        private delegate void ChatEvent(string msg, string fontStyle, string targetChat);
        private ChatEvent _addMessage;
        private Socket _serverSocket;
        private Thread listenThread;
        //private string serverIp = "192.168.1.101";
        private const string serverIp = "159.69.223.153";
        private int serverPortt = 49050;
        int yScroll = -1;

        const string  systemStyle = " size = 3  style = \"background:black; color:white\" ";
        public string defChatStyle = " size = 2 style \"background:white; color:black\" ";
        private void ConnectToServer()
        {
            wbChat.DocumentText = "";
            wbFight.DocumentText = "";
            wbSystem.DocumentText = "";
            _addMessage = new ChatEvent(WriteLocalChat);
            contextMenuStripUser = new ContextMenuStrip();
            ToolStripMenuItem newBattle = new ToolStripMenuItem();
            newBattle.Text = "Вызвать на ДУЭЛЬ!!1!";
            newBattle.Click += NewBattle;
            contextMenuStripUser.Items.Add(newBattle);
            ToolStripMenuItem privateMsg = new ToolStripMenuItem();
            privateMsg.Text = "Личное сообщение";
            privateMsg.Click += NewPrivateMsg; ;
            //contextMenuStripUser.Items.Add(privateMsg);

            lbUsers.ContextMenuStrip = contextMenuStripUser;

            IPAddress temp = IPAddress.Parse(serverIp);
            _serverSocket = new Socket(temp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Connect(new IPEndPoint(temp, serverPortt));
            if (_serverSocket.Connected)
            {
                btnSendChatMsg.Enabled = true; 
                WriteLocalChat("Связь с сервером установлена.", systemStyle);
                listenThread = new Thread(listner);
                listenThread.IsBackground = true;
                listenThread.Start();

            }
            else
                WriteLocalChat("Связь с сервером не установлена.", systemStyle);

            Send($"setname|{userName}");
        }

        private void NewPrivateMsg(object sender, EventArgs e)
        {
            if (lbUsers.SelectedItems.Count > 0)
                tbChatMsg.Text = $"\\w " + lbUsers.SelectedItem + " ";
        }

        private void NewBattle(object sender, EventArgs e)
        {
            if (!events.isFight && lbUsers.SelectedItems.Count > 0 && 
                !string.Equals(lbUsers.SelectedItem.ToString(), userName, StringComparison.CurrentCultureIgnoreCase))
            {
                events.StartEventFight(userName, lbUsers.SelectedItem.ToString().ToLower());
            }
        }
        /// <summary>
        /// Отправка личной информации в чат
        /// </summary>
        /// <param name="msg">Сообщение</param>
        /// <param name="fontStyle">Стиль текста</param>
        private void WriteLocalChat(string msg,   string fontStyle, string targetChat = "main")
        {
            if (InvokeRequired)
            {
                Invoke(_addMessage, msg, fontStyle, targetChat);
                return;
            }
            if (targetChat == "main")
            {
                wbChat.DocumentText += "<font {addStyle}>".Replace("{addStyle}", fontStyle) + msg + "</font><br>";
                if (wbChat.Document.Body != null)
                    yScroll = wbChat.Document.Body.ScrollTop;
                if (tabControlChats.SelectedIndex != 0)
                {
                    string[] arrTextHeader = tpMainChat.Text.Split(' ');
                    int numbNewMess = arrTextHeader.Length > 1 ? Convert.ToInt32(arrTextHeader[1]) + 1 : 1;
                    tpMainChat.Text = tpMainChat.Text.Split(' ')[0] + (numbNewMess > 0 ? " " + numbNewMess.ToString(): "");
                }
            }
            else if (targetChat == "fight")
            {
                wbFight.DocumentText += "<font {addStyle}>".Replace("{addStyle}", fontStyle) + msg + "</font><br>";
                if (wbFight.Document.Body != null)
                    yScroll = wbFight.Document.Body.ScrollTop;
                if (tabControlChats.SelectedIndex != 1)
                {
                    string[] arrTextHeader = tpBattle.Text.Split(' ');
                    int numbNewMess = arrTextHeader.Length > 1 ? Convert.ToInt32(arrTextHeader[1]) + 1 : 1;
                    tpBattle.Text = tpBattle.Text.Split(' ')[0] + (numbNewMess > 0 ? " " + numbNewMess.ToString() : "");
                }
            }
            else if (targetChat == "system")
            {
                wbSystem.DocumentText += "<font {addStyle}>".Replace("{addStyle}", fontStyle) + msg + "</font><br>";
                if (wbSystem.Document.Body != null)
                    yScroll = wbSystem.Document.Body.ScrollTop;
                if (tabControlChats.SelectedIndex != 1)
                {
                    string[] arrTextHeader = tpSystem.Text.Split(' ');
                    int numbNewMess = arrTextHeader.Length > 1 ? Convert.ToInt32(arrTextHeader[1]) + 1 : 1;
                    tpSystem.Text = tpSystem.Text.Split(' ')[0] + (numbNewMess > 0 ? " " + numbNewMess.ToString() : "");
                }
            }
        }


        public void Send(byte[] buffer)
        {
            try
            {
                _serverSocket.Send(buffer);
            }
            catch { }
        }
        public void Send(string Buffer)
        {
            try
            {
                _serverSocket.Send(Encoding.Unicode.GetBytes(Buffer));
            }
            catch { }
        }
        /// <summary>
        /// Прослушка сокета
        /// </summary>
        public void listner()
        {
            try
            {
                while (_serverSocket.Connected)
                {
                    byte[] buffer = new byte[2048];
                    int bytesReceive = _serverSocket.Receive(buffer);
                    handleCommand(Encoding.Unicode.GetString(buffer, 0, bytesReceive));
                }
            }
            catch
            {
                MessageBox.Show("Связь с сервером прервана");
                Application.Exit();
            }
        }

        public void handleCommand(string cmd)
        {

            string[] commands = cmd.Split('#');
            int countCommands = commands.Length;
            for (int i = 0; i < countCommands; i++)
            {
                try
                {
                    string currentCommand = commands[i];
                    if (string.IsNullOrEmpty(currentCommand))
                        continue;
                    if (currentCommand.Contains("setnamesuccess"))
                    {
                        //Из-за того что программа пыталась получить доступ к контролам из другого потока вылетал эксепщен и поля не разблокировались

                        Invoke((MethodInvoker)delegate
                        {
                            btnSendChatMsg.Enabled = true;
                        });
                        continue;
                    }
                    if (currentCommand.Contains("setnamefailed"))
                    {
                        WriteLocalChat("Неверный ник!", systemStyle);
                        continue;
                    }
                    if (currentCommand.Contains("msg"))
                    {

                        string[] strCommand = currentCommand.Split('|');
                        if (strCommand.Length == 2)
                            WriteLocalChat(strCommand[1], defChatStyle);
                        else
                            for (int z = 0; z < strCommand.Length; z++)
                                if (strCommand[z].Contains("??chat"))
                                {
                                    string[] chats = strCommand[z].Split(':');
                                    foreach (string chat in chats)
                                    {
                                        if (chat == "main")
                                            WriteLocalChat(strCommand[1], defChatStyle);
                                        if (chat == "fight")
                                            WriteLocalChat(strCommand[1], defChatStyle, "fight");
                                        if (chat == "system")
                                            WriteLocalChat(strCommand[1], defChatStyle, "system");
                                    }
                                }
                        continue;
                    }

                    if (currentCommand.Contains("userlist"))
                    {
                        string[] Users = currentCommand.Split('|')[1].Split(',');
                        int countUsers = Users.Length;
                        lbUsers.Invoke((MethodInvoker)delegate { lbUsers.Items.Clear(); });
                        for (int j = 0; j < countUsers; j++)
                        {
                            if (Users[j].ToString() != "")
                                lbUsers.Invoke((MethodInvoker)delegate { lbUsers.Items.Add(Users[j]); });
                        }
                        continue;

                    }

                }
                catch (Exception exp)
                {
                    Console.WriteLine("Error with handleCommand: " + exp.Message);
                }

            }


        }

        /// <summary>
        /// Отправка сообщения в общий чат
        /// </summary>
        /// <param name="msg"></param>
        public void SendMsg(string msg)
        {
            if (string.IsNullOrEmpty(msg))
                return;
            if (msg[0] == '\\' && msg[1] == 'w')
            {
                string temp = msg.Split(' ')[0];
                string content = msg.Substring(temp.Length + 1);
                temp = temp.Replace("\\w", string.Empty);
                Send($"#private|{temp}|{content}");
            }
            else
                Send($"#message|{userName}: {msg}");
            tbChatMsg.Text = string.Empty;
        }
        #endregion

        private void tabControlChats_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (((TabControl)(sender)).SelectedIndex)
            {
                case 0: tpMainChat.Text = "Чат"; break;
                case 1: tpBattle.Text = "Бои"; break;
                case 2: tpSystem.Text = "Системый"; break;
            }
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Send("#endsession");
        }

        private void btтInfoInvStat_Click(object sender, EventArgs e)
        {
            if (userName != "")
            {
                frmInventory frmInventory = new frmInventory();
                frmInventory.Show();
            }
        }
    }
}
