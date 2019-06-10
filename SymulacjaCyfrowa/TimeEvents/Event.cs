using System;
using System.Collections.Generic;

namespace SymulacjaCyfrowa.TimeEvents
{
    public class Event
    {
        private int _eventTime;
        private string _type;

        protected Event(int eventTime, string type)
        {
            _eventTime = eventTime;
            _type = type;
        }


        protected Event() { }
        protected static readonly Random Rnd = new Random();

        public static void AddToScheduler(List<Event> scheduler, Event addedEvent)
        {
            if (scheduler.Count != 0)
            {
                int index = 0;
                bool flag = false;
                foreach (var e in scheduler)
                {
                    if (e.EventTime >= addedEvent.EventTime)
                    {
                        scheduler.Insert(index, addedEvent);
                        flag = true;
                        break;
                    }
                    index++;
                }
                if (flag == false) scheduler.Add(addedEvent);
            }
            else scheduler.Add(addedEvent);
        }

        protected static void Sorted(List<Event> scheduler) // Sorting scheduler
        {
            scheduler.Sort((x, y) => x.EventTime.CompareTo(y.EventTime)); // Sorted Scheduler
        }

        protected static void SortedByEatingTime(List<Client> clients) // Sorting Storage
        {
            clients.Sort((x, y) => x.EatingTime.CompareTo(y.EatingTime));
        }

        public static void ShowScheduler(List<Event> scheduler)
        {
            foreach (var schedul in scheduler)
            {
                Console.Write(schedul._type + ": " + schedul._eventTime + " ");
            }
            Console.WriteLine();
        }

        public int EventTime
        {
            get => _eventTime;
            set => _eventTime = value;
        }

        protected string Type
        {
            get => _type;
            set => _type = value;
        }

        public static int SystemTime { get; set; }

        public virtual void Execute(List<Event> scheduler, Restaurant restaurant)
        {

        }

    }
}