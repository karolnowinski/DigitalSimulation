using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SymulacjaCyfrowa.TimeEvents;

namespace SymulacjaCyfrowa
{
    public class Restaurant
    {
        private static uint _fourSeatsTables;   //okresla ilosc stolikow 4os
        private static uint _threeSeatsTables;  //okresla ilosc stolikow 3os
        private static uint _twoSeatsTables;    //okresla ilosc stolikow 2os
        private static bool _managerAvailability; //okresla dostepnosc managera
        private static uint _buffetStands;
        private static uint _waiters;
        private static uint _cashiers;
        public const int ServiceTime = 520; //czas prowadzenia przez kierownika sali 35
        public static readonly int[] Seeds = new int[15];
        public static readonly Random Rnd = new Random();
        public static int ClientsCounter { get; set; } = 0;

        public Queue<Client> BuffetQ = new Queue<Client>();    //inicjalizacja kolejki klientów typu Queue<Client>
        public Queue<Client> TableQ = new Queue<Client>();     //inicjalizacja kolejki klientów typu Queue<Client>
        public Queue<Client> CashQ = new Queue<Client>();      //inicjalizacja kolejki klientów typu Queue<Client>
        public List<Client> BuffetClients = new List<Client>(); //inicjalizacja listy klientów bufetu

        public Queue<Client> DrinksQ = new Queue<Client>();
        public List<Client> DrinksClients = new List<Client>();
        public Queue<Client> MealsQ = new Queue<Client>();
        public List<Client> MealsClients = new List<Client>();
        public List<Client> ConsumptionClients = new List<Client>();

    public Restaurant(uint fourSeatsTables, uint threeSeatsTables, uint twoSeatsTables, uint buffetStands, uint waiters, uint cashiers)
        {
            _fourSeatsTables = fourSeatsTables;
            _threeSeatsTables = threeSeatsTables;
            _twoSeatsTables = twoSeatsTables;
            _managerAvailability = true;
            _buffetStands = buffetStands;
            _waiters = waiters;
            _cashiers = cashiers;
        }


        public static List<Event> Begin(List<Event> scheduler, Restaurant restaurant)
        {
            scheduler.Add(new ClientArrival(Event.SystemTime, "RC"));
            Event.SystemTime = 0;                                                   //initialize System Time

            int seed = 398; //1234
            int x = 0;

            for (int i = 0; i < 10 * 10000; i++)
            {
                Generators.Uniform(ref seed);
                if ((i % 10000) == 0) Seeds[x++] = seed;
            }

            return scheduler;
        }

        public static void ShowInfo(List<Event> scheduler, Restaurant restaurant)
        {
            Console.WriteLine(" ");
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("STATYSTYKI:");
            Console.ResetColor();
            Console.WriteLine("Liczba wydarzeń: " + scheduler.Count);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("BUFET:");
            Console.ResetColor();
            Console.WriteLine("Długość kolejki do bufetu: " + restaurant.BuffetQ.Count);
            Console.WriteLine("Liczba grup klientów w Bufecie: " + restaurant.BuffetClients.Count);
            Console.WriteLine("Liczba dostępnych stanowisk w bufecie: " + restaurant.BuffetStands);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("RESTAURANT:");
            Console.ResetColor();
            Console.WriteLine("Długość kolejki do stolików: " + restaurant.TableQ.Count);
            Console.WriteLine("Manager jest dostepny? " + restaurant.ManagerAvailability);
            Console.WriteLine("Liczba dostępnych kelnerów: " + restaurant.Waiters);
            Console.WriteLine("Liczba dostepnych stolikow: 2os.: " + restaurant.TwoSeatsTables + " | 3os.: " + restaurant.ThreeSeatsTables + " | 4os.: " + restaurant.FourSeatsTables);
            Console.WriteLine("Liczba klientów oczekujących na przybycie kelnera wydajacego napoj: " + restaurant.DrinksQ.Count);
            Console.WriteLine("Liczba klientów oczekujących na napoj: " + restaurant.DrinksClients.Count);
            Console.WriteLine("Liczba klientów oczekujących na przybycie kelnera wydajacego posilek: " + restaurant.MealsQ.Count);
            Console.WriteLine("Liczba klientów oczekujących na posilek: " + restaurant.MealsClients.Count);
            Console.WriteLine("Liczba klientów spozywajacych pelny posilek: " + restaurant.ConsumptionClients.Count);
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("CASH:");
            Console.ResetColor();
            Console.WriteLine("Długość kolejki do kasy: " + restaurant.CashQ.Count);
            Console.WriteLine("Liczba dostępnych kasjerów: " + restaurant.Cashiers);
            Console.WriteLine("Liczba obsluzonych klientów: " + Restaurant.ClientsCounter);
            Console.WriteLine();
            Console.WriteLine(" ");
        }

        public uint FourSeatsTables
        {
            get => _fourSeatsTables;
            set => _fourSeatsTables = value;
        }

        public uint ThreeSeatsTables
        {
            get => _threeSeatsTables;
            set => _threeSeatsTables = value;
        }

        public uint TwoSeatsTables
        {
            get => _twoSeatsTables;
            set => _twoSeatsTables = value;
        }

        public uint BuffetStands
        {
            get => _buffetStands;
            set => _buffetStands = value;
        }

        public uint Waiters
        {
            get => _waiters;
            set => _waiters = value;
        }

        public uint Cashiers
        {
            get => _cashiers;
            set => _cashiers = value;
        }

        public bool ManagerAvailability
        {
            get => _managerAvailability;
            set => _managerAvailability = value;
        }

        public bool IsTableAvailable(int amount)
        {
            bool flag = false;
            switch (amount)
            {
                case 4:
                    if (FourSeatsTables > 0) flag = true;
                    break;
                case 3:
                    if (FourSeatsTables > 0 || ThreeSeatsTables > 0) flag = true;
                    break;
                case 2:
                case 1:
                    if (FourSeatsTables > 0 || ThreeSeatsTables > 0 || TwoSeatsTables > 0) flag = true;
                    break;
            }
            return flag;
        }
    }
}
