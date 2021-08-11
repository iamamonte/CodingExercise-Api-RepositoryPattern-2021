using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore.Infrastructure.DataAccess.Interface
{
    public interface IRepository<T>  where T : class
    {
        T FindById(int id);
        void Update(T entity);
        void Delete(T entity);
        void Add(T entity);
        IEnumerable<T> List();

        Task<T> FindByIdAsync(int id);
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
        void AddAsync(T entity);
        Task<IEnumerable<T>> ListAsync();
    }
}
