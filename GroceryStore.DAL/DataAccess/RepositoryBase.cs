using GroceryStore.Infrastructure.Core;
using GroceryStore.Infrastructure.DataAccess.Interface;
using System;
using System.Collections.Generic;

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
    }
}
