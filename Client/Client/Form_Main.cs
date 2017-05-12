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
        public LinkWithServer lws;
        public Control_FriendList friendList;
        public Control_GroupChat groupChat;

        public Form_Main()
        {
            InitializeComponent();
        }

        public Form_Main(User user, LinkWithServer lws)
        {
            InitializeComponent();
            this.user = user;
            this.lws = lws;
            lws.form = this;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = user.nickname;
            textBox2.Text = user.signature;
            comboBox1.Items.Add("在线");
            comboBox1.Items.Add("离线");
            comboBox1.Text = "在线";
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            friendList = new Control_FriendList();
            groupChat = new Control_GroupChat();
            friendList.Show();
            groupBox1.Controls.Add(friendList);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            friendList.Show();
            this.groupBox1.Controls.Clear();
            this.groupBox1.Controls.Add(friendList);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            groupChat.Show();
            this.groupBox1.Controls.Clear();
            this.groupBox1.Controls.Add(groupChat);
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            lws.close(user.password, textBox1.Text, textBox2.Text);
        }
    }
}
