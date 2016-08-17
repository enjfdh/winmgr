using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinMgr
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

            using (var appIcon = new ProcessIcon())
            {
                appIcon.Display();
                appIcon.OnExit += AppIcon_OnExit;

                Application.Run();
            }
        }

        private static void AppIcon_OnExit(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
