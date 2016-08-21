namespace WinMgr.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var actionSource = new HotKeyActionSource();
            actionSource.RegisterHotKeys();

            IDesk desk = new Desk(actionSource, new WindowLocator(), new WorkAreaFactory());
            
            string command;
            while ((command = System.Console.ReadLine()) != "quit")
            {
                switch(command)
                {
                    case "organise":
                        desk.SetOrganiseMode();
                        break;
                    case "work":
                        desk.SetWorkMode();
                        break;
                }
            }
        }
        
    }
}
