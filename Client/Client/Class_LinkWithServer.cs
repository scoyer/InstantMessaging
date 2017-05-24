using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Client
{
    public class LinkWithServer
    {
        public TcpClient client;
        public Class_LinkWithServer lwc;
        public string serverIP;
        public string localIP;
        public int port = 51000;
        public int listen_port;
        public bool Quit = false;
        public User me;

        public Form_Main form;

        public LinkWithServer()
        {
            serverIP = localIP = getLocalIP();
        }

        public string login(string id, string password)
        {
            try
            {
                client = new TcpClient(serverIP, port);
            }
            catch
            {
                return "link_fail";
            }
            Quit = false;
            listen_port = getPort();
            sendToServer(string.Format("login,{0},{1},{2},{3}", localIP, listen_port.ToString(), id, password));
            string content = new BinaryReader(client.GetStream()).ReadString();
            
            //登录成功
            if (content.StartsWith("login_success"))
            {
                lwc = new Class_LinkWithServer(new User(content.Split(',')));
                lwc.start();
            }
            return content;
        }

        public void start(User me)
        {
            this.me = me;
            Thread thread = new Thread(ReceiveFromServer);
            thread.IsBackground = true;
            thread.Start();
        }

        public void close()
        {
            if (client != null)
            {
                lock (lwc.userList)
                {
                    for (int i = 0; i < lwc.userList.Count; i++)
                    {
                        form.friendList.RemoveItemToListBox1(lwc.userList[i]);
                    }
                }
                Quit = true;
                client.Close();
                lwc.close();
            }
        }

        public void ReceiveFromServer()
        {
            string message;
            BinaryReader br = new BinaryReader(client.GetStream());
            while (Quit == false) 
            {
                try
                {
                    message = br.ReadString();
                }
                catch
                {
                    //与服务器失联了，更改为离线状态
                    Console.WriteLine("离线");
                    if (Quit == false)
                        MessageBox.Show("与服务器失联");
                    close();
                    break;
                }
                string[] content = message.Split(',');
                User user = null;
                switch (content[0])
                { 
                    case "login":
                        //Format IP,listen_port,id,nickname,signature
                        user = new User(content);
                        lwc.userList.Add(user);
                        form.friendList.AddItemToListBox1(user);
                        break;
                    case "logout":
                        user = new User(content);
                        lwc.userList.Remove(user);
                        form.friendList.RemoveItemToListBox1(user);
                        break;
                    case "talk":
                        
                        break;
                    default:
                        break;
                }
            }
        }

        public int getPort()
        {
            for (int i = 51000; i < 60000; i++)
            {
                if (!PortInUse(i)) {
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

        public bool sendToServer(string message)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(client.GetStream());
                bw.Write(message);
                bw.Flush();
            }
            catch
            {
                return false;   
            }
            return true;
        }

    }
}
