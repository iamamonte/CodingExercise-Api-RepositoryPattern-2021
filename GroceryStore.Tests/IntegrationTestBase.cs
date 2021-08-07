using NUnit.Framework;
using System.IO;

namespace GroceryStore.Tests
{
    public class IntegrationTestBase
    {
        protected string dbJson;
        [OneTimeSetUp]
        public void SetUp() 
        {
            dbJson = File.ReadAllText($"Resources/database.json");
        }
        
    }
}