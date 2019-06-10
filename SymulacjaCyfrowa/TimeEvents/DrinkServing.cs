using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa.TimeEvents
{
    class DrinkServing : Event
    {
        private int _eventTime;
        private string _type;

        public DrinkServing(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant)
        {
            {
                SortedByEatingTime(restaurant.DrinksClients);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("### CLIENT GET HIS DRINK ###");
                Console.ResetColor();
                Console.WriteLine("Klient o ID: {0} otrzymal swoj napoj", restaurant.DrinksClients[0].ClientId);
                restaurant.Waiters++;
                restaurant.MealsQ.Enqueue(restaurant.DrinksClients[0]);
                restaurant.DrinksClients.RemoveAt(0);
            }
        }
    }
}
