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
        public LinkWithClient lwc;

        public Control_UserManage()
        {
            InitializeComponent();
        }

        private void UserManager_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int selectCount = listView1.SelectedItems.Count;
            if (selectCount > 0)
            {
                string id = listView1.SelectedItems[0].Text;
                lwc.SendToClientById(id, string.Format("talk,{0}", textBox1.Text));
                textBox1.Clear();
            }
            else
            {
                MessageBox.Show("请选择一个用户");
            }
        }

        //add item
        private delegate void AddItemDelegate(ListViewItem lvi);
        public void AddItem(ListViewItem lvi)
        {
            if (listView1.InvokeRequired)
            {
                AddItemDelegate d = AddItem;
                listView1.Invoke(d, lvi);
            }
            else
            {
                listView1.Items.Add(lvi);
            }
        }

        //remove item
        private delegate void RemoveItemDelegate(ListViewItem lvi);
        public void RemoveItem(ListViewItem lvi)
        {
            if (listView1.InvokeRequired)
            {
                RemoveItemDelegate d = RemoveItem;
                listView1.Invoke(d, lvi);
            }
            else
            {
                listView1.Items.Remove(lvi);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
