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
    public class Class_LinkWithServer
    {
        public TcpListener listener;
        public string localIP;
        public int port;
        public bool Quit = false;
        public List<User> userList;

        public Form_ChatWindow form;
        public Form_Main form1;

        public bool start()
        {
            try
            {
                listener = new TcpListener(IPAddress.Parse(localIP), port);
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
        
        //别人连我
        private void ReceiveFormClient()
        {
            TcpClient client = new TcpClient();
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
                for (int i = 0; i < form.)
            }
        }

        //我连别人
        private void ChatWithFriend(object o)
        {
            //

            Form_ChatWindow form = new Form_ChatWindow();
            TcpClient client = form.client;
            BinaryReader br = form.br;
            String info = client.Client.RemoteEndPoint.ToString();
            string msg = null;
            while (true)
            {
                try
                {
                    msg = br.ReadString();
                }
                catch
                {
                    //AddItemToListBox1(string.Format("与{0}的连接断了", info));
                    form2.Close();
                    return;
                }
                //SendToForm2(form2, msg);
            }
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
            
            }
        }

        public bool IsConnect(string id)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].id == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
