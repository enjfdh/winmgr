using System.Windows.Forms;

namespace WinMgr
{
    public class WindowsScreen : IScreen
    {
        public int Height
        {
            get
            {
                return Screen.PrimaryScreen.WorkingArea.Height;
            }
        }

        public int Width
        {
            get
            {
                return Screen.PrimaryScreen.WorkingArea.Width;
            }
        }
    }
}
