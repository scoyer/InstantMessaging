using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class User
    {
        public string id;
        public string password;
        public string nickname;
        public string signature;
        public string photo;
        public string localIP;
        public int port;
        public int listen_port;

        public User()
        {
            
        }

        public User(string[] content)
        {
            this.localIP = content[1];
            this.port = int.Parse(content[2]);
            this.listen_port = int.Parse(content[3]);
            this.id = content[4];
            this.nickname = content[5];
            this.signature = content[6];
        }

        public override string ToString()
        {
            return id + "(" + nickname + ")\n" + signature;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if ((obj.GetType().Equals(this.GetType())) == false)
            {
                return false;
            }
            User user = (User)obj;
            return this.id.Equals(user.id);
        }

        public override int GetHashCode()
        {
            int ret = this.id.GetHashCode();
            ret += this.password.GetHashCode();
            ret += this.nickname.GetHashCode();
            ret += this.signature.GetHashCode();
            ret += this.photo.GetHashCode();
            ret += this.localIP.GetHashCode();
            ret += this.port.GetHashCode();
            ret += this.listen_port.GetHashCode();
            return ret;
        }
    }
}
