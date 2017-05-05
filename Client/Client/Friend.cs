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
    public partial class Friend : Form
    {
        public Friend()
        {
            InitializeComponent();
            for (int i = 0; i < 2; i++)   //添加10行数据
            {
                ListViewItem lvi = new ListViewItem();

                lvi.ImageIndex = i;     //通过与imageList绑定，显示imageList中第i项图标

                lvi.Text = "subitem" + i;

                this.listView1.Items.Add(lvi);
            }
            listView1.SmallImageList = imageList1;
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
        }
    }
}
