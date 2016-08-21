using System;
using System.Reactive.Subjects;
using System.Windows.Forms;

namespace WinMgr
{
    public class HotKeyActionSource : IActionSource
    {
        private Subject<Action> _actions = new Subject<Action>();

        public void RegisterHotKeys()
        {
            HotKeyManager.RegisterHotKey(Keys.Left, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.Right, KeyModifiers.Alt);
            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
        }

        private void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            switch(e.Key)
            {
                case Keys.Left:
                    _actions.OnNext(Action.WorkArea2Left);
                    break;
                case Keys.Right:
                    _actions.OnNext(Action.WorkArea2Right);
                    break;
            }
        }

        public IObservable<Action> Actions
        {
            get
            {
                return _actions;
            }
        }
    }
}
