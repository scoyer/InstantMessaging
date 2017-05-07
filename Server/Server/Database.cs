using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Database
    {
        List<User> userList;

        public Database()
        {
            userList = new List<User>();
        }

        public void readFromDatabase()
        {

        }

        public void writeToDatebase()
        { 
            
        }

        public void updateUser(User user)
        {
            User tmp = findUser(user.id);
            if (tmp != null) tmp = user;
        }

        public User findUser(string id)
        {
            for (int i = 0; i < userList.Count; i++)
            {
                if (userList[i].id == id)
                    return userList[i];
            }
            return null;
        }

        public void addUser(User user)
        {
            if (findUser(user.id) != null) return;
            userList.Add(user);
        }

        public void delUser(User user)
        {
            if (findUser(user.id) == null) return;
            userList.Remove(user);
        }
    }
}
