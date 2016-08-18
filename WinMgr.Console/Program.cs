using System;
using System.Reactive.Subjects;

namespace WinMgr.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            var source = new ConsoleActionSource();
            var manager = new WindowLocationManager(new WindowLocator(), new WindowController(), 
                new WindowsScreen());
            var marshaller = new ActionMarshaller(manager, source);

            string command;
            while ((command = System.Console.ReadLine()) != "quit")
            {
                switch (command)
                {
                    case "maximise":
                        source.Maximise();
                        break;
                    case "lhalf":
                        source.LeftHalf();
                        break;
                }
            }
        }
        
        public class ConsoleActionSource : IActionSource
        {
            private Subject<Action> _actions = new Subject<Action>();

            public IObservable<Action> Actions
            {
                get
                {
                    return _actions;
                }
            }

            public void Maximise()
            {
                _actions.OnNext(Action.Maximise);
            }

            internal void LeftHalf()
            {
                _actions.OnNext(Action.Left);
            }
        }
    }
}
