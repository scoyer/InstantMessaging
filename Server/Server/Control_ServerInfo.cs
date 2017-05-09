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
    public partial class Control_ServerInfo : UserControl
    {
        public string localIP;
        public int port;
        public LinkWithClient lwc;

        public Control_ServerInfo()
        {
            InitializeComponent();
        }

        public Control_ServerInfo(LinkWithClient LWC)
        {
            InitializeComponent();
            this.lwc = LWC;
        }

        private void ServerInfo_Load(object sender, EventArgs e)
        {
            localIP = getLocalIP();
            port = getPort();
            textBox1.Text = localIP;
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

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == "ON")
            {
                textBox2.Text = "0";
                textBox4.Text = "OFF";
                button1.Text = "开始监听";
                lwc.stop();
            }
            else
            {
                textBox4.Text = "ON";
                button1.Text = "停止监听";
                lwc.start(textBox1.Text, int.Parse(textBox3.Text));
            }
        }

    }
}
