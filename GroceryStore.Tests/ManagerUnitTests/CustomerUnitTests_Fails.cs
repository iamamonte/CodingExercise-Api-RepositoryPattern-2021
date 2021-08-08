using Bogus;
using GroceryStore.DAL;
using GroceryStore.Infrastructure;
using GroceryStore.Interface;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroceryStore.Tests.ManagerTests
{
    public class CustomerUnitTests_Fails
    {
        private GroceryStoreManager manager;
        private Faker faker = new Faker();
        Mock<ILoggerAdapter> mockLogger;
        
        [SetUp]
        public void Setup()
        {
            mockLogger = new Mock<ILoggerAdapter>();
            mockLogger.Setup(x => x.Debug(It.IsAny<string>(), It.IsAny<object[]>()));
            mockLogger.Setup(x => x.Warn(It.IsAny<string>(), It.IsAny<object[]>()));
            mockLogger.Setup(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()));
            mockLogger.Setup(x => x.Information(It.IsAny<string>(), It.IsAny<object[]>()));
        }

        [Test]
        public void Create_HandlesException() 
        {
            string errorMessage = faker.Lorem.Sentence();
            Mock<IRepository<CustomerEntity>> mockCustomerRepository = new Mock<IRepository<CustomerEntity>>();
            mockCustomerRepository.Setup(x => x.Add(It.IsAny<CustomerEntity>()))
                .Throws(new Exception(errorMessage));
            
            manager = new GroceryStoreManager(mockCustomerRepository.Object, mockLogger.Object);
            var response = manager.CreateOrUpdateCustomers(new Customer[] { new Customer { } });

            Assert.IsFalse(response.Succeeded);
            Assert.IsNull(response.Result);
            Assert.IsTrue(response.ErrorMessages.First().Contains(errorMessage));
            mockLogger.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Once());

        }

        [Test]
        public void Update_HandlesException()
        {
            string errorMessage = faker.Lorem.Sentence();
            Mock<IRepository<CustomerEntity>> mockCustomerRepository = new Mock<IRepository<CustomerEntity>>();
            mockCustomerRepository.Setup(x => x.FindById(1))
                .Returns(new CustomerEntity { Id = 1, Name = "Found Customer" });
            mockCustomerRepository.Setup(x => x.Update(It.IsAny<CustomerEntity>()))
                .Throws(new Exception(errorMessage));

            manager = new GroceryStoreManager(mockCustomerRepository.Object, mockLogger.Object);
            var response = manager.CreateOrUpdateCustomers(new Customer[] { new Customer { Id = 1 } });

            Assert.IsFalse(response.Succeeded);
            Assert.IsNull(response.Result);
            Assert.IsTrue(response.ErrorMessages.First().Contains(errorMessage));
            mockLogger.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Once());

        }

        [Test]
        public void FindCustomers_HandlesException()
        {
            string errorMessage = faker.Lorem.Sentence();
            Mock<IRepository<CustomerEntity>> mockCustomerRepository = new Mock<IRepository<CustomerEntity>>();
            mockCustomerRepository.Setup(x => x.List())
                .Throws(new Exception(errorMessage));

            manager = new GroceryStoreManager(mockCustomerRepository.Object, mockLogger.Object);
            var response = manager.FindCustomers(x => true);

            Assert.IsFalse(response.Succeeded);
            Assert.IsNull(response.Result);
            Assert.IsTrue(response.ErrorMessages.First().Contains(errorMessage));
            mockLogger.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Once());

        }

        [Test]
        public void DeleteCustomers_HandlesException()
        {
            string errorMessage = faker.Lorem.Sentence();
            Mock<IRepository<CustomerEntity>> mockCustomerRepository = new Mock<IRepository<CustomerEntity>>();
            mockCustomerRepository.Setup(x => x.FindById(1))
               .Returns(new CustomerEntity { Id = 1, Name = "Found Customer" });
            mockCustomerRepository.Setup(x => x.Delete(It.IsAny<CustomerEntity>()))
                .Throws(new Exception(errorMessage));

            manager = new GroceryStoreManager(mockCustomerRepository.Object, mockLogger.Object);
            var response = manager.DeleteCustomers(new Customer[] { new Customer { Id = 1 } });

            Assert.IsFalse(response.Succeeded);
            Assert.IsNull(response.Result);
            Assert.IsTrue(response.ErrorMessages.First().Contains(errorMessage));
            mockLogger.Verify(x => x.Error(It.IsAny<Exception>(), It.IsAny<string>(), It.IsAny<object[]>()), Times.Once());

        }
    }
}
