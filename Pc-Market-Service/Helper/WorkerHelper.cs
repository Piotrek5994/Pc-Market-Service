using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Helper
{
    public class WorkerHelper
    {
        private Timer _timer;

        public WorkerHelper()
        {
            ScheduleNextRun();
        }

        private void ScheduleNextRun()
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddHours(8); // Następne uruchomienie dzisiaj o 8 rano
            if (now > nextRun)
            {
                // Jeśli już po 8, planujemy na jutro
                nextRun = nextRun.AddDays(1);
            }

            var dueTime = nextRun - now; // Obliczamy, ile czasu pozostało do uruchomienia
            _timer = new Timer(DoWork, null, dueTime, Timeout.InfiniteTimeSpan); // Ustawiamy timer na jednorazowe uruchomienie
        }

        private void DoWork(object state)
        {
            // Logika, która ma być wykonana o 8 rano
            Console.WriteLine("Wykonywanie pracy: " + DateTime.Now.ToString());

            // Po wykonaniu zadania, planujemy następne uruchomienie
            ScheduleNextRun();
        }
    }
}
