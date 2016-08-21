using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using WinMgr;

namespace WinMgr.Specifications
{
    [TestFixture]
    public class WindowLocationManagerSpecification
    {
        private Mock<IWindowLocator> _locator;
        private WindowControllerStub _controller;
        private Mock<IScreen> _screen;
        private IWindowLocationManager _subject;
        private Mock<IWindow> _currentWindow;
        private IntPtr _windowPointer = new IntPtr(99);
        private IntPtr _windowPointer1 = new IntPtr(1);
        private IntPtr _windowPointer2 = new IntPtr(2);
        private int SCREEN_WIDTH = 100;
        private int SCREEN_HEIGHT = 200;

        [SetUp]
        public void SetUp()
        {
            _locator = new Mock<IWindowLocator>();
            _controller = new WindowControllerStub();
            _currentWindow = new Mock<IWindow>();
            _currentWindow.SetupGet(x => x.Pointer).Returns(_windowPointer);
            _screen = new Mock<IScreen>();
            _screen.SetupGet(x => x.Width).Returns(SCREEN_WIDTH);
            _screen.SetupGet(x => x.Height).Returns(SCREEN_HEIGHT);
            _locator.Setup(x => x.GetCurrentWindow()).Returns(_currentWindow.Object);
            _subject = new WindowLocationManager(_locator.Object, _controller, _screen.Object);
        }
      

        [Test]
        public void Should_Maximise_Current_Window()
        {
            //Arrange

            //Act
            _subject.Maximise();

            //Assert
            Assert.AreEqual(0, _controller.Windows[_windowPointer].XLocation);
            Assert.AreEqual(0, _controller.Windows[_windowPointer].YLocation);
            Assert.AreEqual(_screen.Object.Width, _controller.Windows[_windowPointer].Width);
            Assert.AreEqual(_screen.Object.Height, _controller.Windows[_windowPointer].Height);
        }

        [Test]
        public void Should_Move_Current_Window_To_Left_Half()
        {
            //Arrange

            //Act
            _subject.MoveLeft();

            //Assert
            AssertInLeftHalf(_controller.Windows[_windowPointer]);
        }

        [Test]
        public void Should_Move_Current_Window_To_Right_Half()
        {
            //Arrange

            //Act
            _subject.MoveRight();

            //Assert
            Assert.AreEqual(_screen.Object.Width / 2, _controller.Windows[_windowPointer].XLocation);
            Assert.AreEqual(0, _controller.Windows[_windowPointer].YLocation);
            Assert.AreEqual(_screen.Object.Width / 2, _controller.Windows[_windowPointer].Width);
            Assert.AreEqual(_screen.Object.Height, _controller.Windows[_windowPointer].Height);
        }

        [Test]
        public void Should_Move_Left_Half_Window_To_Left_Third()
        {
            //Arrange
            var window = WindowStub.LeftHalfWindow(_windowPointer, _screen.Object);

            _locator.Reset();
            _locator.Setup(x => x.GetCurrentWindow()).Returns(window);

            //Act
            _subject.MoveLeft();

            //Assert
            AssertInLeftThird(_controller.Windows[_windowPointer]);
        }

        [Test]
        public void Should_Move_Left_Third_Window_To_Left_Half()
        {
            //Arrange
            var window = WindowStub.LeftThirdWindow(_windowPointer, _screen.Object);

            _locator.Reset();
            _locator.Setup(x => x.GetCurrentWindow()).Returns(window);

            //Act
            _subject.MoveRight();

            //Assert
            AssertInLeftHalf(_controller.Windows[_windowPointer]);
        }

        private void AssertInLeftHalf(IWindow window)
        {
            Assert.AreEqual(0, window.XLocation);
            Assert.AreEqual(0, window.YLocation);
            Assert.AreEqual(_screen.Object.Width / 2, window.Width);
            Assert.AreEqual(_screen.Object.Height, window.Height);
        }

        private void AssertInLeftThird(IWindow window)
        {
            Assert.AreEqual(0, window.XLocation);
            Assert.AreEqual(0, window.YLocation);
            Assert.AreEqual(_screen.Object.Width / 3, window.Width);
            Assert.AreEqual(_screen.Object.Height, window.Height);
        }

        private void AssertInRightTwoThirds(IWindow window)
        {
            Assert.AreEqual((_screen.Object.Width / 3) + 1, window.XLocation);
            Assert.AreEqual(0, window.YLocation);
            Assert.AreEqual(_screen.Object.Width - (_screen.Object.Width / 3) , window.Width);
            Assert.AreEqual(_screen.Object.Height, window.Height);
        }
    }
}

public class WindowControllerStub : IWindowController
{
    public Dictionary<IntPtr, WindowInfo> Windows = new Dictionary<IntPtr, WindowInfo>(); 

    public void SetWindowLocation(IntPtr windowPointer, int xLocation, int yLocation, int width, int height)
    {
        Windows[windowPointer] = new WindowInfo { XLocation = xLocation, YLocation = yLocation, Width = width, Height = height };
    }

    public class WindowInfo : IWindow
    {
        public IntPtr Pointer { get; set; }
        public int XLocation { get; set; }
        public int YLocation { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}

public class WindowStub : IWindow
{
    public WindowStub(IntPtr ptr)
    {
        Pointer = ptr;
    }

    public WindowStub(IntPtr ptr, int xLocation, int yLocation, int width, int height) : this(ptr)
    {
        XLocation = xLocation;
        YLocation = yLocation;
        Width = width;
        Height = height;
    }
    
    public IntPtr Pointer { get; private set; }
    public int XLocation { get; private set; }
    public int YLocation { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }

    public static IWindow LeftHalfWindow(IntPtr ptr, IScreen screen)
    {
        return new WindowStub(
                ptr,
                0,
                0,
                screen.Width / 2,
                screen.Height);
    }

    public static IWindow RightHalfWindow(IntPtr ptr, IScreen screen)
    {
        return new WindowStub(
                ptr,
                screen.Width / 2,
                0,
                screen.Width / 2,
                screen.Height);
    }

    public static IWindow LeftThirdWindow(IntPtr ptr, IScreen screen)
    {
        return new WindowStub(
                ptr,
                0,
                0,
                screen.Width / 3,
                screen.Height);
    }

    public override bool Equals(object obj)
    {
        var o = obj as WindowStub;
        if (o == null) return false;

        return o.Pointer == Pointer;
    }
}

