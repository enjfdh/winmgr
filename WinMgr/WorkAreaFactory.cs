using System;

namespace WinMgr
{
    public class WorkAreaFactory : IWorkAreaFactory
    {
        public IWorkAreaOrganiser CreateWorkArea1()
        {
            return CreateWorkArea();
        }

        public IWorkAreaOrganiser CreateWorkArea2()
        {
            return CreateWorkArea();
        }

        public IWorkAreaOrganiser CreateWorkArea3()
        {
            return CreateWorkArea();
        }

        private IWorkAreaOrganiser CreateWorkArea()
        {
            return new WorkArea(new WindowController(), new WindowsScreen());
        }
    }
}
