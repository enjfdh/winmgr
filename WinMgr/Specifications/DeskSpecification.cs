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
        [TestCase(Action.WorkArea1Activate)]
        [TestCase(Action.WorkArea2Activate)]
        [TestCase(Action.WorkArea3Activate)]
        public void Should_Add_Left_Window_To_Active_Work_Area(Action activateAction)
        {
            //Arrange

            //Act
            Send(activateAction);
            Send(Action.Left);

            //Assert
            if (activateAction == Action.WorkArea1Activate) _workArea1.Verify(x => x.SetLeftWindow(_window));
            else if (activateAction == Action.WorkArea2Activate) _workArea2.Verify(x => x.SetLeftWindow(_window));
            else if (activateAction == Action.WorkArea3Activate) _workArea3.Verify(x => x.SetLeftWindow(_window));
        }

        [Test]
        [TestCase(Action.WorkArea1Activate)]
        [TestCase(Action.WorkArea2Activate)]
        [TestCase(Action.WorkArea3Activate)]
        public void Should_Add_Right_Window_To_Active_Work_Area(Action activateAction)
        {
            //Arrange

            //Act
            Send(activateAction);
            Send(Action.Right);

            //Assert
            if (activateAction == Action.WorkArea1Activate) _workArea1.Verify(x => x.SetRightWindow(_window));
            else if (activateAction == Action.WorkArea2Activate) _workArea2.Verify(x => x.SetRightWindow(_window));
            else if (activateAction == Action.WorkArea3Activate) _workArea3.Verify(x => x.SetRightWindow(_window));
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
        [TestCase(Action.WorkArea1Activate)]
        [TestCase(Action.WorkArea2Activate)]
        [TestCase(Action.WorkArea3Activate)]
        public void Should_Act_Left_On_Active_WorkArea(Action activateAction)
        {
            //Arrange

            //Act
            Send(activateAction);
            Send(Action.Left);

            //Assert
            if (activateAction == Action.WorkArea1Activate) _workArea1.Verify(x => x.Left());
            else if (activateAction == Action.WorkArea2Activate) _workArea2.Verify(x => x.Left());
            else if (activateAction == Action.WorkArea3Activate) _workArea3.Verify(x => x.Left());
        }

        [Test]
        [TestCase(Action.WorkArea1Activate)]
        [TestCase(Action.WorkArea2Activate)]
        [TestCase(Action.WorkArea3Activate)]
        public void Should_Act_Right_On_Active_WorkArea(Action activateAction)
        {
            //Arrange

            //Act
            Send(activateAction);
            Send(Action.Right);

            //Assert
            if (activateAction == Action.WorkArea1Activate) _workArea1.Verify(x => x.Right());
            else if (activateAction == Action.WorkArea2Activate) _workArea2.Verify(x => x.Right());
            else if (activateAction == Action.WorkArea3Activate) _workArea3.Verify(x => x.Right());
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

        [Test]
        [TestCase(Action.WorkArea1Activate, 2,  Action.Down)]
        [TestCase(Action.WorkArea2Activate, 3,  Action.Down)]
        [TestCase(Action.WorkArea3Activate, 1,  Action.Down)]
        [TestCase(Action.WorkArea1Activate, 3, Action.Up)]
        [TestCase(Action.WorkArea2Activate, 1, Action.Up)]
        [TestCase(Action.WorkArea3Activate, 2, Action.Up)]

        [TestCase(Action.WorkArea1Activate, 1, Action.Down, Action.Up)]
        public void Should_Move_Between_WorkAreas_With_Up_Down(Action activateAction, int expectedActivatedWorkArea, params Action[] moveActions)
        {
            //Arrange

            //Act
            Send(activateAction);
            foreach(var a in moveActions) Send(a);

            //Assert
            if (expectedActivatedWorkArea == 1) _workArea1.Verify(x => x.Activate());
            else if (expectedActivatedWorkArea == 2) _workArea2.Verify(x => x.Activate());
            else if (expectedActivatedWorkArea == 3) _workArea3.Verify(x => x.Activate());
        }
    }
}

