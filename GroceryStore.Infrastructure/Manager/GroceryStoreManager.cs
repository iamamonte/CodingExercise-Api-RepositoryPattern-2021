using GroceryStore.Domain.Core;
using GroceryStore.Domain.Interface.Core;
using GroceryStore.Domain.Interface.Manager;
using GroceryStore.Infrastructure;
using GroceryStore.Infrastructure.Core;
using GroceryStore.Infrastructure.DataAccess.Interface;
using GroceryStore.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroceryStore.Domain.Manager
{
    public class GroceryStoreManager : IGroceryStoreManager
    {
        private readonly IRepository<CustomerEntity> _customerRespository;
        private readonly ILoggerAdapter _libraryLogger;

        public GroceryStoreManager(IRepository<CustomerEntity> repository, ILoggerAdapter libraryLogger) 
        {
            _customerRespository = repository;
            _libraryLogger = libraryLogger;
        }

        public IResponse<IEnumerable<ICustomer>> CreateOrUpdateCustomers(ICustomer[] customers)
        {
            try
            {
                List<ICustomer> affectedCustomers = new List<ICustomer>();
                foreach (var customer in customers)
                {
                    var entity = _customerRespository.FindById(customer.Id);
                    if (entity != null)
                    {
                        entity.Name = customer.Name;
                        _customerRespository.Update(entity);
                    }
                    else
                    {
                        var newCustomer = new CustomerEntity { Name = customer.Name };
                        _customerRespository.Add(newCustomer);
                        customer.Id = newCustomer.Id;
                    }
                    affectedCustomers.Add(customer);
                }
                return new ManagerResponse<IEnumerable<ICustomer>>(affectedCustomers);
            }
            catch (Exception e)
            {
                string errorMessage = "Failed to create or update customers";
                _libraryLogger.Error(e, errorMessage, customers);
                return ManagerResponse<IEnumerable<ICustomer>>.Error(e);
            }
           
        }

        public IResponse<object> DeleteCustomers(ICustomer[] customers)
        {
            try
            {
                int deletedEntities = 0;
                foreach (var customer in customers)
                {
                    var entity = _customerRespository.FindById(customer.Id);
                    if (entity != null)
                    {
                        _customerRespository.Delete(entity);
                        deletedEntities++;
                    }
                }
                return new ManagerResponse<object>(deletedEntities);
            }
            catch (Exception e)
            {
                string errorMessage = "Failed to delete customers.";
                _libraryLogger.Error(e, errorMessage, customers);
                return ManagerResponse<object>.Error(e);
            }
        }

        public IResponse<IEnumerable<ICustomer>> FindCustomers(Func<ICustomer, bool> searchExpression = null)
        {
            try
            {
                var result = _customerRespository.List()
                    .Where(searchExpression ?? (x => true))
                    .Select(x => new Customer { Id = x.Id, Name = x.Name });
                return new ManagerResponse<IEnumerable<ICustomer>>(result);
            }
            catch (Exception e)
            {
                string errorMessage = "Failed to find customers";
                _libraryLogger.Error(e, errorMessage, searchExpression);
                return ManagerResponse<IEnumerable<ICustomer>>.Error(e);
            }
        }
    }
}
