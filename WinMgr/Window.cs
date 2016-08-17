using System;

namespace WinMgr
{
    public class Window : IWindow
    {
        public Window(IntPtr pointer)
        {
            Pointer = pointer;
        }

        public IntPtr Pointer { get; private set; }
    }
}