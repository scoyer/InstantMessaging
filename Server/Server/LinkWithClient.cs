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


namespace Server
{
    public class LinkWithClient
    {
        public TcpListener listener;
        public List<User> userList;
        public List<ListViewItem> lviList;
        public bool Quit;

        public Database database;
        public Form_Main form;

        public LinkWithClient()
        {

        }

        public LinkWithClient(Form_Main FORM) {
            this.form = FORM;
        }

        public int start(string localIP, int port)
        {
            try
            {
                Quit = false;
                userList = new List<User>();
                lviList = new List<ListViewItem>();
                database = new Database();
                database.readFromDatabase();
                listener = new TcpListener(IPAddress.Parse(localIP), port);
                listener.Start();
            }
            catch
            {
                return 0;
            }
            Thread thread = new Thread(ConnectWithClient);
            thread.IsBackground = true;
            thread.Start();
            return 1;
        }

        public void stop()
        {
            try
            {
                Quit = true;
                database.writeToDatebase();
                listener.Stop();
                //clear userList
                for (int i = 0; i < userList.Count; i++)
                {
                    if (userList[i].client != null)
                        userList[i].client.Close();
                }
                userList.Clear();
                //clear lviList
                for (int i = 0; i < lviList.Count; i++) {
                    form.usermanager.RemoveItem(lviList[i]);
                }
                lviList.Clear();
            }
            catch
            {
                return;
            }
        }

        void ConnectWithClient()
        {
            TcpClient client = null;
            while (Quit == false)
            {
                try
                {
                    client = listener.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                Thread thread = new Thread(ReceiveFromClient);
                thread.IsBackground = true;
                thread.Start(client);
            }
        }

        void ReceiveFromClient(object o)
        {
            TcpClient client = (TcpClient)o;
            BinaryReader br = new BinaryReader(client.GetStream());
            string message;
            User user = null;
            ListViewItem lvi = null;
            while (Quit == false)
            {
                try
                {
                    message = br.ReadString();
                }
                catch
                {
                    if (Quit == false) { 
                        //用户离开
                        userList.Remove(user);
                        lviList.Remove(lvi);
                        sendToAllClient("logout," + user.encode());
                        form.usermanager.RemoveItem(lvi);
                    }
                    break;
                }
                string[] content = message.Split(',');
                switch (content[0])
                {
                    case "login":
                        //Format IP,listen_port,id,password
                        //check validity
                        string id = content[3];
                        string password = content[4];
                        //查找有没有重复登录
                        for (int i = 0; i < userList.Count; i++)
                        {
                            if (userList[i].id == id) {
                                SendToClient(client, "login_repeat");
                                return;
                            }    
                        }
                        bool ok = false;
                        user = database.findUser(id);
                        if (user == null)
                        {
                            //用户不存在
                            SendToClient(client, "login_nonexistent");
                        }
                        else if (password != user.password)
                        {
                            //用户密码不正确
                            SendToClient(client, "login_incorrect");
                        }
                        else
                        {
                            //用户登陆成功
                            user.client = client;
                            user.localIP = (client.Client.RemoteEndPoint as IPEndPoint).ToString().Split(':')[0];
                            user.listen_port = int.Parse(content[2]);
                            SendToClient(client, "login_success," + user.encode());
                            sendToAllClient("login," + user.encode());
                            AllShouldSend(user, "login,");
                            userList.Add(user);
                            ok = true;

                            lvi = new ListViewItem();
                            lvi.Text = user.id;
                            lvi.SubItems.Add(user.localIP);
                            lvi.SubItems.Add(user.listen_port.ToString());
                            lvi.SubItems.Add(user.password);
                            lvi.SubItems.Add(user.nickname);
                            lvi.SubItems.Add(user.signature);

                            lviList.Add(lvi);
                            form.usermanager.AddItem(lvi);
                        }
                        if (!ok) return; 
                        break;
                    case "logout":
                        //Format password,nickname,signature
                        userList.Remove(user);
                        lviList.Remove(lvi);
                        sendToAllClient("logout," + user.encode());
                        user.password = content[1];
                        user.nickname = content[2];
                        user.signature = content[3];
                        database.updateUser(user);
                        form.usermanager.RemoveItem(lvi);
                        return;
                    case "text":
                        //Format id1,id2
                        break;
                    case "file":
                        //Format id1,id2
                        break;
                    case "image":
                        //Format id1,id2
                        break;
                    default:
                        break;
                }
            }
        }

        public void SendToClient(TcpClient client, string msg)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(client.GetStream());
                bw.Write(msg);
                bw.Flush();
            }
            catch
            {
                return;
            }
        }

        public void SendToClientById(string id, string msg)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].id == id)
                {
                    SendToClient(userList[i].client, msg);
                    break;
                }
            }
        }

        public void sendToAllClient(string msg)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                try
                {
                    SendToClient(userList[i].client, msg);
                }
                catch
                {
                    continue;
                }
            }
        }

        public void AllShouldSend(User user, string msg)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                try
                {
                    SendToClient(user.client, msg + userList[i].encode());
                }
                catch
                {
                    continue;
                }
            }
        }
    }
}
