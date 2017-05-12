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
            User user = new User();
            user.id = "961042996";
            user.nickname = "scoyer";
            user.signature = "资深宅。";
            listBox1.Items.Add(user);
            this.listBox1.DrawMode = DrawMode.OwnerDrawVariable;
        }

        //add item
        private delegate void AddItemToListBox1Delegate(User user);
        public void AddItemToListBox1(User user)
        {
            if (listBox1.InvokeRequired)
            {
                AddItemToListBox1Delegate d = AddItemToListBox1;
                listBox1.Invoke(d, user);
            }
            else
            {
                listBox1.Items.Add(user);
            }
        }

        //remove item
        private delegate void RemoveItemToListBox1Delegate(User user);
        public void RemoveItemToListBox1(User user)
        {
            if (listBox1.InvokeRequired)
            {
                RemoveItemToListBox1Delegate d = RemoveItemToListBox1;
                listBox1.Invoke(d, user);
            }
            else
            {
                listBox1.Items.Remove(user);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.ClearSelected();
            }
        }

        private void d(object sender, EventArgs e)
        {

        }

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds);
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 50;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Form_ChatWindow form = new Form_ChatWindow();
            form.Show();
        }


    }
}
