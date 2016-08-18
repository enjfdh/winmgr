using System.Windows.Forms;

namespace WinMgr
{
    public class WindowsScreen : IScreen
    {
        public int Height
        {
            get
            {
                return Screen.PrimaryScreen.Bounds.Height;
            }
        }

        public int Width
        {
            get
            {
                return Screen.PrimaryScreen.Bounds.Width;
            }
        }
    }
}
