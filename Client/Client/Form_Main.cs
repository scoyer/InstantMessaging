using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Form_Main : Form
    {

        public User user;
        public Class_LinkWithServer lws;
        public Control_FriendList friendList;
        public Control_Reminder reminder;

        public Form_Main()
        {
            InitializeComponent();
        }

        public Form_Main(User user, Class_LinkWithServer lws)
        {
            InitializeComponent();
            this.user = user;
            this.lws = lws;
            lws.form = this;
            friendList = new Control_FriendList(lws.lwc);
            reminder = new Control_Reminder(lws.lwc);
            lws.start(user);
            friendList.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.comboBox1.SelectedIndex = 0;
            this.notifyIcon1.Visible = true;
            textBox1.Text = user.nickname;
            textBox2.Text = user.signature;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            groupBox1.Controls.Add(friendList);
            groupBox1.Controls.Add(reminder);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            friendList.Show();
            this.groupBox1.Controls.Clear();
            this.groupBox1.Controls.Add(friendList);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            reminder.Show();
            this.groupBox1.Controls.Clear();
            this.groupBox1.Controls.Add(reminder);
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            close();
        }

        private void close()
        {
            lws.sendToServer(string.Format("logout,{0},{1},{2}", user.password, textBox1.Text, textBox2.Text));
            lws.close();
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
        
        private delegate void offlineDelegate();
        public void offline()
        {
            if (this.comboBox1.InvokeRequired)
            {
                offlineDelegate d = offline;
                comboBox1.Invoke(d);
            }
            else
            {
                this.comboBox1.SelectedIndex = 1;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (lws.Quit == true)
                {
                    string msg = lws.login(user.id, user.password);
                    if (msg.Split(',')[0] != "login_success")
                    {
                        comboBox1.SelectedIndex = 1;
                    }
                    else
                    {
                        lws.start(user);
                    }
                }
            }
            else
            {
                close();
            }
        }
    }
}
