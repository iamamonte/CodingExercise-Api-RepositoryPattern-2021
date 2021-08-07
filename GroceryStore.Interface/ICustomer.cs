using System;

namespace GroceryStore.Interface
{
    public interface ICustomer : IEntityBase
    {
        public string Name { get; set; }
    }
}
