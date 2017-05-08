using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class UserManager : UserControl
    {
        public UserManager()
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
        public void AddItemToListBox1(string msg)
        {
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
