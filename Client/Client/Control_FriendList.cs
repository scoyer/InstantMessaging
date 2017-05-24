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
        public Class_LinkWithServer lwc;

        public Control_FriendList()
        {
            InitializeComponent();
        }

        public Control_FriendList(Class_LinkWithServer lwc)
        {
            InitializeComponent();
            this.lwc = lwc;
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

        private void listBox1_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            if (e.Index == -1) return;
            e.DrawFocusRectangle();
            e.Graphics.DrawString(listBox1.Items[e.Index].ToString(), e.Font, new SolidBrush(Color.Black), e.Bounds);
        }

        private void listBox1_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = 50;
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                User user = (User)listBox1.SelectedItem;
                user = lwc.GetUser(user.id);
                if (user.client == null && lwc.LinkToFriend(user))
                {
                    user.form.Show();
                }
                else
                {
                    user.form.Visible = true;
                    user.form.Activate();
                }
                
            }
            catch
            { 
            
            }
        }

        public void ShowToolTip(string id) { 
            
        }

        private void listBox1_MouseMove(object sender, MouseEventArgs e)
        {
        }

    }
}
