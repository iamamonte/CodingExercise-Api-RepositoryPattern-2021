using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Manager.Interface
{
    public interface IResponse<T> where T : class
    {
        public T Result { get; }
        public List<string> ErrorMessages { get; }
        public int StatusCode { get;  }
        public bool Succeeded { get; }
    }
}
