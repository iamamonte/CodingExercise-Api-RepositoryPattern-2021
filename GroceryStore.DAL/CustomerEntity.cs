using GroceryStore.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroceryStore.DAL
{
    public class CustomerEntity : EntityBase, ICustomer
    {
        public string Name { get; set; }
    }
}
