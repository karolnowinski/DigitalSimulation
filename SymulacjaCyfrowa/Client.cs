using SymulacjaCyfrowa.TimeEvents;
using System;
using System.Collections.Generic;


namespace SymulacjaCyfrowa
{
    public class Client
    {
        private int _clientId;                  //numer ID klienta lub grupy klientow
        private string _type;                   //typ klienta lub grupy klientow (bufet/restauracja)
        private int _amountPeople;              //liczba ludzi w grupie klientow
        private uint _eatingTime;               //zmienna przechowujaca czas konca spozywania posilku
        private uint _receivedTable;            //zmienna przechowujaca informacje nt. otrzymanej wielkosci stolu (2-4)
        private int _waitingForServiceTime;
        private int _waitingForTableTime;
        private static readonly Random Rnd = new Random();

        public Client(int id, string type)
        {
            _clientId = id;
            _type = type;
            _eatingTime = 0;
            _receivedTable = 0;
            _waitingForServiceTime = 0;
            _waitingForTableTime = 0;



            double probability = Generators.Uniform(ref Restaurant.Seeds[0]);           // get random number from 0-1
            if (probability >= 0 && probability < 0.11) _amountPeople = 1;              // 0.11 - 1 person
            else if (probability >= 0.11 && probability < 0.44) _amountPeople = 2;      // 0.33 - 2 people
            else if (probability >= 0.44 && probability < 0.77) _amountPeople = 3;      // 0.33 - 3 people
            else _amountPeople = 4;                                                     // 0.23 - 4 people
        }

        public int ClientId
        {
            get => _clientId;
            set => _clientId = value;
        }

        public int Amount
        {
            get => _amountPeople;
            set => _amountPeople = value;
        }

        public string Type
        {
            get => _type;
            set => _type = value;
        }

        public uint EatingTime
        {
            get => _eatingTime;
            set => _eatingTime = value;
        }

        public uint ReceivedTable
        {
            get => _receivedTable;
            set => _receivedTable = value;
        }

        public int WaitingForServiceTime
        {
            get => _waitingForServiceTime;
            set => _waitingForServiceTime = value;
        }

        public int WaitingForTableTime
        {
            get => _waitingForTableTime;
            set => _waitingForTableTime = value;
        }
    }
}
