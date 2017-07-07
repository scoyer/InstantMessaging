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
    public partial class Form_Login : Form
    {
        public Class_LinkWithServer lws;
        public User me;

        public Form_Login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            lws = new Class_LinkWithServer();
            this.KeyPreview = true;
            //Form_Main main = new Form_Main();
            //main.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            string[] content = lws.login(textBox1.Text, textBox2.Text).Split(',');
            switch (content[0])
            {
                case "link_fail":
                    textBox3.Text = "连接不上服务器";
                    break;
                case "login_repeat":
                    textBox3.Text = "请勿重复登录";
                    break;
                case "login_nonexistent":
                    textBox3.Text = "用户不存在";
                    break;
                case "login_incorrect":
                    textBox3.Text = "密码错误";
                    break;
                case "login_success":
                    me = new User(content);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    break;
                default:
                    textBox3.Text = "UNKnown Error";
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            Form_Setting form = new Form_Setting(lws.serverIP, lws.port);
            if (form.ShowDialog(this) == DialogResult.OK) {
                lws.serverIP = form.textBox1.Text;
                lws.port = int.Parse(form.textBox2.Text);
            }
        }

        private void Login_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void Form_Login_KeyDown(object sender, KeyEventArgs e)
        {
            textBox3.Text = "";
            if (e.KeyValue == 13)
            {
                button2.PerformClick();
            }
        }

        private void Form_Login_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = "";
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.Activate();
            }
        }

    }
}
