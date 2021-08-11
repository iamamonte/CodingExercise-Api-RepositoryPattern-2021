using GroceryStore.Domain.Interface.Core;

namespace GroceryStore.Infrastructure.Core
{
    public class CustomerEntity : EntityBase, ICustomer
    {
        public string Name { get; set; }
    }
}
