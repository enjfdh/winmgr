using Moq;
using NUnit.Framework;
using System;

namespace WinMgr.Specifications.WorkAreaSpecs
{
    public abstract class WorkAreaSpecification
    {
        protected IWorkAreaOrganiser _subject;
        protected IWindow _windowLeft;
        protected IWindow _windowRight;
        protected WindowControllerStub _controller;
        protected Mock<IScreen> _screen;

        [SetUp]
        public virtual void SetUp()
        {
            _controller = new WindowControllerStub();
            _screen = new Mock<IScreen>();
            _screen.SetupGet(x => x.Height).Returns(100);
            _screen.SetupGet(x => x.Width).Returns(200);

            _subject = new WorkArea(_controller, _screen.Object);

            _windowLeft = new WindowStub(new IntPtr(0));
            _windowRight = new WindowStub(new IntPtr(1));
        }
    }

    [TestFixture]
    public class When_Organising : WorkAreaSpecification
    {
        [Test]
        public void Should_Set_Left_Window()
        {
            //Arrange

            //Act
            _subject.SetLeftWindow(_windowLeft);

            //Assert
            Assert.AreEqual(_windowLeft, _subject.LeftWindow);
        }

        [Test]
        public void Should_Set_Right_Window()
        {
            //Arrange

            //Act
            _subject.SetRightWindow(_windowRight);

            //Assert
            Assert.AreEqual(_windowRight, _subject.RightWindow);
        }
    }

    [TestFixture]
    public class When_Working : WorkAreaSpecification
    {
        #region SetUp

        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _subject.SetLeftWindow(_windowLeft);
            _subject.SetRightWindow(_windowRight);
        }


        private void AssertInLocation(WindowLocation expectedWindowLocation, IWindow window, string message)
        {
            Console.WriteLine(message + " : " + expectedWindowLocation);

            int x = 0, y = 0, width = 0, height = 0;

            height = _screen.Object.Height;
            y = 0;

            if (expectedWindowLocation == WindowLocation.LeftHalf)
            {
                x = 0;
                width = _screen.Object.Width / 2;
            }
            else if (expectedWindowLocation == WindowLocation.LeftThird)
            {
                x = 0;
                width = _screen.Object.Width / 3;
            }
            else if (expectedWindowLocation == WindowLocation.LeftTwoThirds)
            {
                x = 0;
                width = _screen.Object.Width - (_screen.Object.Width / 3);
            }
            else if (expectedWindowLocation == WindowLocation.RightHalf)
            {
                x = _screen.Object.Width / 2;
                width = (_screen.Object.Width / 2);
            }
            else if (expectedWindowLocation == WindowLocation.RightThird)
            {
                x = _screen.Object.Width - (_screen.Object.Width / 3);
                width = (_screen.Object.Width / 3);
            }
            else if (expectedWindowLocation == WindowLocation.RightTwoThirds)
            {
                x = (_screen.Object.Width / 3);
                width = _screen.Object.Width - (_screen.Object.Width / 3);
            }

            var info = _controller.Windows[window.Pointer];

            Assert.AreEqual(x, info.XLocation, "XLocation");
            Assert.AreEqual(y, info.YLocation, "YLocation");
            Assert.AreEqual(width, info.Width, "Width");
            Assert.AreEqual(height, info.Height, "Height");
        }

        public enum TestAction
        {
            Left, Right
        }

        public enum WindowLocation
        {
            LeftHalf, LeftThird, LeftTwoThirds,
            RightHalf, RightThird, RightTwoThirds
        }
        #endregion

        [Test]

        [TestCase(WindowLocation.LeftThird, WindowLocation.RightTwoThirds, 
            new TestAction[] { TestAction.Left })]

        [TestCase(WindowLocation.LeftThird, WindowLocation.RightTwoThirds,
            new TestAction[] { TestAction.Left, TestAction.Left })]

        [TestCase(WindowLocation.LeftHalf, WindowLocation.RightHalf,
            new TestAction[] { TestAction.Left, TestAction.Right })]

        [TestCase(WindowLocation.LeftTwoThirds, WindowLocation.RightThird,
            new TestAction[] { TestAction.Right})]

        [TestCase(WindowLocation.LeftTwoThirds, WindowLocation.RightThird,
            new TestAction[] { TestAction.Right, TestAction.Right })]

        [TestCase(WindowLocation.LeftHalf, WindowLocation.RightHalf,
            new TestAction[] { TestAction.Right, TestAction.Left })]
        
        public void Should_End_Up_With_Correct_Proportions_After_Action(
            WindowLocation expectedLeftWindowLocation,
            WindowLocation expectedRightWindowLocation, 
            params TestAction[] actions)
        {
            //Arrange

            //Act
            foreach(var a in actions)
            {
                if (a == TestAction.Left)
                {
                    _subject.Left();
                    Console.WriteLine("Left");
                }
                else
                {
                    _subject.Right();
                    Console.WriteLine("Right");
                }
            }


            //Assert
            AssertInLocation(expectedLeftWindowLocation, _windowLeft, "Left window");
            AssertInLocation(expectedRightWindowLocation, _windowRight, "Right Window");
        }


        [Test]
        public void Should_Restore_Windows_On_Activate()
        {
            //Arrange

            //Act

            //Assert
        }
    }
}
