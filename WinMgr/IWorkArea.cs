namespace WinMgr
{
    public interface IWorkArea
    {
        IWindow LeftWindow { get; }
        IWindow RightWindow { get; }
    }
}
