using System;
using System.Collections.Generic;
using SymulacjaCyfrowa.TimeEvents;

namespace SymulacjaCyfrowa
{
    public static class Program
    {
        public const int MiA = 280;
        public const int SigmaA = 45;
        public const int MiB = 3450;
        public const int SigmaB = 90;
        public const int LambdaN = 1200;
        public const int LambdaJ = 1900;
        public const int LambdaF = 2400;
        public const int LambdaP = 1200;

        static void Main()
        {
            Restaurant restaurant = new Restaurant(6, 12, 5, 18, 7, 5);
            List<Event> scheduler = new List<Event>(); //creates scheduler which is list of Events
            Restaurant.Begin(scheduler, restaurant);

            foreach (var t in Restaurant.Seeds)
            {
                Console.WriteLine(t);
            }

            Console.WriteLine("\nWybierz tryb pracy programu: ");
            Console.WriteLine("1 - ciagly | 2 - krokowy | 3 - wyjscie z programu");
            int choice = Convert.ToInt32(Console.ReadLine());
            bool cleared = false;

            switch (choice)
            {
                case 1:
                    while (Restaurant.ClientsCounter < 50000) //tryb pracy ciaglej
                    {
                        Console.BackgroundColor = ConsoleColor.Blue; //kolorowanie wystapienia w czasie
                        Console.Write(" {0} ", Event.SystemTime);
                        Console.ResetColor();
                        scheduler[0].Execute(scheduler, restaurant);
                        scheduler.RemoveAt(0);

                        bool timeEventTrig = true;
                        while (timeEventTrig)
                        {
                            timeEventTrig = false;
                            if ((restaurant.TableQ.Count > 0) && (restaurant.ManagerAvailability) &&
                                (restaurant.IsTableAvailable((restaurant.TableQ.Peek().Amount))))
                            {
                                scheduler = ConditionalEvents.GetTable(scheduler, restaurant);
                                timeEventTrig = true;
                            }

                            if (restaurant.BuffetQ.Count > 0 &&
                                restaurant.BuffetStands >= restaurant.BuffetQ.Peek().Amount)
                            {
                                scheduler = ConditionalEvents.GetBuffetStand(scheduler, restaurant);
                                timeEventTrig = true;
                            }

                            if (restaurant.CashQ.Count > 0 && restaurant.Cashiers > 0)
                            {
                                scheduler = ConditionalEvents.GetCashier(scheduler, restaurant);
                                timeEventTrig = true;
                            }

                            if (restaurant.MealsQ.Count > 0 && restaurant.Waiters > 0)
                            {
                                scheduler = ConditionalEvents.GetMeal(scheduler, restaurant);
                                timeEventTrig = true;
                            }
                            else if (restaurant.DrinksQ.Count > 0 && restaurant.Waiters > 0)
                            {
                                restaurant.DrinksQ.Peek().WaitingForServiceTime = Event.SystemTime - restaurant.DrinksQ.Peek().WaitingForServiceTime; //end counting
                                Statistics.WaitingForServiceTime.Add(restaurant.DrinksQ.Peek().WaitingForServiceTime);
                                scheduler = ConditionalEvents.GetDrink(scheduler, restaurant);
                                timeEventTrig = true;
                            }
                        }

                        //Statistics.ShowStatistics(Restaurant.ClientsCounter);

                        Statistics.CashQueueLength.Add(restaurant.CashQ.Count);
                        Statistics.TableQueueLength.Add(restaurant.TableQ.Count);
                        Statistics.MeasSystemTime.Add(Event.SystemTime);

                        if (Restaurant.ClientsCounter > 11000 && cleared == false) //zerowanie fazy poczatkowej
                        {
                            Restaurant.ClientsCounter = 0;
                            cleared = true;
                            Statistics.WaitingForTableTime.Clear();
                            Statistics.TableQueueLength.Clear();
                            Statistics.WaitingForServiceTime.Clear();
                            Statistics.CashQueueLength.Clear();
                            Statistics.MeasSystemTime.Clear();
                            Statistics.ActualClientsAmount.Clear();
                        }
                        Event.SystemTime = scheduler[0].EventTime;
                    }
                    break;
                case 2: //tryb pracy krokowej
                    while (Restaurant.ClientsCounter < 50000) //tryb pracy ciaglej
                    {
                        Console.BackgroundColor = ConsoleColor.Blue; //kolorowanie wystapienia w czasie
                        Console.Write(" {0} ", Event.SystemTime);
                        Console.ResetColor();
                        scheduler[0].Execute(scheduler, restaurant);
                        scheduler.RemoveAt(0);
                        Console.ReadKey();

                        bool timeEventTrig = true;
                        while (timeEventTrig)
                        {
                            timeEventTrig = false;
                            if ((restaurant.TableQ.Count > 0) && (restaurant.ManagerAvailability) &&
                                (restaurant.IsTableAvailable((restaurant.TableQ.Peek().Amount))))
                            {
                                scheduler = ConditionalEvents.GetTable(scheduler, restaurant);
                                timeEventTrig = true;
                                Console.ReadKey();
                            }

                            if (restaurant.BuffetQ.Count > 0 &&
                                restaurant.BuffetStands >= restaurant.BuffetQ.Peek().Amount)
                            {
                                scheduler = ConditionalEvents.GetBuffetStand(scheduler, restaurant);
                                timeEventTrig = true;
                                Console.ReadKey();
                            }

                            if (restaurant.CashQ.Count > 0 && restaurant.Cashiers > 0)
                            {
                                scheduler = ConditionalEvents.GetCashier(scheduler, restaurant);
                                timeEventTrig = true;
                                Console.ReadKey();
                            }

                            if (restaurant.MealsQ.Count > 0 && restaurant.Waiters > 0)
                            {
                                scheduler = ConditionalEvents.GetMeal(scheduler, restaurant);
                                timeEventTrig = true;
                                Console.ReadKey();
                            }
                            else if (restaurant.DrinksQ.Count > 0 && restaurant.Waiters > 0)
                            {
                                restaurant.DrinksQ.Peek().WaitingForServiceTime = Event.SystemTime - restaurant.DrinksQ.Peek().WaitingForServiceTime; //end counting
                                Statistics.WaitingForServiceTime.Add(restaurant.DrinksQ.Peek().WaitingForServiceTime);
                                scheduler = ConditionalEvents.GetDrink(scheduler, restaurant);
                                timeEventTrig = true;
                                Console.ReadKey();
                            }
                        }

                        //Statistics.ShowStatistics(Restaurant.ClientsCounter);

                        Statistics.CashQueueLength.Add(restaurant.CashQ.Count);
                        Statistics.TableQueueLength.Add(restaurant.TableQ.Count);
                        Statistics.MeasSystemTime.Add(Event.SystemTime);

                        if (Restaurant.ClientsCounter > 11000 && cleared == false) //zerowanie fazy poczatkowej
                        {
                            Restaurant.ClientsCounter = 0;
                            cleared = true;
                            Statistics.WaitingForTableTime.Clear();
                            Statistics.TableQueueLength.Clear();
                            Statistics.WaitingForServiceTime.Clear();
                            Statistics.CashQueueLength.Clear();
                            Statistics.MeasSystemTime.Clear();
                            Statistics.ActualClientsAmount.Clear();
                        }
                        Event.SystemTime = scheduler[0].EventTime;
                    }
                    break;
                case 3:
                    break;
                default:
                    {
                        Console.WriteLine("Podales nieznany numer.");
                        break;
                    }
            }
            Restaurant.ShowInfo(scheduler, restaurant);
            Statistics.SaveStatistics();
            Console.WriteLine("KONIEC DZIAŁANIA PROGRAMU");
            Console.ReadKey();
        }
    }
}

