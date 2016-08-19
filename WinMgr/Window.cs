using System;

namespace WinMgr
{
    public class Window : IWindow
    {
        public Window(IntPtr pointer, int xLocation, int yLocation, int width, int height)
        {
            Pointer = pointer;
            XLocation = xLocation;
            YLocation = yLocation;
            Height = height;
            Width = width;
        }

        public int Height
        {
            get; private set;

        }

        public IntPtr Pointer { get; private set; }

        public int Width
        {
            get; private set;
        }

        public int XLocation
        {
            get; private set;
        }

        public int YLocation
        {
            get; private set;
        }
    }
}