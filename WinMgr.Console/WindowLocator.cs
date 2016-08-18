using System;
using System.Runtime.InteropServices;

namespace WinMgr.Console
{
    public class WindowLocator : IWindowLocator
    {
        [DllImport("user32.dll")]
        private  static extern IntPtr GetForegroundWindow();

        public IWindow GetCurrentWindow()
        {
            return new Window(GetForegroundWindow());
        }
    }
}