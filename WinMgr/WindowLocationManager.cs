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
        public void MoveRight()
        {
            var currentWindow = CurrentWindow;

            if (IsInLeftThird(currentWindow))
            {
                MoveToLeftHalf(currentWindow);
            }
            else
            {
                _controller.SetWindowLocation(
                    currentWindow.Pointer,
                    _screen.Width / 2,
                    0,
                    _screen.Width / 2,
                    _screen.Height);
            }
        }

        private IWindow CurrentWindow
        {
            get { return _locator.GetCurrentWindow(); }
        }

        private bool IsInLeftThird(IWindow window)
        {
            return IsInLeftSector(window, 3);
        }

        private bool IsInLeftHalf(IWindow window)
        {
            return IsInLeftSector(window, 2);
        }

        private bool IsInLeftSector(IWindow window, int divisor)
        {
            return window.XLocation == 0
                && window.YLocation == 0
                && window.Width == _screen.Width / divisor
                && window.Height == _screen.Height;
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