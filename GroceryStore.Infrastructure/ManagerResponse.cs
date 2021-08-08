using GroceryStore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Infrastructure
{
    public class ManagerResponse<T> : IResponse<T> where T : class 
    {
        public const int StatusSuccess = 200;
        public const int StatusInvalid = 400;
        public const int StatusError = 500;

        private readonly int _statusCode = -1;
        private readonly T _data;
        private readonly List<string> _errors;

        public static ManagerResponse<T> Error(Exception exception, string message = null) 
        {
            return new ManagerResponse<T>(StatusError, null, new List<string> { exception.Message, message });
        }

        public ManagerResponse(T data, int statusCode = StatusSuccess) 
        {
            _statusCode = statusCode;
            _data = data;
            _errors = new List<string>();

        }

        public ManagerResponse(int statusCode, T data, List<string> errors = null)
        {
            _statusCode = statusCode;
            _data = data;
            _errors = errors ?? new List<string>();

        }
        public T Result => _data;

        public List<string> ErrorMessages => _errors;

        public int StatusCode => _statusCode;

        public bool Succeeded => _statusCode == StatusSuccess;
    }
}
