using System.Linq;
using CampusNext.AzureSearch.Repository;
using CampusNext.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests.AzureSearchCrudOperationTests
{
    [TestClass]
    public class AzureSearchCrudFindTutorTests
    {
        private IAzureSearchRepository _azureSearchRepository;

        [TestInitialize]
        public void Initialize()
        {
            _azureSearchRepository = new AzureSearchFindTutorRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
        }

        [TestMethod]
        public void AddTutor()
        {
            var tutor = new FindTutor
            {
                Id = 1,
                CampusCode = "NDSU",
                Course = "899A",
                Description = "Description",
                Rate = "10.90 per hour"
            };

            var result = _azureSearchRepository.AddAsync(tutor).Result;
        }

        [TestMethod]
        public void UpdateTutor()
        {
            var tutor = new FindTutor
            {
                Id = 1,
                CampusCode = "NDSU",
                Course = "899A",
                Description = "Description",
                Rate = "10.90 per hour"
            };

            var result = _azureSearchRepository.UpdateAsync(tutor).Result;
        }

        [TestMethod]
        public void GetTutor()
        {
            var result = _azureSearchRepository.Get<FindTutor>("1").Result;

        }

        [TestMethod]
        public void DeleteTutor()
        {
            var tutor = new FindTutor
            {
                Id = 1
            };

            var result = _azureSearchRepository.DeleteAsync(tutor).Result;
        }

        [TestMethod]
        public void CountTextbook()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
            var result = azureTextbookRepository.Count();
        }

        [TestMethod]
        public void CountTutorForCampusNDSU()
        {
            var result = _azureSearchRepository.Count("findtutor", "NDSU").Result;
        }

        [TestMethod]
        public void CountTutorForCampusUND()
        {
            var result = _azureSearchRepository.Count("findtutor", "UND").Result;
        }

        [TestMethod]
        public void SearchTextbook()
        {
            var result = _azureSearchRepository.Search("d").Result;
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void SearchTextbookFirstItemHasValueOfMyBook()
        {
            var result = _azureSearchRepository.Search("Description", "NDSU").Result;
            var expected = (result.First() as FindTutor).Description;
            Assert.AreEqual("Description", expected);
        }
    }
}
