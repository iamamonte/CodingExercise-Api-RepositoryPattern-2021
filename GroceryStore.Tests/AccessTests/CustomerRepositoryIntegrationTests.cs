using Bogus;
using GroceryStore.DAL;
using GroceryStore.Interface;
using Moq;
using NUnit.Framework;
using System.IO;
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
            int expectedCustomerCount = customerRepository.List().Count() + 1;
            CustomerEntity newCustomer = new CustomerEntity { Name = faker.Name.FullName() };
            customerRepository.Add(newCustomer);
            
            int actualCustomerCount = customerRepository.List().Count();
            Assert.AreEqual(expectedCustomerCount, actualCustomerCount);
            
            CustomerEntity createdCustomer = customerRepository.FindById(newCustomer.Id);
            Assert.IsNotNull(createdCustomer);
            Assert.AreEqual(newCustomer.Name, createdCustomer.Name);
        }

        [Test]
        public void Update_Succeeds() 
        {
            var existingCustomers = customerRepository.List();
            int expectedCustomerCount = existingCustomers.Count();
            CustomerEntity expectedCustomer = new CustomerEntity { Name = $"Updated {faker.Name.FullName()}", Id = existingCustomers.First().Id };
            customerRepository.Update(expectedCustomer);

            CustomerEntity actualCustomer = customerRepository.FindById(expectedCustomer.Id);
    
            Assert.IsNotNull(actualCustomer);
            Assert.AreEqual(expectedCustomer.Name, actualCustomer.Name);
            Assert.AreEqual(expectedCustomerCount, customerRepository.List().Count());
        }

        [Test]
        public void Delete_Succeeds() 
        {
            int expectedCustomerCount = customerRepository.List().Count() - 1;
            customerRepository.Delete(new CustomerEntity { Id = 1 });

            Assert.AreEqual(expectedCustomerCount, customerRepository.List().Count());

        }

        [Test]
        public void FindById_ReturnsNull() 
        {
            Assert.IsNull(customerRepository.FindById(-1));
        }
    }
}