using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Client
{
    class LinkWithServer
    {
        private TcpClient client;
        private 

        LinkWithServer()
        { 
        
        }

        LinkWithServer(TcpClient client)
        {
            this.client = client;
        }

        bool sendToServer(string message)
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

        void revcWithServer()
        {
            try
            {

            }
            catch
            { 
                
            }
        }

        void close()
        {
            if (client != null)
            {
                client.Close();
            }
        }
    }
}
