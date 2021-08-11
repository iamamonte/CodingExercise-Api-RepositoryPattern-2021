using GroceryStore.Infrastructure.Core;
using GroceryStore.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore.Infrastructure.DataAccess
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : EntityBase
    {
        public virtual T FindById(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<T> List()
        {
            throw new NotImplementedException();
        }

        public virtual Task<T> FindByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public virtual void UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void DeleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual void AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual Task<IEnumerable<T>> ListAsync()
        {
            throw new NotImplementedException();
        }
    }
}
