using GroceryStore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.DAL
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
