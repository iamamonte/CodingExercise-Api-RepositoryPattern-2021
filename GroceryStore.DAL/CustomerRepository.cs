using GroceryStore.Infrastructure.Core;
using GroceryStore.Infrastructure.DataAccess;
using GroceryStore.Infrastructure.DataAccess.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace GroceryStore.Infrastructure
{
    public class CustomerRepository : RepositoryBase<CustomerEntity>, IRepository<CustomerEntity>
    {
        private ICollection<CustomerEntity> Customers;
        private readonly IDatabaseAccess _databaseAccess;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private class CustomersDbModel 
        {
            public List<CustomerEntity> Customers { get; set; }
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


        public override void Add(CustomerEntity entity)
        {

            int nextId = Customers.Max(x => x.Id) + 1;
            entity.Id = nextId;
            Customers.Add(entity);
            SaveContext();
        }

        public override void Delete(CustomerEntity entity)
        {
            CustomerEntity customer = FindById(entity.Id);
            if (customer != null)
            {
                Customers.Remove(customer);
            }
            SaveContext();
            
        }

        public override CustomerEntity FindById(int id)
        {
            return Customers.FirstOrDefault(x => x.Id == id);
        }

        public override IEnumerable<CustomerEntity> List()
        {
            return Customers;
        }

        public void Update(CustomerEntity entity)
        {
            CustomerEntity customer = FindById(entity.Id);
            customer.Name = entity.Name;
            SaveContext();
        }
    }
}
