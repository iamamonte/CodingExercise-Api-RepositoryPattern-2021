using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Interface
{
    public interface IRepository<T>  where T : class
    {
        T FindById(int id);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
        IEnumerable<T> List();
    }
}
