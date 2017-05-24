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
        public Control_Log log;
        public Control_ServerInfo serverinfo;
        public LinkWithClient lwc;

        public Form_Main()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            usermanager = new Control_UserManage();
            log = new Control_Log();
            serverinfo = new Control_ServerInfo();
            groupBox1.Controls.Add(serverinfo);
            lwc = new LinkWithClient(this);
            usermanager.lwc = lwc;
            serverinfo.lwc = lwc;
            serverinfo.Show();
            log.Show();
            usermanager.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(serverinfo);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(usermanager);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            lwc.stop();
        }

        private void log_button_Click(object sender, EventArgs e)
        {
            groupBox1.Controls.Clear();
            groupBox1.Controls.Add(log);
        }

    }
}
