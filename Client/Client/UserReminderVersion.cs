using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class UserReminderVersion
    {
        public string id;
        public string nickname;
        public Form_ChatWindow form;
        public int count;

        public UserReminderVersion()
        { 
        
        }

        public UserReminderVersion(User user)
        {
            this.id = user.id;
            this.nickname = user.nickname;
            this.form = user.form;
            this.count = 1;
        }

        public override string ToString()
        {
            return id + "(" + nickname + ")\n" + "未读信息（" + count.ToString() + ")";
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
            UserReminderVersion user = (UserReminderVersion)obj;
            return this.id.Equals(user.id);
        }

        public override int GetHashCode()
        {
            int ret = this.id.GetHashCode();
            ret += this.nickname.GetHashCode();
            ret += this.form.GetHashCode();
            ret += this.count.GetHashCode();
            return ret;
        }
    }
}
