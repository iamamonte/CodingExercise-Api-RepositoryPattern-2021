using GroceryStore.Interface;
using Microsoft.Extensions.Logging;
using System;

namespace GroceryStore.Infrastructure
{
    public class LoggerAdapter : ILoggerAdapter
    {
        private  readonly ILogger _logger;
        public LoggerAdapter(ILogger logger = null) 
        {
            _logger = logger;
        }
        public ILogger Logger { get => _logger; }

        public void Debug(string e, params object[] parameters)
        {
            _logger?.LogDebug(e, parameters);
        }

        public void Error(Exception e, string message, params object[] parameters)
        {
            _logger?.LogError(e, message, parameters);
        }

        public void Information(string e, params object[] parameters)
        {
            _logger?.LogInformation(e, parameters);
        }

        public void Warn(string e, params object[] parameters)
        {
            _logger.LogWarning(e, parameters);
        }
    }
}
