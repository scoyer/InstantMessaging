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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
            comboBox1.Items.Add("在线");
            comboBox1.Items.Add("离线");
            comboBox1.SelectedIndex = 0;
            this.IsMdiContainer = true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form childForm = new Friend();
            childForm.BackColor = System.Drawing.Color.Yellow;
            childForm.StartPosition = FormStartPosition.Manual;
            childForm.Location = new Point(0, 17);
            Button bt = new Button();
            bt.Text = "振豪";
            childForm.Controls.Add(bt);
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.MdiParent = this;
            childForm.Parent = this.panel1;
            childForm.Show();
        }
    }
}
