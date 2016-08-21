using System;

namespace WinMgr
{
    public interface IWindowController
    {
        void SetWindowLocation(IntPtr windowPointer, int xLocation, int yLocation, int width, int height);
        void ShowWindow(IntPtr windowPointer);
        void MinimiseWindow(IntPtr windowPointer);
    }
}