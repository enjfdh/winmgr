using System;
using System.Runtime.InteropServices;

namespace WinMgr
{
    public class WindowLocator : IWindowLocator
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        public IWindow GetCurrentWindow()
        {
            var ptr = GetForegroundWindow();

            return new Window(ptr);
        }
    }
}