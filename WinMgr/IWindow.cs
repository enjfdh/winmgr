using System;

namespace WinMgr
{
    public interface IWindow
    {
        IntPtr Pointer { get; }
        int XLocation { get; }
        int YLocation { get; }
        int Height { get; }
        int Width { get; }
    }
}