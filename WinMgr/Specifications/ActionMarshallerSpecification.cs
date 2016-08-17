using Moq;
using NUnit.Framework;
using System.Reactive.Subjects;

namespace WinMgr.Specifications
{
    [TestFixture]
    public class ActionMarshallerSpecification
    {
        [Test]
        public void Should_Marshall_Maximise_Action()
        {
            //Arrange
            var manager = new Mock<IWindowLocationManager>();
            var actionSource = new Mock<IActionSource>();
            Subject<Action> actions = new Subject<Action>();
            actionSource.SetupGet(x => x.Actions).Returns(actions);
            IActionMarshaller subject = new ActionMarshaller(manager.Object, actionSource.Object);
            
            //Act
            actions.OnNext(Action.Maximise);

            //Assert
            manager.Verify(x => x.Maximise());
        }
    }
}
