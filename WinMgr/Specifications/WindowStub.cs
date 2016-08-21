using System;

namespace WinMgr.Specifications
{
    public class WindowStub : IWindow
    {
        public WindowStub() { }

        public WindowStub(IntPtr ptr)
        {
            Pointer = ptr;
        }

        public int Height
        {
            get; set;
        }

        public IntPtr Pointer
        {
            get; set;
        }

        public int Width
        {
            get; set;
        }

        public int XLocation
        {
            get; set;
        }

        public int YLocation
        {
            get; set;
        }
    }
}
