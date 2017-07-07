using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Control_Log : UserControl
    {
        public Control_Log()
        {
            InitializeComponent();
        }

        //add item
        private delegate void AddItemToListBox1Delegate(string msg);
        public void AddItemToListBox1(string msg)
        {
            if (listBox1.InvokeRequired)
            {
                AddItemToListBox1Delegate d = AddItemToListBox1;
                listBox1.Invoke(d, msg);
            }
            else
            {
                listBox1.Items.Add(msg);
            }
        }

        //remove item
        private delegate void RemoveItemToListBox1Delegate(String msg);
        public void RemoveItemToListBox1(String msg)
        {
            if (listBox1.InvokeRequired)
            {
                RemoveItemToListBox1Delegate d = RemoveItemToListBox1;
                listBox1.Invoke(d, msg);
            }
            else
            {
                listBox1.Items.Remove(msg);
                listBox1.SelectedIndex = listBox1.Items.Count - 1;
                listBox1.ClearSelected();
            }
        }

    }
}
