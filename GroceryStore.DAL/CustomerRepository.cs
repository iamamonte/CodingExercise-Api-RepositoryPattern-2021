using GroceryStore.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GroceryStore.DAL
{
    public class CustomerRepository : IRepository<Customer>
    {
        private ICollection<Customer> Customers;
        private readonly IDatabaseAccess _databaseAccess;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private class CustomersDbModel 
        {
            public List<Customer> Customers { get; set; }
            public CustomersDbModel() { }
        }

        public CustomerRepository(IDatabaseAccess databaseAccess)
        {
            _databaseAccess = databaseAccess;
            string dbJson = _databaseAccess.Init();
            Customers = JsonSerializer.Deserialize<CustomersDbModel>(dbJson, options).Customers
                .ToList();
        }

        private void SaveContext()
        {
            _databaseAccess.Write(JsonSerializer.Serialize(new CustomersDbModel { Customers = this.Customers.ToList() }, options));
        }


        public void Add(Customer entity)
        {

            int nextId = Customers.Max(x => x.Id) + 1;
            entity.Id = nextId;
            Customers.Add(entity);
            SaveContext();
        }

        public void Delete(Customer entity)
        {
            Customer customer = FindById(entity.Id);
            if (customer != null)
            {
                Customers.Remove(customer);
            }
            SaveContext();
            
        }

        public Customer FindById(int id)
        {
            return Customers.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Customer> List()
        {
            return Customers.ToList();
        }

        public void Update(Customer entity)
        {
            Customer customer = FindById(entity.Id);
            customer.Name = entity.Name;
            SaveContext();
        }
    }
}
