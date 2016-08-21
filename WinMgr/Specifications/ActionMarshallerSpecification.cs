using Moq;
using NUnit.Framework;
using System.Reactive.Subjects;

namespace WinMgr.Specifications
{
    [TestFixture]
    public class ActionMarshallerSpecification
    {
        private Mock<IWindowLocationManager> _manager;
        private Mock<IActionSource> _actionSource;
        private Subject<Action> _actions;
        private IActionMarshaller _subject;

        [SetUp]
        public void SetUp()
        {
            _manager = new Mock<IWindowLocationManager>();
            _actionSource = new Mock<IActionSource>();
            _actions = new Subject<Action>();
            _actionSource.SetupGet(x => x.Actions).Returns(_actions);

            IActionMarshaller subject = new ActionMarshaller(_manager.Object, _actionSource.Object);
        }
        

        [Test]
        public void Should_Marhall_Move_Left()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea2Left);

            //Assert
            _manager.Verify(x => x.MoveLeft());
        }

        [Test]
        public void Should_Marhall_Move_Right()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea2Right);

            //Assert
            _manager.Verify(x => x.MoveRight());
        }
    }
}
