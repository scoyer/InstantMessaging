using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace Server
{
    public class Database
    {
        private List<User> userList;
        private string path = "E:\\database\\userinfo.txt";
      
        public Database()
        {
            userList = new List<User>();
        }

        public void readFromDatabase()
        {
            try
            {
                StreamReader sr = new StreamReader(path, Encoding.UTF8);
                while (!sr.EndOfStream)
                {
                    User user = new User();
                    try
                    {
                        user.id = sr.ReadLine();
                        user.password = sr.ReadLine();
                        user.nickname = sr.ReadLine();
                        user.signature = sr.ReadLine();
                        userList.Add(user);
                        //Console.WriteLine(user.id);
                    }
                    catch
                    {
                        break;
                    }
                }
                sr.Close();
            }
            catch
            {
                return;
            }
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
