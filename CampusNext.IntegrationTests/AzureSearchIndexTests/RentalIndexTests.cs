using CampusNext.AzureSearch.Indexer;
using CampusNext.AzureSearch.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests.AzureSearchIndexTests
{
    [TestClass]
    public class RentalIndexTests
    {
        private IIndexer _rentalIndexer;

        [TestInitialize]
        public void Initialize()
        {
            _rentalIndexer = new RentalIndex(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
        }

        [TestMethod]
        public void DeleteIndex()
        {
            Assert.AreEqual(true, _rentalIndexer.Delete());
        }

        [TestMethod]
        public void CreateIndex()
        {
            _rentalIndexer.Create();
        }

        [TestMethod]
        public void UpdateIndex()
        {
            _rentalIndexer.Update();
        }

        [TestMethod]
        public void GetRentalIndex()
        {
            IAzureSearchRepository azureRentalRepository = new AzureSearchFindTutorRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
            var result = azureRentalRepository.GetIndex("shareride");
            const int expected = 9;
            var actual = result.Fields.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
