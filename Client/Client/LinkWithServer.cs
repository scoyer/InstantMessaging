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

        public LinkWithServer()
        {
            localIP = getLocalIP();
            listen_port = getPort();
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
            sendToServer(string.Format("login,{0},{1},{2},{3}", localIP, listen_port.ToString(), id, password));
            string content = new BinaryReader(client.GetStream()).ReadString();
            //登录成功
            if (content.StartsWith("login_success"))
            {
                Thread thread = new Thread(ReceiveFromServe);
                //thread.IsBackground = true;
                thread.Start();
            }
            return content;
        }

        public void ReceiveFromServe()
        {
            string content;
            BinaryReader br = new BinaryReader(client.GetStream());
            while (true) 
            {
                try
                {
                    content = br.ReadString();
                }
                catch
                {
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

        public void close()
        {
            if (client != null)
            {
                client.Close();
            }
        }
    }
}
