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
            HotKeyManager.RegisterHotKey(Keys.Home, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.Up, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.PageUp, KeyModifiers.Alt);

            HotKeyManager.RegisterHotKey(Keys.Left, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.Right, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.Clear, KeyModifiers.Alt);

            HotKeyManager.RegisterHotKey(Keys.End, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.PageDown, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.Down, KeyModifiers.Alt);

            HotKeyManager.RegisterHotKey(Keys.Insert, KeyModifiers.Alt);

            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
        }

        private void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            switch(e.Key)
            {
                case Keys.Home:
                    _actions.OnNext(Action.WorkArea1Left);
                    break;
                case Keys.PageUp:
                    _actions.OnNext(Action.WorkArea1Right);
                    break;
                case Keys.Up:
                    _actions.OnNext(Action.WorkArea1Activate);
                    break;

                case Keys.Left:
                    _actions.OnNext(Action.WorkArea2Left);
                    break;
                case Keys.Right:
                    _actions.OnNext(Action.WorkArea2Right);
                    break;
                case Keys.Clear:
                    _actions.OnNext(Action.WorkArea2Activate);
                    break;

                case Keys.End:
                    _actions.OnNext(Action.WorkArea3Left);
                    break;
                case Keys.PageDown:
                    _actions.OnNext(Action.WorkArea3Right);
                    break;
                case Keys.Down:
                    _actions.OnNext(Action.WorkArea3Activate);
                    break;

                case Keys.Insert:
                    _actions.OnNext(Action.ActivateConsole);
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
