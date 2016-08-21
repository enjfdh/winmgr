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

            HotKeyManager.RegisterHotKey(Keys.NumPad1, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.NumPad2, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.NumPad3, KeyModifiers.Alt);

            HotKeyManager.RegisterHotKey(Keys.Up, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.Down, KeyModifiers.Alt);
            HotKeyManager.RegisterHotKey(Keys.NumPad0, KeyModifiers.Alt);


            HotKeyManager.HotKeyPressed += HotKeyManager_HotKeyPressed;
        }

        private void HotKeyManager_HotKeyPressed(object sender, HotKeyEventArgs e)
        {
            switch(e.Key)
            {
                case Keys.Left:
                    _actions.OnNext(Action.Left);
                    break;
                case Keys.Right:
                    _actions.OnNext(Action.Right);
                    break;
                case Keys.Up:
                    _actions.OnNext(Action.Up);
                    break;
                case Keys.Down:
                    _actions.OnNext(Action.Down);
                    break;
                case Keys.NumPad1:
                    _actions.OnNext(Action.WorkArea1Activate);
                    break;
                case Keys.NumPad2:
                    _actions.OnNext(Action.WorkArea2Activate);
                    break;
                case Keys.NumPad3:
                    _actions.OnNext(Action.WorkArea3Activate);
                    break;
                case Keys.NumPad0:
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
