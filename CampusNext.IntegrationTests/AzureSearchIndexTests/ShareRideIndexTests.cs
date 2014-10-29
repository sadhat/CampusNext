using CampusNext.AzureSearch.Indexer;
using CampusNext.AzureSearch.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests.AzureSearchIndexTests
{
    [TestClass]
    public class ShareRideIndexTests
    {
        private IIndexer _shareRideIndexer;

        [TestInitialize]
        public void Initialize()
        {
            _shareRideIndexer = new ShareRideIndex(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
        }

        [TestMethod]
        public void DeleteIndex()
        {
            Assert.AreEqual(true, _shareRideIndexer.Delete());
        }

        [TestMethod]
        public void CreateIndex()
        {
            _shareRideIndexer.Create();
        }

        [TestMethod]
        public void UpdateIndex()
        {
            _shareRideIndexer.Update();
        }

        [TestMethod]
        public void GetShareRideIndex()
        {
            IAzureSearchRepository azureShareRideRepository = new AzureSearchFindTutorRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
            var result = azureShareRideRepository.GetIndex("shareride");
            const int expected = 9;
            var actual = result.Fields.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
