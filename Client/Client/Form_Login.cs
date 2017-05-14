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
        public LinkWithServer lws;
        public User user = new User();

        public Form_Login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            lws = new LinkWithServer();
            this.KeyPreview = true;
            Form_ChatWindow form = new Form_ChatWindow();
            form.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            string[] content = lws.start(textBox1.Text, textBox2.Text).Split(',');
            switch (content[0])
            {
                case "link_fail":
                    textBox3.Text = "连接不上服务器";
                    //MessageBox.Show("连接不上服务器");
                    break;
                case "login_repeat":
                    textBox3.Text = "请勿重复登录";
                   // MessageBox.Show("请勿重复登录");
                    break;
                case "login_nonexistent":
                    textBox3.Text = "用户不存在";
                    //MessageBox.Show("用户不存在");
                    break;
                case "login_incorrect":
                    textBox3.Text = "密码错误";
                    //MessageBox.Show("密码错误");
                    break;
                case "login_success":
                    //MessageBox.Show("登录成功");
                    user.id = textBox1.Text;
                    user.password = textBox2.Text;
                    user.localIP = content[1];
                    user.port = int.Parse(content[2]);
                    user.listen_port = int.Parse(content[3]);
                    user.nickname = content[5];
                    user.signature = content[6];
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    break;
                default:
                    textBox3.Text = "UNKnown Error";
                    //MessageBox.Show("UNKnown Error");
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            Form_Setting form = new Form_Setting(lws.localIP, lws.port);
            form.ShowDialog(this);
            if (form.save) {
                lws.localIP = form.text1;
                lws.port = int.Parse(form.text2);
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
            //MessageBox.Show("KeyCode:" + e.KeyCode + ",\r\n KeyData:" + e.KeyData + ",\r\n KeyValue:" + e.KeyValue);  
        }

        private void Form_Login_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = "";
        }

    }
}
