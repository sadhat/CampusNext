using System.IO;
using System.Linq;
using CampusNext.AzureSearch.Repository;
using CampusNext.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests.AzureSearchCrudOperationTests
{
    [TestClass]
    public class ShareRideTests
    {
        private IAzureSearchRepository _azureSearchRepository;

        [TestInitialize]
        public void Initialize()
        {
            _azureSearchRepository = new AzureSearchShareRideRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
        }

        [TestMethod]
        public void AddShareRide()
        {
            var entity = new ShareRide
            {
                Id = 1,
                CampusCode = "NDSU",
                FromLocation = "Fargo",
                IsRoundTrip = true,
                ToLocation = "NY",
                StartDateTime = "December 14, 2014 at 2 PM",
                ReturnDateTime = "Jan 15, 2015 anytime",
                AdditionalInfo = "All late arrival will be left behind"
            };

            var result = _azureSearchRepository.AddAsync(entity).Result;
        }

        [TestMethod]
        public void UpdateShareRide()
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
        public void GetShareRide()
        {
            var result = _azureSearchRepository.Get<FindTutor>("1").Result;

        }

        [TestMethod]
        public void DeleteShareRide()
        {
            var shareRide = new ShareRide
            {
                Id = 1
            };

            var result = _azureSearchRepository.DeleteAsync(shareRide).Result;
        }

        [TestMethod]
        public void CountShareRide()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchShareRideRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
            var result = azureTextbookRepository.Count().Result;
        }

        [TestMethod]
        public void CountShareRideForCampusNDSU()
        {
            var result = _azureSearchRepository.Count("shareride", "NDSU").Result;
        }

        [TestMethod]
        public void CountShareRideForCampusUND()
        {
            var result = _azureSearchRepository.Count("shareride", "UND").Result;
        }

        [TestMethod]
        public void SearchShareRide()
        {
            var result = _azureSearchRepository.Search("d").Result;
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void SearchShareRideFirstItemHasValueOfMyBook()
        {
            var result = _azureSearchRepository.Search("Description", "NDSU").Result;
            var expected = (result.First() as FindTutor).Description;
            Assert.AreEqual("Description", expected);
        }
    }
}
