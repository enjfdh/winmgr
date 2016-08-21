namespace WinMgr.Specifications
{
    public interface IDesk
    {
        IWorkArea WorkArea1 { get; }
        IWorkArea WorkArea2 { get; }
        IWorkArea WorkArea3 { get; }

        void SetWorkMode();
        void SetOrganiseMode();
    }
}