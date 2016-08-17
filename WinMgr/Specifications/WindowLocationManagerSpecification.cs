using Moq;
using NUnit.Framework;

namespace WinMgr.Specifications
{
    [TestFixture]
    public class WindowLocationManagerSpecification
    {
        private Mock<IWindowLocator> _locator;
        private Mock<IWindowController> _controller;
        private IWindowLocationManager _subject;
        private IWindow _currentWindow;

        [SetUp]
        public void SetUp()
        {
            _locator = new Mock<IWindowLocator>();
            _controller = new Mock<IWindowController>();
            _currentWindow = Mock.Of<IWindow>();
            _locator.Setup(x => x.GetCurrentWindow()).Returns(_currentWindow);
            _subject = new WindowLocationManager(_locator.Object, _controller.Object);
        }
      

        [Test]
        public void Should_Maximise_Current_Window()
        {
            //Arrange
            
            //Act
            _subject.Maximise();

            //Assert
            _controller.Verify(x => x.Maximise(_currentWindow));
        }

        [Test]
        public void Should_Move_Current_Window_To_Left_Side()
        {
            //Arrange
            var window = Mock.Of<IWindow>();
            _locator.Setup(x => x.GetCurrentWindow()).Returns(window);
            
            //Act
            _subject.MoveLeft();

            //Assert
            _controller.Verify(x => x.MoveLeft(window));
        }
    }
}
