using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa.TimeEvents
{
    class Consumption : Event
    {
        private int _eventTime;
        private string _type;

        public Consumption(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant)
        {
            SortedByEatingTime(restaurant.ConsumptionClients);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### RESTAURANT CLIENT FINISHED HIS MEAL AND GO TO THE CASH ###");
            Console.ResetColor();
            Console.WriteLine("Klient typu {0} o ID: {1} wybiera sie w kierunku kasy", restaurant.ConsumptionClients[0].Type, restaurant.ConsumptionClients[0].ClientId);
            switch (restaurant.ConsumptionClients[0].ReceivedTable)
            {
                case 4:
                    restaurant.FourSeatsTables++;
                    break;
                case 3:
                    restaurant.ThreeSeatsTables++;
                    break;
                case 2:
                    restaurant.TwoSeatsTables++;
                    break;
            }
            Client tempClient = restaurant.ConsumptionClients[0];
            restaurant.CashQ.Enqueue(tempClient);
            restaurant.ConsumptionClients.RemoveAt(0);
        }

    }
}
