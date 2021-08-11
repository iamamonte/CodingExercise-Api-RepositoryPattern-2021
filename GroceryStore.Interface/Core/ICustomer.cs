using System;

namespace GroceryStore.Domain.Interface.Core
{
    public interface ICustomer
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
