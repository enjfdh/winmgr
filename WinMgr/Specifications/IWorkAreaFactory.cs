﻿namespace WinMgr.Specifications
{
    public interface IWorkAreaFactory
    {
        IWorkAreaOrganiser CreateWorkArea1();
        IWorkAreaOrganiser CreateWorkArea2();
        IWorkAreaOrganiser CreateWorkArea3();
    }
}