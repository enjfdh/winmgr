using System;

namespace WinMgr
{
    public class WorkArea : IWorkAreaOrganiser
    {
        public IWindow LeftWindow
        {
            get; private set;
        }

        public IWindow RightWindow
        {
            get; private set;
        }

        public void Left()
        {
            throw new NotImplementedException();
        }

        public void Right()
        {
            throw new NotImplementedException();
        }

        public void SetLeftWindow(IWindow window)
        {
            LeftWindow = window;
        }

        public void SetRightWindow(IWindow window)
        {
            RightWindow = window;
        }
    }
}