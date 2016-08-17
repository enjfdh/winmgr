using System;

namespace WinMgr
{
    public class ActionMarshaller : IActionMarshaller
    {
        private IWindowLocationManager _manager;
        private IActionSource _actionSource;
        

        public ActionMarshaller(IWindowLocationManager manager, IActionSource actionSource)
        {
            _manager = manager;
            _actionSource = actionSource;
            _actionSource.Actions.Subscribe(HandleAction);
        }

        private void HandleAction(Action action)
        {
            _manager.Maximise();
        }
    }
}