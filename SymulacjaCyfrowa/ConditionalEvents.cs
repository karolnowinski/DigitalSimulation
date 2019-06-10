using SymulacjaCyfrowa.TimeEvents;
using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa
{
    class ConditionalEvents : Event
    {
        public static List<Event> GetTable(List<Event> scheduler, Restaurant restaurant)
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### RESTAURANT CLIENTS GET TABLE! ###");
            Console.ResetColor();
            restaurant.TableQ.Peek().WaitingForTableTime = SystemTime - restaurant.TableQ.Peek().WaitingForTableTime; //stop counting
            Statistics.WaitingForTableTime.Add(restaurant.TableQ.Peek().WaitingForTableTime);
            restaurant.ManagerAvailability = false;
            scheduler.Add(new ManagerBusy(SystemTime + Restaurant.ServiceTime, "MB"));
            Console.WriteLine("Manager is not available until {0}.", (SystemTime + Restaurant.ServiceTime));
            if (restaurant.TableQ.Peek().Amount <= 2 && restaurant.TwoSeatsTables != 0)
            {
                restaurant.TwoSeatsTables--;
                Console.WriteLine("Manager przydzielil klientowi stolik 2osobowy.");
                Console.WriteLine("Liczba dostepnych stolikow: 2os.: " + restaurant.TwoSeatsTables + " | 3os.: " + restaurant.ThreeSeatsTables + " | 4os.: " + restaurant.FourSeatsTables);
                restaurant.TableQ.Peek().ReceivedTable = 2;
            }
            else if (restaurant.TableQ.Peek().Amount <= 3 && restaurant.ThreeSeatsTables != 0)
            {
                restaurant.ThreeSeatsTables--;
                Console.WriteLine("Manager przydzielil klientowi stolik 3osobowy.");
                Console.WriteLine("Liczba dostepnych stolikow: 2os.: " + restaurant.TwoSeatsTables + " | 3os.: " + restaurant.ThreeSeatsTables + " | 4os.: " + restaurant.FourSeatsTables);
                restaurant.TableQ.Peek().ReceivedTable = 3;
            }
            else if (restaurant.TableQ.Peek().Amount <= 4 && restaurant.FourSeatsTables != 0)
            {
                restaurant.FourSeatsTables--;
                Console.WriteLine("Manager przydzielil klientowi stolik 4osobowy.");
                Console.WriteLine("Liczba dostepnych stolikow: 2os.: " + restaurant.TwoSeatsTables + " | 3os.: " + restaurant.ThreeSeatsTables + " | 4os.: " + restaurant.FourSeatsTables);
                restaurant.TableQ.Peek().ReceivedTable = 4;
            }
            Sorted(scheduler);
            return scheduler;
        }

        public static List<Event> GetBuffetStand(List<Event> scheduler, Restaurant restaurant)
        {
            var time = Generators.Normal(ref Restaurant.Seeds[0], ref Restaurant.Seeds[1], Program.MiB, Program.SigmaB);
            switch (restaurant.BuffetQ.Peek().Amount)
            {
                case 4:
                    restaurant.BuffetStands -= 4;
                    break;
                case 3:
                    restaurant.BuffetStands -= 3;
                    break;
                case 2:
                    restaurant.BuffetStands -= 2;
                    break;
                case 1:
                    restaurant.BuffetStands--;
                    break;
            }
            restaurant.BuffetQ.Peek().EatingTime = (uint)(SystemTime + time);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### CLIENT GO TO THE BUFFET ###");
            Console.ResetColor();
            Console.WriteLine("Klient o ID {0} idzie spożyc posiłek w Bufecie.", restaurant.BuffetQ.Peek().ClientId);
            restaurant.BuffetClients.Add(restaurant.BuffetQ.Dequeue());
            Console.WriteLine("Dlugosc kolejki do Bufetu to: " + restaurant.BuffetQ.Count);
            scheduler.Add(new BuffetConsumption((int)(SystemTime + time), "BW"));
            Sorted(scheduler);
            return scheduler;
        }
        public static List<Event> GetCashier(List<Event> scheduler, Restaurant restaurant)
        {
            var time = Generators.Exponential(Program.LambdaP, ref Restaurant.Seeds[0]);
            restaurant.Cashiers--;
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### CLIENT GO TO THE CASH ###");
            Console.ResetColor();
            Console.WriteLine("Klient o ID {0} jest obsługiwany przez kasjera.", restaurant.CashQ.Peek().ClientId);
            Console.WriteLine("Czas konca jego obslugi planowany jest na {0}.", (int)(SystemTime + time));
            restaurant.CashQ.Dequeue();
            scheduler.Add(new CashierBusy((int)(SystemTime + time), "CW"));
            Sorted(scheduler);
            return scheduler;
        }

        public static List<Event> GetDrink(List<Event> scheduler, Restaurant restaurant)
        {
            var time = Generators.Exponential(Program.LambdaN, ref Restaurant.Seeds[0]);
            restaurant.Waiters--;
            restaurant.DrinksQ.Peek().EatingTime = (uint)(SystemTime + time);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### CLIENTS ARE WAITING FOR DRINKS ###");
            Console.ResetColor();
            Console.WriteLine("Klient o ID {0} jest obsługiwany przez kelnera.", restaurant.DrinksQ.Peek().ClientId);
            restaurant.DrinksClients.Add(restaurant.DrinksQ.Dequeue());
            scheduler.Add(new DrinkServing((int)(SystemTime + time), "DS"));
            Sorted(scheduler);
            return scheduler;
        }

        public static List<Event> GetMeal(List<Event> scheduler, Restaurant restaurant)
        {
            var time = Generators.Exponential(Program.LambdaJ, ref Restaurant.Seeds[1]);
            restaurant.Waiters--;
            restaurant.MealsQ.Peek().EatingTime = (uint)(SystemTime + time);
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### CLIENTS ARE WAITING FOR MEAL ###");
            Console.ResetColor();
            Console.WriteLine("Klient o ID {0} jest obsługiwany przez kelnera.", restaurant.MealsQ.Peek().ClientId);
            restaurant.MealsClients.Add(restaurant.MealsQ.Dequeue());
            scheduler.Add(new MealServing((int)(SystemTime + time), "MS"));
            Sorted(scheduler);
            return scheduler;
        }
    }
}

