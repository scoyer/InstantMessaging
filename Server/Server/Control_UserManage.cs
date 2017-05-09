using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Server
{
    public partial class Control_UserManage : UserControl
    {
        public Control_UserManage()
        {
            InitializeComponent();
        }

        private void UserManager_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private delegate void AddItemToListBox1Delegate(string str);
        public void AddItemToListBox1(object o)
        {
            string msg = (string)o;
            if (listBox1.InvokeRequired)
            {
                AddItemToListBox1Delegate add = AddItemToListBox1;
                listBox1.Invoke(add, msg);
            }
            else
            {
                listBox1.Items.Add(msg);
            }
        }
    }
}
