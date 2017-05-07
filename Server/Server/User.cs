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
    public class User
    {
        public TcpClient client;
        public string ipaddress;
        public int port;
        public int listen_port;
        public string id;
        public string password;
        public string nickname;
        public string signature;

        public User()
        { 
        
        }

        public string encode()
        { 
            //format: IP,listen_port,nickname,signature
            return string.Format("{0},{1},{2},{3},{4}",ipaddress, listen_port.ToString(),nickname,signature);
        }

        public User decode(string code)
        {
            User user = new User();
            string[] str = code.Split(',');
            user.ipaddress = str[0];
            user.port = int.Parse(str[1]);
            user.nickname = str[2];
            user.signature = str[3];
            return user;
        }
    }
}
