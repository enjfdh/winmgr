using Moq;
using NUnit.Framework;
using System;
using System.Reactive.Subjects;

namespace WinMgr.Specifications.DeskSpecs
{
    public abstract class DeskSpecification
    {
        protected Mock<IActionSource> _actionSource;
        protected Subject<Action> _actions;
        protected Mock<IWindowLocator> _windowLocator;
        protected Mock<IWorkAreaFactory> _workAreaFactory;
        protected Mock<IWorkAreaOrganiser> _workArea1;
        protected Mock<IWorkAreaOrganiser> _workArea2;
        protected Mock<IWorkAreaOrganiser> _workArea3;
        protected WindowStub _window;
        protected IDesk _subject;
        
        public virtual void SetUp()
        {
            _actionSource = new Mock<IActionSource>();
            _actions = new Subject<Action>();
            _actionSource.SetupGet(x => x.Actions).Returns(_actions);

            _windowLocator = new Mock<IWindowLocator>();
            _window = new WindowStub(new IntPtr(0));
            _windowLocator.Setup(x => x.GetCurrentWindow()).Returns(_window);

            _workArea1 = new Mock<IWorkAreaOrganiser>();
            _workArea2 = new Mock<IWorkAreaOrganiser>();
            _workArea3 = new Mock<IWorkAreaOrganiser>();

            _workAreaFactory = new Mock<IWorkAreaFactory>();
            _workAreaFactory.Setup(x => x.CreateWorkArea1()).Returns(_workArea1.Object);
            _workAreaFactory.Setup(x => x.CreateWorkArea2()).Returns(_workArea2.Object);
            _workAreaFactory.Setup(x => x.CreateWorkArea3()).Returns(_workArea3.Object);

            _subject = new Desk(_actionSource.Object, _windowLocator.Object, _workAreaFactory.Object);
        }
        
        protected void Send(Action action)
        {
            _actions.OnNext(action);
        }
    }

    [TestFixture]
    public class When_Organising_Desk : DeskSpecification
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _subject.SetOrganiseMode();
        }

        [Test]
        public void Should_Add_Left_Window_To_WorkArea1()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea1Left);

            //Assert
            _workArea1.Verify(x => x.SetLeftWindow(_window));
        }

        [Test]
        public void Should_Add_Right_Window_To_WorkArea1()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea1Right);

            //Assert
            _workArea1.Verify(x => x.SetRightWindow(_window));
        }

        [Test]
        public void Should_Add_Left_Window_To_WorkArea2()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea2Left);

            //Assert
            _workArea2.Verify(x => x.SetLeftWindow(_window));
        }

        [Test]
        public void Should_Add_Right_Window_To_WorkArea2()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea2Right);

            //Assert
            _workArea2.Verify(x => x.SetRightWindow(_window));
        }

        [Test]
        public void Should_Add_Left_Window_To_WorkArea3()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea3Left);

            //Assert
            _workArea3.Verify(x => x.SetLeftWindow(_window));
        }

        [Test]
        public void Should_Add_Right_Window_To_WorkArea3()
        {
            //Arrange

            //Act
            _actions.OnNext(Action.WorkArea3Right);

            //Assert
            _workArea3.Verify(x => x.SetRightWindow(_window));
        }
    }

    [TestFixture]
    public class When_Working : DeskSpecification
    {
        [SetUp]
        public override void SetUp()
        {
            base.SetUp();
            _subject.SetWorkMode();
        }

        [Test]
        public void Should_Act_Left_On_WorkArea1()
        {
            //Arrange

            //Act
            Send(Action.WorkArea1Left);

            //Assert
            _workArea1.Verify(x => x.Left());
        }

        [Test]
        public void Should_Act_Right_On_WorkArea1()
        {
            //Arrange

            //Act
            Send(Action.WorkArea1Right);

            //Assert
            _workArea1.Verify(x => x.Right());
        }

        [Test]
        public void Should_Act_Left_On_WorkArea2()
        {
            //Arrange

            //Act
            Send(Action.WorkArea2Left);

            //Assert
            _workArea2.Verify(x => x.Left());
        }

        [Test]
        public void Should_Act_Right_On_WorkArea2()
        {
            //Arrange

            //Act
            Send(Action.WorkArea2Right);

            //Assert
            _workArea2.Verify(x => x.Right());
        }

        [Test]
        public void Should_Act_Left_On_WorkArea3()
        {
            //Arrange

            //Act
            Send(Action.WorkArea3Left);

            //Assert
            _workArea3.Verify(x => x.Left());
        }

        [Test]
        public void Should_Act_Right_On_WorkArea3()
        {
            //Arrange

            //Act
            Send(Action.WorkArea3Right);

            //Assert
            _workArea3.Verify(x => x.Right());
        }

        [Test]
        public void Should_Activate_WorkArea1_And_Deactivate_Others()
        {
            //Arrange

            //Act
            Send(Action.WorkArea1Activate);

            //Assert
            _workArea1.Verify(x => x.Activate());
            _workArea2.Verify(x => x.Deactivate());
            _workArea3.Verify(x => x.Deactivate());
        }

        [Test]
        public void Should_Activate_WorkArea2_And_Deactivate_Others()
        {
            //Arrange

            //Act
            Send(Action.WorkArea2Activate);

            //Assert
            _workArea2.Verify(x => x.Activate());
            _workArea1.Verify(x => x.Deactivate());
            _workArea3.Verify(x => x.Deactivate());
        }

        [Test]
        public void Should_Activate_WorkArea3_And_Deactivate_Others()
        {
            //Arrange

            //Act
            Send(Action.WorkArea3Activate);

            //Assert
            _workArea3.Verify(x => x.Activate());
            _workArea1.Verify(x => x.Deactivate());
            _workArea2.Verify(x => x.Deactivate());
        }
    }
}

