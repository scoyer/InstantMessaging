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

namespace Client
{
    public class LinkWithServer
    {
        public TcpClient client;
        public string localIP;
        public int port = 51000;
        public int listen_port;
        public bool Quit = false;

        public Form_Main form;

        public LinkWithServer()
        {
            localIP = getLocalIP();
            listen_port = getPort();
        }

        public LinkWithServer(Form_Main form)
        {
            localIP = getLocalIP();
            listen_port = getPort();
            this.form = form;
        }

        public string start(string id, string password)
        {
            try
            {
                client = new TcpClient(localIP, port);
            }
            catch
            {
                return "link_fail";
            }
            Quit = false;
            sendToServer(string.Format("login,{0},{1},{2},{3}", localIP, listen_port.ToString(), id, password));
            string content = new BinaryReader(client.GetStream()).ReadString();
            //登录成功
            if (content.StartsWith("login_success"))
            {
                Thread thread = new Thread(ReceiveFromServer);
                thread.IsBackground = true;
                thread.Start();
            }
            return content;
        }

        public void close(string password, string nickname, string signature)
        {
            if (client != null)
            {
                Quit = true;
                sendToServer(string.Format("logout,{0},{1},{2}", password, nickname, signature));
                client.Close();
            }
        }

        public void ReceiveFromServer()
        {
            string message;
            BinaryReader br = new BinaryReader(client.GetStream());
            while (true) 
            {
                try
                {
                    message = br.ReadString();
                }
                catch
                {
                    break;
                }
                string[] content = message.Split(',');
                switch (content[0])
                { 
                    case "login":
                        //Format IP,port,listen_port,id,nickname,signature
                        while (true)
                        {
                            if (form != null && form.friendList != null)
                            {
                                form.friendList.AddItemToListBox1(new User(content));
                                break;
                            }
                            else
                            {
                                Thread.Sleep(20);
                            }
                        }
                        break;
                    case "logout":
                        while (true)
                        {
                            if (form != null && form.friendList != null)
                            {
                                form.friendList.RemoveItemToListBox1(new User(content));
                                break;
                            }
                            else
                            {
                                Thread.Sleep(20);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        /*
        private void AddFriend(object o)
        {
            while (true)
            {
                if (form == null || form.friendList == null)
                {
                    Thread.Sleep(10);
                }
                else
                {
                    form.friendList.AddItemToListBox1((User)o);
                    break;
                }
            }
        }

        private void RemoveFriend(object o)
        {
            while (true)
            {
                if (form == null || form.friendList == null)
                {
                    Thread.Sleep(10);
                }
                else
                {
                    form.friendList.RemoveItemToListBox1((User)o);
                    break;
                }
            }
        }
        */
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
