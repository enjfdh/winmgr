using System;

namespace WinMgr
{
    public class Desk : IDesk
    {
        private IActionSource _actionSource;
        private IDisposable _actionSubscription;
        private IWindowLocator _windowLocator;
        private Mode _mode = Mode.Organise;

        public Desk(IActionSource actionSource, IWindowLocator windowLocator, IWorkAreaFactory workAreaFactory)
        {
            _actionSource = actionSource;
            _windowLocator = windowLocator;

            _workArea1 = workAreaFactory.CreateWorkArea1();
            _workArea2 = workAreaFactory.CreateWorkArea2();
            _workArea3 = workAreaFactory.CreateWorkArea3();

            _actionSubscription = _actionSource.Actions.Subscribe(HandleAction);
        }

        private readonly IWorkAreaOrganiser _workArea1;
        public IWorkArea WorkArea1 { get { return _workArea1; } }

        private readonly IWorkAreaOrganiser _workArea2;
        public IWorkArea WorkArea2 { get { return _workArea2; } }

        private readonly IWorkAreaOrganiser _workArea3;
        public IWorkArea WorkArea3 { get { return _workArea3; } }

        public void SetWorkMode()
        {
            _mode = Mode.Work;
        }

        public void SetOrganiseMode()
        {
            _mode = Mode.Organise;
        }

        private void HandleAction(Action action)
        {
            if (_mode == Mode.Organise) HandleOrganiseAction(action);
            else HandleWorkAction(action);
        }

        private void HandleWorkAction(Action action)
        {
            switch (action)
            {
                case Action.WorkArea1Left:
                    _workArea1.Left();
                    break;
                case Action.WorkArea1Right:
                    _workArea1.Right();
                    break;
                case Action.WorkArea1Activate:
                    _workArea2.Deactivate();
                    _workArea3.Deactivate();
                    _workArea1.Activate();
                    break;
                case Action.WorkArea2Left:
                    _workArea2.Left();
                    break;
                case Action.WorkArea2Right:
                    _workArea2.Right();
                    break;
                case Action.WorkArea2Activate:
                    _workArea1.Deactivate();
                    _workArea3.Deactivate();
                    _workArea2.Activate();
                    break;
                case Action.WorkArea3Left:
                    _workArea3.Left();
                    break;
                case Action.WorkArea3Right:
                    _workArea3.Right();
                    break;
                case Action.WorkArea3Activate:
                    _workArea1.Deactivate();
                    _workArea2.Deactivate();
                    _workArea3.Activate();
                    break;
            }
        }

        private void HandleOrganiseAction(Action action)
        { 
            switch (action)
            {
                case Action.WorkArea1Left:
                    _workArea1.SetLeftWindow(_windowLocator.GetCurrentWindow());
                    break;
                case Action.WorkArea1Right:
                    _workArea1.SetRightWindow(_windowLocator.GetCurrentWindow());
                    break;
                case Action.WorkArea2Left:
                    _workArea2.SetLeftWindow(_windowLocator.GetCurrentWindow());
                    break;
                case Action.WorkArea2Right:
                    _workArea2.SetRightWindow(_windowLocator.GetCurrentWindow());
                    break;
                case Action.WorkArea3Left:
                    _workArea3.SetLeftWindow(_windowLocator.GetCurrentWindow());
                    break;
                case Action.WorkArea3Right:
                    _workArea3.SetRightWindow(_windowLocator.GetCurrentWindow());
                    break;
            }
        }

        private enum Mode
        {
            Organise,
            Work
        }

    }
}