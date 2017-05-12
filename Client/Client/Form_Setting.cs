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
    public partial class Form_Setting : Form
    {
        
        private LinkWithServer lws;
        private static Form_Setting form = new Form_Setting();
        
        public Form_Setting()
        {
            InitializeComponent();
        }

        public static Form_Setting getWindow(LinkWithServer lws)
        {
            if (form.IsDisposed)
            {
                form = new Form_Setting();
            }
            else
            {

            }
            form.lws = lws;
            return form;
        }
        
        public Form_Setting(LinkWithServer LWS)
        {
            this.lws = LWS;
            InitializeComponent();
        }

        private void setting_Load(object sender, EventArgs e)
        {
            textBox1.Text = lws.localIP;
            textBox2.Text = lws.port.ToString();
            textBox3.Text = "";
            textBox3.Enabled = false;
        }
        /*
        private delegate void ChangeTextBox1Delegate(string str);
        public void ChangeTextBox1(string msg)
        {
            if (textBox1.InvokeRequired)
            {
                ChangeTextBox1Delegate add = ChangeTextBox1;
                textBox1.Invoke(add, msg);
            }
            else
            {
                textBox1.Text = msg;
            }
        }

        private delegate void ChangeTextBox2Delegate(string str);
        public void ChangeTextBox2(string msg)
        {
            if (textBox2.InvokeRequired)
            {
                ChangeTextBox2Delegate add = ChangeTextBox2;
                textBox2.Invoke(add, msg);
            }
            else
            {
                textBox2.Text = msg;
            }
        }
        */
        private void button1_Click(object sender, EventArgs e)
        {
            lws.localIP = textBox1.Text;
            try
            {
                lws.port = int.Parse(textBox2.Text);
                this.Close();
            }
            catch
            { 
                textBox3.Text = "端口号应为0~65535的整数";
            }
        }

        private void Form_Setting_MouseClick(object sender, MouseEventArgs e)
        {
            textBox3.Text = "";
        }
         
    }
}
