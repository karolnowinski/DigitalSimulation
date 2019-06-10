using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa.TimeEvents
{
    class ManagerBusy : Event
    {
        private int _eventTime;
        private string _type;

        public ManagerBusy(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### MANAGER IS AVAILABLE NOW ###");
            Console.ResetColor();
            restaurant.TableQ.Peek().WaitingForServiceTime = SystemTime; // start counting
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(restaurant.TableQ.Peek().ClientId + " Start WaitingForServiceTime | " + restaurant.TableQ.Peek().WaitingForServiceTime);
            Console.WriteLine("System Time | " + SystemTime);
            Console.WriteLine("Event.SystemTime | " + Event.SystemTime);
            Console.ResetColor();
            restaurant.DrinksQ.Enqueue(restaurant.TableQ.Dequeue());
            restaurant.ManagerAvailability = true;
        }

    }
}
