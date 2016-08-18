using System;

namespace WinMgr
{
    public class WindowLocationManager : IWindowLocationManager
    {
        private IWindowLocator _locator;
        private IWindowController _controller;
        private IScreen _screen;

        public WindowLocationManager(IWindowLocator locator, IWindowController controller, IScreen screen)
        {
            _locator = locator;
            _controller = controller;
            _screen = screen;
        }

        public void Maximise()
        {
            _controller.SetWindowLocation(
                _locator.GetCurrentWindow().Pointer,
                0,
                0,
                _screen.Width,
                _screen.Height);
        }

        public void LeftHalf()
        {
            _controller.SetWindowLocation(
                _locator.GetCurrentWindow().Pointer,
                0,
                0,
                _screen.Width / 2,
                _screen.Height);
        }
    }
}