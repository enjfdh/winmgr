using System;
using System.Runtime.InteropServices;

namespace WinMgr
{
    public class WindowLocator : IWindowLocator
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        public IWindow GetCurrentWindow()
        {
            var ptr = GetForegroundWindow();
            var rect = new Rect();
            GetWindowRect(ptr, ref rect);

            return new Window(ptr, 
                rect.Left, 
                rect.Top, 
                rect.Right - rect.Left, 
                rect.Bottom - rect.Top);
        }

        public struct Rect
        {
            public int Left { get; set; }
            public int Top { get; set; }
            public int Right { get; set; }
            public int Bottom { get; set; }
        }
    }
}