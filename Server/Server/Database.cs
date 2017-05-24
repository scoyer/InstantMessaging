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
        private string path = "database\\userinfo.txt";
      
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
            try
            {
                StreamWriter sw = new StreamWriter(path);
                for (int i = 0; i < userList.Count; i++)
                {
                    sw.WriteLine(userList[i].id);
                    sw.WriteLine(userList[i].password);
                    sw.WriteLine(userList[i].nickname);
                    sw.WriteLine(userList[i].signature);
                }
                sw.Close();
            }
            catch
            {
                return;
            }
        }

        public void updateUser(User user)
        {
            User tmp = findUser(user.id);
            if (tmp != null)
            {
                tmp.password = user.password;
                tmp.nickname = user.nickname;
                tmp.signature = user.signature;
            }

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
