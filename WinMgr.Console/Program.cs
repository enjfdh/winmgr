namespace WinMgr.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var manager = new WindowLocationManager(new WindowLocator(), new WindowController(), 
                new WindowsScreen());
            var actionSource = new HotKeyActionSource();
            actionSource.RegisterHotKeys();
            var marshaller = new ActionMarshaller(manager, actionSource);
            

            string command;
            while ((command = System.Console.ReadLine()) != "quit")
            {
            }
        }
        
    }
}
