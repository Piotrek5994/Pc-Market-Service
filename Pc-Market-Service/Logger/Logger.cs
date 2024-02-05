using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pc_Market_Service.Logger
{
    public class Logger
    {
        private readonly ILogger<Logger> _consoleLogger;

        public Logger(ILogger<Logger> consoleLogger)
        {
            _consoleLogger = consoleLogger;
        }
        public void LogInformation(string message)
        {
            _consoleLogger.LogInformation(message);
        }

        public void LogWarning(string message)
        {
            _consoleLogger.LogWarning(message);
        }

        public void LogError(string message)
        {
            _consoleLogger.LogError(message);
        }
    }
}
