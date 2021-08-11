using GroceryStore.Infrastructure;
using System;

namespace GroceryStoreAPI.Helpers
{
    public class SerilogAdapter : ILoggerAdapter
    {
        private readonly Serilog.ILogger _logger;

        public SerilogAdapter(Serilog.ILogger logger) 
        {
            _logger = logger;
        }

        public void Debug(string e, params object[] parameters)
        {
            _logger?.Debug(e, parameters);
        }

        public void Error(Exception e, string message, params object[] parameters)
        {
            _logger?.Error(e, message, parameters);
        }

        public void Information(string e, params object[] parameters)
        {
            _logger?.Information(e, parameters);
        }

        public void Warn(string e, params object[] parameters)
        {
            _logger.Warning(e, parameters);
        }
    }
}
