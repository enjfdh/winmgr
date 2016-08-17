using System;
using System.Reactive.Subjects;

namespace WinMgr.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            string command;
            var source = new ConsoleActionSource();
            var manager = new WindowLocationManager(new WindowLocator(), new WindowController());
            var marshaller = new ActionMarshaller(manager, new ConsoleActionSource());

            while((command = System.Console.ReadLine()) != "quit")
            {
                switch (command)
                {
                    case "maximise":
                        source.Maximise();
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
        }
    }
}
