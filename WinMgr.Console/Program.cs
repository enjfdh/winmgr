using System;

namespace WinMgr.Console
{
    public class Program
    {
        private IWindowController _controller = new WindowController();
        private IWindowLocator _locator = new WindowLocator();
        private IWindow _thisWindow;
    
        static void Main(string[] args)
        {
            var program = new Program();
            program.Run();
        }

        public void Run()
        {
            ShowInstructions();

            var actionSource = new HotKeyActionSource();
            actionSource.RegisterHotKeys();
            actionSource.Actions.Subscribe(HandleAction);

            _thisWindow = _locator.GetCurrentWindow();

            IDesk desk = new Desk(actionSource, new WindowLocator(), new WorkAreaFactory());

            string command;
            System.Console.Write(">");
            while ((command = System.Console.ReadLine()) != "quit")
            {
                switch (command)
                {
                    case "organise":
                        System.Console.WriteLine("Organise mode. Use Alt + Numpad keys to set windows for work areas 1-3");
                        desk.SetOrganiseMode();
                        break;
                    case "work":
                        System.Console.WriteLine("Work mode. Use Alt + Numpad keys to set windows to activate work areas and emphasise windows");
                        desk.SetWorkMode();
                        break;
                }
                System.Console.Write(">");
                _controller.MinimiseWindow(_thisWindow.Pointer);
            }
        }
        
        private void HandleAction(Action action)
        {
            if (action != Action.ActivateConsole) return;

            _controller.ShowWindow(_thisWindow.Pointer);
        }
        
        private static void ShowInstructions()
        {
            System.Console.WriteLine("Organise mode :");
            System.Console.WriteLine("  Alt+NumPad7 : set workarea1 left window");
            System.Console.WriteLine("  Alt+NumPad9 : set workarea1 right window");
            System.Console.WriteLine("  Alt+NumPad4 : set workarea2 left window");
            System.Console.WriteLine("  Alt+NumPad6 : set workarea2 right window");
            System.Console.WriteLine("  Alt+NumPad1 : set workarea3 left window");
            System.Console.WriteLine("  Alt+NumPad3 : set workarea3 right window");
            System.Console.WriteLine();

            System.Console.WriteLine("Work mode :");
            System.Console.WriteLine("  Alt+NumPad8 : activate workarea1");
            System.Console.WriteLine("  Alt+NumPad7 : emphasise workarea1 right window");
            System.Console.WriteLine("  Alt+NumPad9 : emphasise workarea1 left window");
            System.Console.WriteLine("  Alt+NumPad5 : activate workarea2");
            System.Console.WriteLine("  Alt+NumPad4 : emphasise workarea2 right window");
            System.Console.WriteLine("  Alt+NumPad6 : emphasise workarea2 left window");
            System.Console.WriteLine("  Alt+NumPad2 : activate workarea3");
            System.Console.WriteLine("  Alt+NumPad1 : emphasise workarea3 right window");
            System.Console.WriteLine("  Alt+NumPad3 : emphasise workarea3 left window");
            System.Console.WriteLine();

            System.Console.WriteLine("Use commands organise and work, or quit to exit");
            System.Console.WriteLine();
        }
    }
}
