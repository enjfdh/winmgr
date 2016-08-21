namespace WinMgr
{
    public interface IWorkAreaOrganiser : IWorkArea
    {
        void SetLeftWindow(IWindow window);
        void SetRightWindow(IWindow window);
        void Left();
        void Right();
        void Activate();
        void Deactivate();
    }
}
