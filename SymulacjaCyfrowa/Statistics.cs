using System;
using System.Collections.Generic;
using System.IO;

namespace SymulacjaCyfrowa
{
    public static class Statistics
    {

        public static int ActualClients { get; set; } = 0;
        public static readonly List<int> WaitingForTableTime = new List<int>();
        public static readonly List<int> TableQueueLength = new List<int>();
        public static readonly List<int> WaitingForServiceTime = new List<int>();
        public static readonly List<int> CashQueueLength = new List<int>();
        public static readonly List<int> MeasSystemTime = new List<int>();
        public static readonly List<int> ActualClientsAmount = new List<int>();

        private static int _counter = 1000;

        private static double GetAverage(List<int> list)
        {
            double ret = 0.0;
            foreach (var el in list)
            {
                ret += el;
            }

            return ret / list.Count;
        }

        public static void ShowStatistics(int clientsCounter)
        {
            if (clientsCounter % 1000 == 0)
            {
                var thousand = "Średni czas oczekiwania na stolik:  " + Statistics.GetAverage(Statistics.WaitingForTableTime) + "\n";
                thousand += "Średni czas oczekiwania na kelnera:  " + Statistics.GetAverage(Statistics.WaitingForServiceTime) + "\n";
                thousand += "Średnia kolejka do stolików:  " + Statistics.GetAverage(Statistics.TableQueueLength) + "\n";
                thousand += "Średnia kolejka do kas:  " + Statistics.GetAverage(Statistics.CashQueueLength) + "\n";
                thousand += "Średnia liczba klientów w systemie:  " + Statistics.GetAverage(Statistics.ActualClientsAmount);
                Console.WriteLine("{0}k: \n" + thousand, _counter / 1000);
                _counter += 1000;
                thousand = "";
            }
        }

        public static void SaveStatistics()
        {
            string pathDesktop = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string averageTableQueueTime = pathDesktop + "\\WaitingForTableTime.csv";
            string averageTableQueueLength = pathDesktop + "\\TableQueueLength.csv";
            string averageTableServiceTime = pathDesktop + "\\WaitingForServiceTime.csv";
            string averageCashQueueLength = pathDesktop + "\\CashQueueLength.csv";
            string measSystemTime = pathDesktop + "\\MeasSystemTime.csv";
            string actualClientsAmount = pathDesktop + "\\ActualClients.csv";

            if (!File.Exists(averageTableQueueTime))
            {
                File.Create(averageTableQueueTime).Close();
            }
            if (!File.Exists(averageTableQueueLength))
            {
                File.Create(averageTableQueueLength).Close();
            }
            if (!File.Exists(averageTableServiceTime))
            {
                File.Create(averageTableServiceTime).Close();
            }
            if (!File.Exists(averageCashQueueLength))
            {
                File.Create(averageCashQueueLength).Close();
            }
            if (!File.Exists(measSystemTime))
            {
                File.Create(measSystemTime).Close();
            }
            if (!File.Exists(actualClientsAmount))
            {
                File.Create(actualClientsAmount).Close();
            }

            string delimeter = "\n";

            using (System.IO.TextWriter writer = File.CreateText(averageTableQueueTime))
            {
                for (int index = 0; index < WaitingForTableTime.Count; index++)
                {
                    writer.WriteLine(string.Join(delimeter, WaitingForTableTime[index]));
                }
            }

            using (System.IO.TextWriter writer = File.CreateText(averageTableQueueLength))
            {
                for (int index = 0; index < TableQueueLength.Count; index++)
                {
                    writer.WriteLine(string.Join(delimeter, TableQueueLength[index]));
                }
            }

            using (System.IO.TextWriter writer = File.CreateText(averageTableServiceTime))
            {
                for (int index = 0; index < WaitingForServiceTime.Count; index++)
                {
                    writer.WriteLine(string.Join(delimeter, WaitingForServiceTime[index]));
                }
            }

            using (System.IO.TextWriter writer = File.CreateText(averageCashQueueLength))
            {
                for (int index = 0; index < CashQueueLength.Count; index++)
                {
                    writer.WriteLine(string.Join(delimeter, CashQueueLength[index]));
                }
            }

            using (System.IO.TextWriter writer = File.CreateText(measSystemTime))
            {
                for (int index = 0; index < MeasSystemTime.Count; index++)
                {
                    writer.WriteLine(string.Join(delimeter, MeasSystemTime[index]));
                }
            }

            using (System.IO.TextWriter writer = File.CreateText(actualClientsAmount))
            {
                for (int index = 0; index < ActualClientsAmount.Count; index++)
                {
                    writer.WriteLine(string.Join(delimeter, ActualClientsAmount[index]));
                }
            }
        }

    }
}
