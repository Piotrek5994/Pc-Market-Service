using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Helper
{
    public class WorkerHelper : IHostedService
    {
        private Timer _timer; // Timer do planowania zadań

        // Metoda StartAsync jest wywoływana przy uruchomieniu usługi, inicjalizuje harmonogram zadania.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            ScheduleNextRun();
            return Task.CompletedTask; // Zwraca ukończone zadanie, nie blokując wątku
        }

        // Metoda ScheduleNextRun planuje następne uruchomienie zadania.
        private void ScheduleNextRun()
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddHours(8); // Następne uruchomienie dzisiaj o 8:00 rano
            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1); // Jeśli jest po 8:00, planuje na następny dzień
            }

            var dueTime = nextRun - now; // Oblicza czas do następnego uruchomienia
            // Ustawia timer na jednorazowe uruchomienie z obliczonym opóźnieniem
            _timer = new Timer(DoWork, null, dueTime, Timeout.InfiniteTimeSpan);
        }

        // Metoda DoWork zawiera logikę, która ma być wykonana przez timer.
        private void DoWork(object state)
        {
            Console.WriteLine("Performing work: " + DateTime.Now); // Logika zadania, np. logowanie

            ScheduleNextRun(); // Replanuje następne uruchomienie po wykonaniu zadania
        }

        // Metoda StopAsync jest wywoływana przy zamykaniu usługi, służy do zatrzymania timera i czyszczenia.
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0); // Zatrzymuje timer
            return Task.CompletedTask; // Zwraca ukończone zadanie, nie wymaga czekania
        }
    }
}
