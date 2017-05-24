using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form_Login login = new Form_Login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                Form_Main main = new Form_Main(login.me, login.lws);
                login.Dispose();
                Application.Run(main);
            }
        }
    }
}
