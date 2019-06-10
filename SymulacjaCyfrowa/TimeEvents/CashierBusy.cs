using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa.TimeEvents
{
    public class CashierBusy : Event
    {
        private int _eventTime;
        private string _type;

        public CashierBusy(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### CLIENT PAID THE BILL IN CASH AND SAY GOODBYE ;) ###");
            Console.ResetColor();
            Statistics.ActualClients--;
            restaurant.Cashiers++;
            Restaurant.ClientsCounter++;
        }
    }
}
