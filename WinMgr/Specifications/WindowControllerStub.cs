using System;
using System.Collections.Generic;

namespace WinMgr.Specifications
{
    public class WindowControllerStub : IWindowController
    {
        public Dictionary<IntPtr, WindowStub> Windows = new Dictionary<IntPtr, WindowStub>();
        
        public void SetWindowLocation(IntPtr windowPointer, int xLocation, int yLocation, int width, int height)
        {
            Windows[windowPointer] = new WindowStub
            {
                Pointer = windowPointer,
                XLocation = xLocation,
                YLocation = yLocation,
                Width = width,
                Height = height
            };
        }
    }
}
