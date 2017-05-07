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

        public Database database;

        public LinkWithClient()
        {
            userList = new List<User>();
            database = new Database();
        }

        public int start(string localIP, int port)
        {
            try
            {
                database.readFromDatabase();
                listener = new TcpListener(IPAddress.Parse(localIP), port);
                listener.Start();
                Thread thread = new Thread(ConnectWithClient);
                thread.Start();
            }
            catch
            {
                return 0;
            }
            return 1;
        }

        public void stop()
        {
            try
            {
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
                User user = new User();
                user.client = client;
                userList.Add(user);
                Thread thread = new Thread(ReceiveFromClient);
                thread.Start(user);
            }
        }

        void ReceiveFromClient(object o)
        {
            User user = (User)o;
            TcpClient client = user.client;
            BinaryReader br = new BinaryReader(client.GetStream());
            string message;
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
                        //Format IP,port,listen_port,id,password
                        //check validity
                        string id = content[3];
                        string password = content[4];
                        User login_user = database.findUser(id);
                        if (login_user == null) 
                        {
                        //用户不存在
                        }
                        else if (password != login_user.password)
                        {
                        //用户密码不正确
                        }
                        else
                        { 
                        //用户登陆成功
                        }
                        break;
                    case "logout":
                        //Format id,password,nickname,signature
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

        void SentToClient(TcpClient client, string msg)
        {
            try
            {
                BinaryWriter bw = new BinaryWriter(client.GetStream());
                bw.Write(msg);
                bw.Flush();
            }
            catch
            {

            }
        }
    }
}
