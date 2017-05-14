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
        
        //private static Form_Setting form = new Form_Setting();
        public bool save = false;
        public string text1;
        public string text2;
        
        public Form_Setting()
        {
            InitializeComponent();
        }
        /*
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
        */
        public Form_Setting(string localIP, int port)
        {
            InitializeComponent();
            textBox1.Text = localIP;
            textBox2.Text = port.ToString();
        }

        private void setting_Load(object sender, EventArgs e)
        {
            textBox3.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                short port = short.Parse(textBox2.Text);
                save = true;
                text1 = textBox1.Text;
                text2 = textBox2.Text;
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
