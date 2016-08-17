using System;
using System.Runtime.InteropServices;

namespace WinMgr
{
    public class WindowLocationController
    {

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

       
    }
}
