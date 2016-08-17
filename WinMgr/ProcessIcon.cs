using System;
using System.Windows.Forms;
using System.Drawing;

namespace WinMgr
{
    public class ProcessIcon : IDisposable
    {
        private NotifyIcon notifyIcon;
        private ContextMenu contextMenu;

        public ProcessIcon()
        {
            notifyIcon = new NotifyIcon();
            notifyIcon.Icon = SystemIcons.Application;

            contextMenu = new ContextMenu();
            contextMenu.MenuItems.Add(new MenuItem("Exit", OnExitClicked));

            notifyIcon.ContextMenu = contextMenu;
        }

        private void OnExitClicked(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            OnExit?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler OnExit;


        public void Dispose()
        {
            notifyIcon.Visible = false;
        }

        public void Display()
        {
            notifyIcon.Visible = true;
        }
    }
}
