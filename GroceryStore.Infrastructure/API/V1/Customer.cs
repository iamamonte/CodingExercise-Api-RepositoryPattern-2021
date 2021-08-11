using System;
using System.ComponentModel.DataAnnotations;

namespace GroceryStore.API.V1
{
    public class Customer
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
