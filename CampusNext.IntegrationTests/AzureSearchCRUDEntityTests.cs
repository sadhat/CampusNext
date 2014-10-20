using System;
using System.Linq;
using CampusNext.AzureSearch.Repository;
using CampusNext.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests
{
    [TestClass]
    public class AzureSearchCRUDEntityTests
    {
        string serviceName = "campusnext";
        string serviceApiKey = "2CD1601EF0EAB39FBBB84F2E95EA727F";

        [TestMethod]
        public void AddTextbook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(serviceName, serviceApiKey);
            var newTextboook = new Textbook
            {
                Id = 1,
                Authors = "Megal",
                CampusCode = "NDSU",
                Course = "899A",
                Description = "Description",
                Name = "My Book",
                Price = 12.00
            };

            azureTextbookRepository.AddAsync(newTextboook);
        }

        [TestMethod]
        public void UpdateTextbook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(serviceName, serviceApiKey);
            var newTextboook = new Textbook
            {
                Id = 1,
                Authors = "Megal",
                CampusCode = "NDSU",
                Course = "899A",
                Description = "This is quite nice description",
                Name = "My Book2",
                Price = 12.00
            };

            var result = azureTextbookRepository.UpdateAsync(newTextboook).Result;
        }

        [TestMethod]
        public void DeleteTextbook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(serviceName, serviceApiKey);
            var newTextboook = new Textbook
            {
                Id = 1,
                Authors = "Megal",
                CampusCode = "NDSU",
                Course = "899",
                Description = "Description",
                Name = "My Book",
                Price = 12.00
            };

            var result = azureTextbookRepository.DeleteAsync(newTextboook).Result;
        }

        [TestMethod]
        public void CountTextbook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(serviceName, serviceApiKey);
            var result = azureTextbookRepository.Count();

        }

        [TestMethod]
        public void GetTextbook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(serviceName, serviceApiKey);
            var result = azureTextbookRepository.Get<Textbook>("1").Result;

        }

        [TestMethod]
        public void SearchTextbook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(serviceName, serviceApiKey);
            var result = azureTextbookRepository.Search("my book", "NDSU").Result;
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void SearchTextbookFirstItemHasValueOfMyBook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(serviceName, serviceApiKey);
            var result = azureTextbookRepository.Search("book", "NDSU").Result;
            var expected = (result.First() as Textbook).Name;
            Assert.AreEqual("My Book", expected);
        }
    }
}
