using NUnit.Framework;
using System;

namespace WinMgr.Specifications
{
    public class WorkAreaSpecification
    {
    }

    [TestFixture]
    public class When_Recording_Windows
    {
        [Test]
        public void Should_Record_Left_Window()
        {
            //Arrange
            IWorkArea subject = new WorkArea();
            var window = new WindowStub(new IntPtr(0));

            //Act
            subject.SetLeftWindow(window);

            //Assert
            Assert.AreEqual(window, subject.LeftWindow);
        }
    }

    internal class WorkArea : IWorkArea
    {
        public IWindow LeftWindow
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public void SetLeftWindow(WindowStub window)
        {
            throw new NotImplementedException();
        }
    }

    internal interface IWorkArea
    {
        IWindow LeftWindow { get; set; }

        void SetLeftWindow(WindowStub window);
    }
}
