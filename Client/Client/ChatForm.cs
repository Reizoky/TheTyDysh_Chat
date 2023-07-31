using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.IO;


namespace Client
{
    public partial class ChatForm : Form
    {
        private delegate void ChatEvent(string content);
        private ChatEvent _addMessage;
        private Socket _serverSocket;
        private Thread listenThread;
        private string _host = "192.168.1.101";
        //private string _host = "159.69.223.153";
        private int _port = 49050;
        public ChatForm()
        {
            InitializeComponent();
            _addMessage = new ChatEvent(AddMessage);
            userMenu = new ContextMenuStrip();
            ToolStripMenuItem PrivateMessageItem = new ToolStripMenuItem();
            PrivateMessageItem.Text = "Личное сообщение";
            PrivateMessageItem.Click += delegate 
            {
                if (userList.SelectedItems.Count > 0)
                {
                    messageData.Text = $"\"{userList.SelectedItem} ";
                }
            };
            userMenu.Items.Add(PrivateMessageItem);
            
            userList.ContextMenuStrip = userMenu;

        }

        int yScroll = -1;
        private void AddMessage(string Content)
        {
            if(InvokeRequired)
            {
                Invoke(_addMessage, Content);
                return;
            }
            wbChat.DocumentText += "<font size=2 {addStyle}>" + Content + "</font><br>";
            if (wbChat.Document.Body != null)
                yScroll = wbChat.Document.Body.ScrollTop;
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            IPAddress temp = IPAddress.Parse(_host);
            _serverSocket = new Socket(temp.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.Connect(new IPEndPoint(temp, _port));
            if (_serverSocket.Connected)
            {
                enterChat.Enabled = true;
                nicknameData.Enabled = true;
                AddMessage("Связь с сервером установлена.");
                listenThread = new Thread(listner);
                listenThread.IsBackground = true;
                listenThread.Start();
                
            }
            else
                AddMessage("Связь с сервером не установлена.");
            
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
                            AddMessage($"Добро пожаловать, {nicknameData.Text}");
                            nameData.Text = nicknameData.Text;
                            messageData.Enabled = true;
                            userList.Enabled = true;
                            nicknameData.Enabled = false;
                            enterChat.Enabled = false;
                        });
                        continue;
                    }
                    if(currentCommand.Contains("setnamefailed"))
                    {
                        AddMessage("Неверный ник!");
                        continue;
                    }
                    if(currentCommand.Contains("msg"))
                    {
                        string[] Arguments = currentCommand.Split('|');
                        AddMessage(Arguments[1]);
                        continue;
                    }

                    if(currentCommand.Contains("userlist"))
                    {
                        string[] Users = currentCommand.Split('|')[1].Split(',');
                        int countUsers = Users.Length;
                        userList.Invoke((MethodInvoker)delegate { userList.Items.Clear(); });
                        for(int j = 0;j < countUsers;j++)
                        {
                            userList.Invoke((MethodInvoker)delegate { userList.Items.Add(Users[j]) ; });
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

        private void enterChat_Click(object sender, EventArgs e)
        {
            string nickName = nicknameData.Text;
            if (string.IsNullOrEmpty(nickName))
                return;
            Send($"#setname|{nickName}");
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_serverSocket.Connected)
            {
                _serverSocket.Disconnect(false);
                Send("#endsession");
                lb
            }
        }

        private void wbChat_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (yScroll + 350 >= wbChat.Document.Body.ScrollRectangle.Height)
            {
                wbChat.Document.Window.ScrollTo(0, wbChat.Document.Body.ScrollRectangle.Height);
            }
            else
            {
                wbChat.Document.Window.ScrollTo(0, yScroll);
            }
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            string msgData = messageData.Text;
            if (string.IsNullOrEmpty(msgData))
                return;
            if (msgData[0] == '"')
            {
                string temp = msgData.Split(' ')[0];
                string content = msgData.Substring(temp.Length + 1);
                temp = temp.Replace("\"", string.Empty);
                Send($"#private|{temp}|{content}");
            }
            else
                Send($"#message|{msgData}");
            messageData.Text = string.Empty;
        }
    }
}
