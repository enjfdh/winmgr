using System;

namespace WinMgr
{
    public interface IActionSource
    {
        IObservable<Action> Actions { get; }
    }
}