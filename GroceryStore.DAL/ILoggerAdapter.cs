using System;


namespace GroceryStore.Infrastructure
{
    public interface ILoggerAdapter
    {
        void Error(Exception e, string message, params object[] parameters);
        void Debug(string e, params object[] parameters);
        void Information(string e, params object[] parameters);
        void Warn(string e, params object[] parameters);
    }
}
