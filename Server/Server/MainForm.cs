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
    public partial class MainForm : Form
    {
        public UserManager usermanager;
        public window2 w2;
        public ServerInfo serverinfo;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            usermanager = new UserManager();
            w2 = new window2();
            serverinfo = new ServerInfo();
            groupBox1.Controls.Add(serverinfo);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serverinfo.Show();
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(serverinfo);
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

    }
}
