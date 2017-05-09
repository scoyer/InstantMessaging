using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Control_FriendList : UserControl
    {
        public Control_FriendList()
        {
            InitializeComponent();
            listBox1.ItemHeight = 12;
            listBox1.Items.Add("振豪");
            listBox1.Items.Add("振豪");
            listBox1.Items.Add("振豪");
            listBox1.Items.Add("振豪");
            listBox1.Items.Add("振豪");
            listBox1.Items.Add("振豪");
        }
    }
}
