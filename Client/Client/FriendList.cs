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
    public partial class FriendList : UserControl
    {
        public FriendList()
        {
            InitializeComponent();
            listBox1.ItemHeight = 12;
            listBox1.Items.Add("振豪");
            listBox1.Items.Add("振宇");
        }
    }
}
