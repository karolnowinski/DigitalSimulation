using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa.TimeEvents
{
    public class BuffetConsumption : Event
    {
        private int _eventTime;
        private string _type;

        public BuffetConsumption(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant)
        {
            SortedByEatingTime(restaurant.BuffetClients);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### BUFFET CLIENT FINISHED HIS MEAL  ###");
            Console.ResetColor();
            switch (restaurant.BuffetClients[0].Amount)
            {
                case 4:
                    restaurant.BuffetStands += 4;
                    break;
                case 3:
                    restaurant.BuffetStands += 3;
                    break;
                case 2:
                    restaurant.BuffetStands += 2;
                    break;
                case 1:
                    restaurant.BuffetStands++;
                    break;
            }
            Client tempClient = restaurant.BuffetClients[0];
            restaurant.CashQ.Enqueue(restaurant.BuffetClients[0]);
            restaurant.BuffetClients.RemoveAt(0);
        }

    }
}


