using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Server
{
    public partial class ServerInfo : UserControl
    {
        public string hostname;
        public int port;

        public ServerInfo()
        {
            InitializeComponent();
            hostname = getLocalIP();
            port = getPort();
            textBox1.Text = hostname;
            textBox2.Text = "0";
            textBox2.Enabled = false;
            textBox3.Text = port.ToString(); ;
            textBox4.Text = "OFF";
            textBox4.Enabled = false;
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

    }
}
