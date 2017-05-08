using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Client
{
    public partial class login : Form
    {
        public LinkWithServer lws;
        public MainForm form;

        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            lws = new LinkWithServer();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] content = lws.start(textBox1.Text, textBox2.Text).Split(',');
            switch (content[0])
            {
                case "link_fail":
                    MessageBox.Show("连接不上服务器");
                    break;
                case "login_repeat":
                    MessageBox.Show("请勿重复登录");
                    break;
                case "login_nonexistent":
                    MessageBox.Show("用户不存在");
                    break;
                case "login_incorrect":
                    MessageBox.Show("密码错误");
                    break;
                case "login_success":
                    MessageBox.Show("登录成功");
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    break;
                default:
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new setting(lws);
            form.Show();
        }
    }
}
