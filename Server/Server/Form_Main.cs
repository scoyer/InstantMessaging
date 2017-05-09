using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form_Main : Form
    {
        public Control_UserManage usermanager;
        public Control_Log w2;
        public Control_ServerInfo serverinfo;
        public LinkWithClient lwc;

        public Form_Main()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lwc = new LinkWithClient(this);
            usermanager = new Control_UserManage();
            w2 = new Control_Log();
            serverinfo = new Control_ServerInfo(lwc);
            groupBox1.Controls.Add(serverinfo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverinfo.Show();
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(serverinfo);
            //usermanager.AddItemToListBox1("振豪");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            w2.Show();
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(w2);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            usermanager.Show();
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(usermanager);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lwc.stop();
        }

    }
}
