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
                    break;
                }

            }
        }
        /*
        private void ChatWithFriend(Object o)
        {
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
         * */
    }
}
