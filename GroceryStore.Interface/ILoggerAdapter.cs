using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Interface
{
    public interface ILoggerAdapter
    {
        void Error(Exception e, string message, params object[] parameters);
        void Debug(string e, params object[] parameters);
        void Information(string e, params object[] parameters);
        void Warn(string e, params object[] parameters);
    }
}
