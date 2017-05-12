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
    public partial class Form_ChatWindow : Form
    {
        public Form_ChatWindow()
        {
            InitializeComponent();
        }

        private void Form_ChatWindow_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(button3, "发送文件");
            toolTip2.SetToolTip(button4, "窗口抖动");
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
