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

        public void MoveLeft()
        {
            var currentWindow = CurrentWindow;

            if (IsInLeftHalf(currentWindow))
            {
                MoveToLeftThird(currentWindow);
            }
            else
            {
                MoveToLeftHalf(currentWindow);
            }
        }

        private bool IsInLeftHalf(IWindow window)
        {
            return window.XLocation == 0
                && window.YLocation == 0
                && window.Width == _screen.Width / 2
                && window.Height == _screen.Height;
        }

        public void MoveRight()
        {
            _controller.SetWindowLocation(
                _locator.GetCurrentWindow().Pointer,
                _screen.Width / 2,
                0,
                _screen.Width / 2,
                _screen.Height);
        }

        private IWindow CurrentWindow
        {
            get { return _locator.GetCurrentWindow(); }
        }

        private void MoveToLeftHalf(IWindow window)
        {
            _controller.SetWindowLocation(
                    window.Pointer,
                    0,
                    0,
                    _screen.Width / 2,
                    _screen.Height);
        }

        private void MoveToLeftThird(IWindow window)
        {
            _controller.SetWindowLocation(
                    window.Pointer,
                    0,
                    0,
                    _screen.Width / 3,
                    _screen.Height);
        }

    }
}