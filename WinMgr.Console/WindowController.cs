using System;
using System.Runtime.InteropServices;

namespace WinMgr.Console
{
    public class WindowController : IWindowController
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        private const int SWP_SHOWWINDOW = 0x0040;
        private const int HWND_TOP = 0;

        public void SetWindowLocation(IntPtr windowPointer, int xLocation, int yLocation, int width, int height)
        {
            SetWindowPos(windowPointer, HWND_TOP, xLocation, yLocation, width, height, SWP_SHOWWINDOW);
        }
    }
}