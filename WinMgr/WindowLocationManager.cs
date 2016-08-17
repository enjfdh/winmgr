using System;

namespace WinMgr
{
    public class WindowLocationManager : IWindowLocationManager
    {
        private IWindowLocator _locator;
        private IWindowController _controller;

        public WindowLocationManager(IWindowLocator locator, IWindowController controller)
        {
            _locator = locator;
            _controller = controller;
        }

        public void Maximise()
        {
            _controller.Maximise(_locator.GetCurrentWindow());
        }

        public void MoveLeft()
        {
            _controller.MoveLeft(_locator.GetCurrentWindow());
        }
    }
}