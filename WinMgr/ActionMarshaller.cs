using System;

namespace WinMgr
{
    public class ActionMarshaller : IActionMarshaller
    {
        private IWindowLocationManager _manager;
        private IActionSource _actionSource;
        private IDisposable _actionsSubscription; 

        public ActionMarshaller(IWindowLocationManager manager, IActionSource actionSource)
        {
            _manager = manager;
            _actionSource = actionSource;
            _actionsSubscription = _actionSource.Actions.Subscribe(HandleAction);
        }

        private void HandleAction(Action action)
        {
            switch (action)
            {
                case Action.Maximise:
                    _manager.Maximise();
                    break;
                case Action.Left:
                    _manager.MoveLeft();
                    break;
                case Action.Right:
                    _manager.MoveRight();
                    break;
            }
        }
    }
}