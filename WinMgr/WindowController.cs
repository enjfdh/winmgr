using System;
using System.Runtime.InteropServices;

namespace WinMgr
{
    public class WindowController : IWindowController
    {
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        private static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private const int SWP_SHOWWINDOW = 0x0040;
        private const int HWND_TOP = 0;
        private const int MINIMISE = 6;
        private const int RESTORE = 9;

        public void SetWindowLocation(IntPtr windowPointer, int xLocation, int yLocation, int width, int height)
        {
            SetWindowPos(windowPointer, HWND_TOP, xLocation, yLocation, width, height, SWP_SHOWWINDOW);
        }

        public void ShowWindow(IntPtr windowPointer)
        {
            ShowWindowAsync(windowPointer, RESTORE);
        }

        public void MinimiseWindow(IntPtr windowPointer)
        {
            ShowWindowAsync(windowPointer, MINIMISE);
        }
    }
}