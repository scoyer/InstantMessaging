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

        public Form_Main()
        {
            InitializeComponent();
        }

        public Form_Main(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            textBox1.Text = user.nickname;
            textBox2.Text = user.signature;
            comboBox1.Items.Add("在线");
            comboBox1.Items.Add("离线");
            comboBox1.SelectedIndex = 0;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Control_FriendList childForm = new Control_FriendList();
            childForm.Show();
            this.groupBox1.Controls.Add(childForm);
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
