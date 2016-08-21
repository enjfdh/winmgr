using NUnit.Framework;
using System;

namespace WinMgr.Specifications
{
    public class WorkAreaSpecification
    {
    }

    [TestFixture]
    public class When_Setting_Windows
    {
        [Test]
        public void Should_Set_Left_Window()
        {
            //Arrange
            IWorkAreaOrganiser subject = new WorkArea();
            var window = new WindowStub(new IntPtr(0));

            //Act
            subject.SetLeftWindow(window);

            //Assert
            Assert.AreEqual(window, subject.LeftWindow);
        }

        [Test]
        public void Should_Set_Right_Window()
        {
            //Arrange
            IWorkAreaOrganiser subject = new WorkArea();
            var window = new WindowStub(new IntPtr(0));

            //Act
            subject.SetRightWindow(window);

            //Assert
            Assert.AreEqual(window, subject.RightWindow);
        }
    }

    

    
}
