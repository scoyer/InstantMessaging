using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Net.NetworkInformation;

namespace Client
{
    public partial class Form_ChatWindow : Form
    {
        public BinaryWriter bw;
        public string hisid;
        public string hisnickname;
        public User me;

        public Form_ChatWindow()
        {
            InitializeComponent();
        }

        public Form_ChatWindow(string id, string nickname, User me, BinaryWriter bw)
        {
            InitializeComponent();
            this.textBox3.Text = string.Format("{0}({1})", nickname, id);
            this.bw = bw;
            this.hisid = id;
            this.hisnickname = nickname;
            this.me = me;
        }

        private void Form_ChatWindow_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button3, "发送文件");
            toolTip2.SetToolTip(button4, "窗口抖动");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.InitialDirectory = "D:\\";
            ofd.RestoreDirectory = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileName = ofd.FileName;
                Thread thread = new Thread(SendFile);
                thread.IsBackground = true;
                thread.Start(fileName);
            }
        }

        private void SendFile(object o)
        {
            int port = getPort();
            string filePath = (string)o;
            TcpListener listener = null;
            FileStream fs = null;
            TcpClient client = null;

            try
            {
                //新建监听端口
                listener = new TcpListener(IPAddress.Parse(me.localIP), port);
                listener.Start();
                
                //提取文件信息
                string fileName = System.IO.Path.GetFileName(filePath);
                fs = new FileStream(filePath, FileMode.Open);
                int fsLen = (int)fs.Length;


                //发送文件信息给用户
                //Format localIP,port,nickname,filename,length
                this.bw.Write(string.Format("file,{0},{1},{2},{3},{4}", me.localIP, port.ToString(), hisnickname, fileName, fsLen));
                this.bw.Flush();

                client = listener.AcceptTcpClient();
                BinaryWriter _bw = new BinaryWriter(client.GetStream());
                BinaryReader _br = new BinaryReader(client.GetStream());

                //读取用户请求
                string request = _br.ReadString();
                if (request == "cancle")
                {
                    AddItemToListBox1("对方取消文件传输");
                    if (listener != null) listener.Stop();
                    if (client != null) client.Close();
                    if (fs != null) fs.Close();
                    return;
                }

                //读文件
                byte[] heByte = new byte[fsLen];
                int r = fs.Read(heByte, 0, heByte.Length);

                _bw.Write(heByte);
                _bw.Flush();
                _br.Read();
                AddItemToListBox1("发送文件成功");
            }
            catch
            {
                //发送文件错误
                AddItemToListBox1("发送文件失败");
            }
            if (listener != null) listener.Stop();
            if (client != null) client.Close();
            if (fs != null) fs.Close();
        }

        public int getPort()
        {
            for (int i = 51000; i < 60000; i++)
            {
                if (!PortInUse(i))
                {
                    return i;
                }
            }
            return 0;
        }

        public bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();

            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }
        
        private delegate void AddItemToListBox1Delegate(string str);
        public void AddItemToListBox1(string str)
        {
            if (listBox1.InvokeRequired)
            {
                AddItemToListBox1Delegate add = AddItemToListBox1;
                listBox1.Invoke(add, str);
            }
            else
            {
                listBox1.Items.Add(str);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string msg = textBox1.Text;
            textBox1.Clear();
            SendMessage(string.Format("talk,{0}", msg));
            AddItemToListBox1(format(msg));
        }

        private void SendMessage(string msg) 
        {
            try
            {
                bw.Write(msg);
                bw.Flush();
            }
            catch
            {
                //用户离线
                AddItemToListBox1("==========================该用户已经离线了========================");
                return;
            }
        }

        private string format(string msg) {
            DateTime date = DateTime.Now;
            return string.Format("{0}   {1}\n{2}", me.nickname, date, msg);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Form_ChatWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index == -1) return;
            e.DrawFocusRectangle();

            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds);
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            ListBox theListBox = (ListBox)sender;
            string itemString = (string)theListBox.Items[e.Index];
            int totalHeight = 0;
            string[] content = itemString.Split('\n');
            for (int i = 0; i < content.Length; i++)
            {
                totalHeight += 1 + (content[i].Length / 33);
            }
            e.ItemHeight += totalHeight * 12 + 30;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SendMessage("activate");
        }

        public void Open()
        {
            if (this.IsHandleCreated)
            {
                MethodInvoker mi = new MethodInvoker(OpenForm);
                this.Invoke(mi);
            }
        }

        public void OpenForm()
        { 
            if (this.Visible == false)
            {
                this.Visible = true;
            }
            else
            {
                this.Activate();
            }
        }
    }
}
