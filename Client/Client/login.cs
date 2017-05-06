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
        public LinkWithServer lws = new LinkWithServer();

        public login()
        {
            InitializeComponent();
            /*
            FriendList fl = new FriendList();
            fl.Show();
             * */
            Form f = new MainForm();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(lws.hostname + "  " + lws.port.ToString());
            if (!lws.link())
            {
                MessageBox.Show("连接不上服务器");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = new setting(lws);
            form.Show();
        }
    }
}
