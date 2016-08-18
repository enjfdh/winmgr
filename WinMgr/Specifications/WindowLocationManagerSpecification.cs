using Moq;
using NUnit.Framework;
using System;

namespace WinMgr.Specifications
{
    [TestFixture]
    public class WindowLocationManagerSpecification
    {
        private Mock<IWindowLocator> _locator;
        private Mock<IWindowController> _controller;
        private Mock<IScreen> _screen;
        private IWindowLocationManager _subject;
        private Mock<IWindow> _currentWindow;
        private IntPtr _windowPointer = new IntPtr(99);
        private int SCREEN_WIDTH = 100;
        private int SCREEN_HEIGHT = 200;

        [SetUp]
        public void SetUp()
        {
            _locator = new Mock<IWindowLocator>();
            _controller = new Mock<IWindowController>();
            _currentWindow = new Mock<IWindow>();
            _currentWindow.SetupGet(x => x.Pointer).Returns(_windowPointer);
            _screen = new Mock<IScreen>();
            _screen.SetupGet(x => x.Width).Returns(SCREEN_WIDTH);
            _screen.SetupGet(x => x.Height).Returns(SCREEN_HEIGHT);
            _locator.Setup(x => x.GetCurrentWindow()).Returns(_currentWindow.Object);
            _subject = new WindowLocationManager(_locator.Object, _controller.Object, _screen.Object);
        }
      

        [Test]
        public void Should_Maximise_Current_Window()
        {
            //Arrange

            //Act
            _subject.Maximise();

            //Assert
            _controller.Verify(x => x.SetWindowLocation(_windowPointer, 0, 0, _screen.Object.Width, 
                _screen.Object.Height));
        }

        [Test]
        public void Should_Move_Current_Window_To_Left_Half()
        {
            //Arrange

            //Act
            _subject.LeftHalf();

            //Assert
            _controller.Verify(x => x.SetWindowLocation(_windowPointer, 0, 0, _screen.Object.Width / 2,
                _screen.Object.Height));
        }
    }
}
