using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa.TimeEvents
{
    class MealServing : Event
    {
        private int _eventTime;
        private string _type;

        public MealServing(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant)
        {
            var time = Generators.Exponential(Program.LambdaF, ref Restaurant.Seeds[0]);
            SortedByEatingTime(restaurant.MealsClients);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### CLIENT GET HIS MEAL ###");
            Console.ResetColor();
            Console.WriteLine("Klient o ID: {0} otrzymal swoj posilek", restaurant.MealsClients[0].ClientId);
            restaurant.Waiters++;
            restaurant.ConsumptionClients.Add(restaurant.MealsClients[0]);
            restaurant.MealsClients.RemoveAt(0);
            scheduler.Add(new Consumption((int)(SystemTime + time), "CT"));
            Sorted(scheduler);
        }
    }
}
