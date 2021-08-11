using System.Collections.Generic;

namespace GroceryStore.Infrastructure.DataAccess.Interface
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
