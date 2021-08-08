using System;

namespace GroceryStore.Interface
{
    public interface ICustomer
    {
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
