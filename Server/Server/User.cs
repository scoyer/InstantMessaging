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
        public string localIP;
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
            //format: IP,listen_port,id,password,nickname,signature
            return string.Format("{0},{1},{2},{3},{4},{5}", localIP, listen_port.ToString(), id, password, nickname, signature);
        }

    }
}
