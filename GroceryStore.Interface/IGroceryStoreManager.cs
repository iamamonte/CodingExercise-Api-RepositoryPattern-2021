using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.Interface
{
    public interface IGroceryStoreManager
    {
        public IResponse<IEnumerable<ICustomer>> CreateOrUpdateCustomers(ICustomer[] customers);
        public IResponse<object> DeleteCustomers(ICustomer[] customers);
        public IResponse<IEnumerable<ICustomer>> FindCustomers(Func<ICustomer, bool> searchExpression = null);
    }
}
