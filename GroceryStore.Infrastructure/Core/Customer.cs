
using GroceryStore.Domain.Interface.Core;

namespace GroceryStore.Domain.Core
{
    public class Customer :  ICustomer
    {

        public Customer() { }

        public Customer(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int Id { get; set; }
    }
}
