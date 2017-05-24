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
        
        public Form_Setting()
        {
            InitializeComponent();
        }
        
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
                int port = int.Parse(textBox2.Text);
                this.DialogResult = DialogResult.OK;
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
