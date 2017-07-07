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
    public partial class Control_Reminder : UserControl
    {
        public Class_LinkWithClient lwc;

        public Control_Reminder()
        {
            InitializeComponent();
        }

        public Control_Reminder(Class_LinkWithClient lwc)
        {
            InitializeComponent();
            this.lwc = lwc;
            User test = new User();
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
                //封装成UserReminderVersion
                UserReminderVersion _user = new UserReminderVersion(user);
                int index = -1;
                for (int i = 0; i < listBox1.Items.Count; i++)
                {
                    if (_user.Equals(listBox1.Items[i]))
                    {
                        index = i;
                        _user = listBox1.Items[i] as UserReminderVersion;
                        _user.count++;
                        listBox1.Items.RemoveAt(i);
                        listBox1.Items.Add(_user);
                        break;
                    }
                }
                if (index == -1)
                {
                    listBox1.Items.Add(_user);
                }
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
                //封装成UserReminderVersion
                UserReminderVersion _user = new UserReminderVersion(user);
                listBox1.Items.Remove(_user);
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
                UserReminderVersion _user = (UserReminderVersion)listBox1.SelectedItem;
                User user = lwc.GetUser(_user.id);
                user.form.Visible = true;
                user.form.Activate();
                RemoveItemToListBox1(user);
            }
            catch
            {

            }
        }

        

    }
}
