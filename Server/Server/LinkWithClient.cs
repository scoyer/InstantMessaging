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

namespace Server
{
    public class LinkWithClient
    {
        public TcpListener listener;
        public List<User> userList;
        public bool Quit;

        public Database database;
        public MainForm form;

        public LinkWithClient()
        {

        }

        public LinkWithClient(MainForm FORM) {
            this.form = FORM;
        }

        public int start(string localIP, int port)
        {
            try
            {
                Quit = false;
                userList = new List<User>();
                database = new Database();
                database.readFromDatabase();
                listener = new TcpListener(IPAddress.Parse(localIP), port);
                listener.Start();
                Thread thread = new Thread(ConnectWithClient);
                thread.Start();
            }
            catch
            {
                //Console.WriteLine("================出错了");
                return 0;
            }
            return 1;
        }

        public void stop()
        {
            try
            {
                Quit = true;
                database.writeToDatebase();
                listener.Stop();
            }
            catch
            {
                return;
            }
        }

        void ConnectWithClient()
        {
            TcpClient client = null;
            while (true)
            {
                //Console.WriteLine("waiting");
                try
                {
                    client = listener.AcceptTcpClient();
                }
                catch
                {
                    break;
                }
                //Console.WriteLine("coming");
                Thread thread = new Thread(ReceiveFromClient);
                thread.Start(client);
            }
        }

        void ReceiveFromClient(object o)
        {
            TcpClient client = (TcpClient)o;
            BinaryReader br = new BinaryReader(client.GetStream());
            string message;
            User user = null;
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
                            user.localIP = content[1];
                            user.port = (client.Client.RemoteEndPoint as IPEndPoint).Port;
                            user.listen_port = int.Parse(content[2]);
                            sendToAllClient("login," + user.encode());
                            SendToClient(client, "login_success," + user.encode());
                            user.client = client;
                            userList.Add(user);
                            ok = true;
                            //form.usermanager.AddItemToListBox1(id + "login");
                        }
                        if (!ok) return; 
                        break;
                    case "logout":
                        //Format password,nickname,signature
                        userList.Remove(user);
                        sendToAllClient("logout," + user.id);
                        user.password = content[1];
                        user.nickname = content[2];
                        user.signature = content[3];
                        database.updateUser(user);
                        break;
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
    }
}
