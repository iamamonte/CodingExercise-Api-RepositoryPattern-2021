using Bogus;
using GroceryStore.Infrastructure;
using GroceryStore.Infrastructure.Core;
using GroceryStore.Infrastructure.DataAccess.Interface;
using Moq;
using NUnit.Framework;
using System.Linq;

namespace GroceryStore.Tests
{
    public class CustomerRepositoryTests : IntegrationTestBase
    {
        private readonly Faker faker = new Faker();
        private CustomerRepository customerRepository;
        
        [SetUp]
        public void SetUp() 
        {
            Mock<IDatabaseAccess> mockDatabaseAccess = new Mock<IDatabaseAccess>();
            mockDatabaseAccess.Setup(x => x.Init()).Returns(dbJson);
            mockDatabaseAccess.Setup(x => x.Write(It.IsAny<string>()))
                .Callback<string>(newDbJson => dbJson = newDbJson);
            customerRepository = new CustomerRepository(mockDatabaseAccess.Object);
        }

        [Test]
        public void Add_Succeeds()
        {
            int expectedCustomerCount = customerRepository.ListAsync().Result.Count() + 1;
            CustomerEntity newCustomer = new CustomerEntity { Name = faker.Name.FullName() };
            customerRepository.AddAsync(newCustomer);
            
            int actualCustomerCount = customerRepository.ListAsync().Result.Count();
            Assert.AreEqual(expectedCustomerCount, actualCustomerCount);
            
            CustomerEntity createdCustomer = customerRepository.FindByIdAsync(newCustomer.Id).Result;
            Assert.IsNotNull(createdCustomer);
            Assert.AreEqual(newCustomer.Name, createdCustomer.Name);
        }

        [Test]
        public void Update_Succeeds() 
        {
            var existingCustomers = customerRepository.ListAsync().Result;
            int expectedCustomerCount = existingCustomers.Count();
            CustomerEntity expectedCustomer = new CustomerEntity { Name = $"Updated {faker.Name.FullName()}", Id = existingCustomers.First().Id };
            customerRepository.UpdateAsync(expectedCustomer);

            CustomerEntity actualCustomer = customerRepository.FindByIdAsync(expectedCustomer.Id).Result;
    
            Assert.IsNotNull(actualCustomer);
            Assert.AreEqual(expectedCustomer.Name, actualCustomer.Name);
            Assert.AreEqual(expectedCustomerCount, customerRepository.ListAsync().Result.Count());
        }

        [Test]
        public void Delete_Succeeds() 
        {
            int expectedCustomerCount = customerRepository.ListAsync().Result.Count() - 1;
            customerRepository.DeleteAsync(new CustomerEntity { Id = 1 });

            Assert.AreEqual(expectedCustomerCount, customerRepository.ListAsync().Result.Count());

        }

        [Test]
        public void FindById_ReturnsNull() 
        {
            Assert.IsNull(customerRepository.FindByIdAsync(-1).Result);
        }
    }
}