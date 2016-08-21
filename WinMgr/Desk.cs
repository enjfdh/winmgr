using System;

namespace WinMgr
{
    public class Desk : IDesk
    {
        private IActionSource _actionSource;
        private IDisposable _actionSubscription;
        private IWindowLocator _windowLocator;
        private Mode _mode = Mode.Organise;
        private IWorkAreaOrganiser _activeWorkArea;

        public Desk(IActionSource actionSource, IWindowLocator windowLocator, IWorkAreaFactory workAreaFactory)
        {
            _actionSource = actionSource;
            _windowLocator = windowLocator;

            _workArea1 = workAreaFactory.CreateWorkArea1();
            _workArea2 = workAreaFactory.CreateWorkArea2();
            _workArea3 = workAreaFactory.CreateWorkArea3();

            _activeWorkArea = _workArea1;

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
                case Action.WorkArea1Activate:
                    Activate(_workArea1);
                    break;
                case Action.WorkArea2Activate:
                    Activate(_workArea2);
                    break;
                case Action.WorkArea3Activate:
                    Activate(_workArea3);
                    break;
                case Action.Left:
                    _activeWorkArea.Left();
                    break;
                case Action.Right:
                    _activeWorkArea.Right();
                    break;
                case Action.Down:
                case Action.Up:
                    ActivateNext(action);
                    break;
            }
        }   

        private void ActivateNext(Action action)
        {
            if (action == Action.Down)
            {
                if (_activeWorkArea == _workArea1) Activate(_workArea2);
                else if (_activeWorkArea == _workArea2) Activate(_workArea3);
                else if (_activeWorkArea == _workArea3) Activate(_workArea1);
            }
            else if (action == Action.Up)
            {
                if (_activeWorkArea == _workArea1) Activate(_workArea3);
                else if (_activeWorkArea == _workArea2) Activate(_workArea1);
                else if (_activeWorkArea == _workArea3) Activate(_workArea2);
            }
        }

        private void Activate(IWorkAreaOrganiser workArea)
        {
            foreach (var w in new[] { _workArea1, _workArea2, _workArea3 })
            {
                if (w != workArea) w.Deactivate();
            }
            workArea.Activate();
            _activeWorkArea = workArea;
        }

        private void HandleOrganiseAction(Action action)
        { 
            switch (action)
            {
                case Action.WorkArea1Activate:
                    _activeWorkArea = _workArea1;
                    break;
                case Action.WorkArea2Activate:
                    _activeWorkArea = _workArea2;
                    break;
                case Action.WorkArea3Activate:
                    _activeWorkArea = _workArea3;
                    break;
                case Action.Left:
                    _activeWorkArea.SetLeftWindow(_windowLocator.GetCurrentWindow());
                    break;
                case Action.Right:
                    _activeWorkArea.SetRightWindow(_windowLocator.GetCurrentWindow());
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