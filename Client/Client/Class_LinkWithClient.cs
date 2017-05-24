using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Client
{
    public class Class_LinkWithServer
    {
        public TcpListener listener;
        public bool Quit = false;
        public List<User> userList = new List<User>();
        public User me;

        public Class_LinkWithServer(User me)
        {
            this.me = me;
        }

        public bool start()
        {
            Console.WriteLine(me.localIP + " " + me.listen_port.ToString());
            try
            {
                listener = new TcpListener(IPAddress.Parse(me.localIP), me.listen_port);
            }
            catch
            {
                return false;
            }
            listener.Start();
            Thread thread = new Thread(ReceiveFormClient);
            thread.IsBackground = true;
            thread.Start();
            return true;
        }

        public string getLocalIP()
        {
            try
            {
                string hostname = Dns.GetHostName();
                IPHostEntry iphostentry = Dns.GetHostEntry(hostname);
                for (int i = 0; i < iphostentry.AddressList.Length; i++)
                {
                    if (iphostentry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return iphostentry.AddressList[i].ToString();
                    }
                }
                return "127.0.0.1";
            }
            catch
            {
                return "127.0.0.1";
            }
        }

        public void close()
        {
            if (listener != null)
            {
                listener.Stop();
            }
        }
        
        //别人连我
        private void ReceiveFormClient()
        {
            TcpClient client = null;
            while (true)
            {
                try
                {
                    client = listener.AcceptTcpClient();
                }
                catch
                {
                    continue;
                }
                string id = new BinaryReader(client.GetStream()).ReadString();
                Console.WriteLine(id);
                User user = GetUser(id);
                if (user == null) {
                    client.Close();
                }
                else {
                    SendMessage(client, "success");
                    user.client = client;
                    user.form = new Form_ChatWindow(user.id, user.nickname, me, new BinaryWriter(client.GetStream()));
                    Thread thread = new Thread(ChatWithFriend);
                    thread.IsBackground = true;
                    thread.Start(user);
                }
            }
        }
        
        //我连别人
        public bool LinkToFriend(User user)
        {
            user = GetUser(user.id);
            TcpClient client = null;
            string status = "";
            try
            {
                client = new TcpClient(user.localIP, user.listen_port);
                SendMessage(client, me.id);
                status = new BinaryReader(client.GetStream()).ReadString();
            }
            catch
            {
                return false;
            }
            if (status != "success") return false;
            user.client = client;
            user.form = new Form_ChatWindow(user.id, user.nickname, me, new BinaryWriter(user.client.GetStream()));
            Thread thread = new Thread(ChatWithFriend);
            thread.IsBackground = true;
            thread.Start(user);
            return true;
        }

        private void ChatWithFriend(object o)
        {
            User user = (User)o;
            TcpClient client = user.client;
            BinaryReader br = new BinaryReader(client.GetStream());
            string msg = null;
            while (true)
            {
                try
                {
                    msg = br.ReadString();
                }
                catch
                {
                    return;
                }
                string[] content = msg.Split(',');
                switch (content[0])
                {
                    case "talk":
                        user.form.AddItemToListBox1(format(msg.Substring(5)));
                        break;
                    case "file":
                        Thread thread = new Thread(ReceiveFile);
                        thread.SetApartmentState(ApartmentState.STA);
                        thread.IsBackground = true;
                        user.photo = msg;
                        thread.Start(user);
                        break;
                    case "activate":
                        user.form.Open();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ReceiveFile(object o)
        {
            User user = (User)o;
            string[] content = user.photo.Split(',');
            try
            {
                TcpClient fClient = new TcpClient(content[1], int.Parse(content[2]));
                //Format localIP,port,nickname,filename,length
                bool ok = false;
                if (MessageBox.Show(string.Format("是否接收文件{0}?", content[4]), string.Format("来自{0}的文件请求", content[3]), MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    
                    //选择文件保存路径
                    string localFilePath = "D:\\";
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.RestoreDirectory = true;
                    sfd.InitialDirectory = localFilePath;
                    sfd.FileName = content[4];
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        localFilePath = sfd.FileName.ToString(); //获得文件路径 

                        SendMessage(fClient, "ok");
                        ok = true;
                        //接受文件
                        int fileLength = int.Parse(content[5]);
                        byte[] heByte = new byte[fileLength];
                        BinaryReader br = new BinaryReader(fClient.GetStream());
                        BinaryWriter bw = new BinaryWriter(fClient.GetStream());
                        br.Read(heByte, 0, fileLength);

                        //传输完成
                        bw.Write("success");
                        bw.Flush();

                        //写文件
                        using (FileStream fs = new FileStream(localFilePath, FileMode.Create))
                        {
                            fs.Write(heByte, 0, fileLength);
                        }

                        user.form.AddItemToListBox1("接受文件成功");
                    }
                    else
                    {
                        
                    }

                }
                else
                {
                    
                }
                if (ok == false) {
                    //拒绝传文件请求
                    SendMessage(fClient, "cancle");
                    user.form.AddItemToListBox1("已取消文件传输");
                }
            }
            catch
            {
                user.form.AddItemToListBox1("传输文件发送未知错误");
            }
            
        }

        private string format(string msg)
        {
            DateTime date = DateTime.Now;
            return string.Format("{0}   {1}\n{2}", me.nickname, date, msg);
        }

        public void SendMessage(TcpClient client, string msg)
        {
            BinaryWriter bw = new BinaryWriter(client.GetStream());
            try
            {
                bw.Write(msg);
                bw.Flush();
            }
            catch
            {
                return;
            }
        }

        public User GetUser(string id) {
            lock (userList)
            {
                for (int i = 0; i < userList.Count; i++)
                {
                    if (userList[i].id.Equals(id))
                    {
                        return userList[i];
                    }
                }
            }
            return null;
        }
    }
}
