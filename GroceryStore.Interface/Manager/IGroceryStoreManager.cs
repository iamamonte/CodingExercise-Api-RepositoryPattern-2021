using GroceryStore.Domain.Interface.Core;
using GroceryStore.Manager.Interface;
using System;
using System.Collections.Generic;

namespace GroceryStore.Domain.Interface.Manager
{
    public interface IGroceryStoreManager
    {
        public IResponse<IEnumerable<ICustomer>> CreateOrUpdateCustomers(ICustomer[] customers);
        public IResponse<object> DeleteCustomers(ICustomer[] customers);
        public IResponse<IEnumerable<ICustomer>> FindCustomers(Func<ICustomer, bool> searchExpression = null);
    }
}
