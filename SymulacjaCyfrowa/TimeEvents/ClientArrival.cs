using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SymulacjaCyfrowa.TimeEvents
{
    class ClientArrival : Event
    {
        private int _eventTime;
        private readonly string _type;
        private static int _idCounter = 0;

        public ClientArrival(int eventTime, string type) : base(eventTime, type)
        {
            _eventTime = eventTime;
            _type = Type;
        }

        public override void Execute(List<Event> scheduler, Restaurant restaurant)
        {
            Statistics.ActualClients++;
            Statistics.ActualClientsAmount.Add(Statistics.ActualClients);
            var time = Generators.Normal(ref Restaurant.Seeds[0], ref Restaurant.Seeds[1], Program.MiA, Program.SigmaA);
            string tempType;
            if (_type == "RC")
            {
                tempType = "BC";
            }
            else
            {
                tempType = "RC";
            }
            _idCounter++;
            Client c = new Client(_idCounter, tempType);
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("### NEW CLIENT! ###");
            Console.ResetColor();
            Console.WriteLine("Przychodzi " + c.Amount + "os. grupa klientów o ID: " + c.ClientId + ". [" + c.Type + "]");
            if (c.Type == "RC")
            {
                c.WaitingForTableTime = SystemTime; //start counting
                restaurant.TableQ.Enqueue(c);
                restaurant.TableQ.Peek().WaitingForTableTime = Event.SystemTime; //start counting
                Console.WriteLine("Dlugosc kolejki do Restauracji to: " + restaurant.TableQ.Count);
            }
            else
            {
                restaurant.BuffetQ.Enqueue(c);
                Console.WriteLine("Dlugosc kolejki do Bufetu to: " + restaurant.BuffetQ.Count);
            }
            scheduler.Add(new ClientArrival((int)(SystemTime + time), tempType));
            Sorted(scheduler);
        }
    }
}
