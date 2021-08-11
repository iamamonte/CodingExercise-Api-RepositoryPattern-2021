using GroceryStore.Domain.Interface.Core;
using GroceryStore.Manager.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStore.Domain.Interface.Manager
{
    public interface IGroceryStoreManager
    {
        public Task<IResponse<IEnumerable<ICustomer>>> CreateOrUpdateCustomers(ICustomer[] customers);
        public Task<IResponse<object>> DeleteCustomers(ICustomer[] customers);
        public Task<IResponse<IEnumerable<ICustomer>>> FindCustomers(Func<ICustomer, bool> searchExpression = null);
    }
}
