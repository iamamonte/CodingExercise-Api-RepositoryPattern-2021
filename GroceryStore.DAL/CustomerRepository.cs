using GroceryStore.Infrastructure.Core;
using GroceryStore.Infrastructure.DataAccess;
using GroceryStore.Infrastructure.DataAccess.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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

        private async void SaveContext()
        {
            await Task.Factory.StartNew(() => _databaseAccess.Write(JsonSerializer.Serialize(new CustomersDbModel { Customers = this.Customers.ToList() }, options)));
            
        }


        public async override void AddAsync(CustomerEntity entity)
        {

            int nextId = Customers.Max(x => x.Id) + 1;
            entity.Id = nextId;
            Customers.Add(entity);
            SaveContext();
        }

        public async override void DeleteAsync(CustomerEntity entity)
        {
            CustomerEntity customer = await FindByIdAsync(entity.Id);
            if (customer != null)
            {
                Customers.Remove(customer);
            }
            SaveContext();
            
        }

        public async override Task<CustomerEntity> FindByIdAsync(int id)
        {
            return await Task.FromResult(Customers.FirstOrDefault(x => x.Id == id));
        }

        public async override Task<IEnumerable<CustomerEntity>> ListAsync()
        {
            return await Task.FromResult(Customers);
        }

        public async override void UpdateAsync(CustomerEntity entity)
        {
            CustomerEntity customer = await FindByIdAsync(entity.Id);
            customer.Name = entity.Name;
            SaveContext();
        }
    }
}
