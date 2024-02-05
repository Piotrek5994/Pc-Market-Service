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
        private Timer _timer;

        // The StartAsync method is called upon the service's startup, initializes the task's schedule.
        public Task StartAsync(CancellationToken cancellationToken)
        {
            ScheduleNextRun();
            return Task.CompletedTask;
        }

        // The ScheduleNextRun method schedules the next task execution.
        private void ScheduleNextRun()
        {
            var now = DateTime.Now;
            var nextRun = now.Date.AddHours(8); // Next execution scheduled for today at 8:00 AM
            if (now > nextRun)
            {
                nextRun = nextRun.AddDays(1); // If it's past 8:00, schedule for the next day
            }

            var dueTime = nextRun - now; // Calculates time until the next execution
            // Sets the timer for a one-time execution with the calculated delay
            _timer = new Timer(DoWork, null, dueTime, Timeout.InfiniteTimeSpan);
        }

        // The DoWork method contains the logic to be executed by the timer.
        private void DoWork(object state)
        {
            Console.WriteLine("Performing work: " + DateTime.Now);

            ScheduleNextRun();
        }

        // The StopAsync method is called upon the service's shutdown, used to stop the timer and cleanup.
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
