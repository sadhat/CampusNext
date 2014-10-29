using CampusNext.AzureSearch.Indexer;
using CampusNext.AzureSearch.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests.AzureSearchIndexTests
{
    [TestClass]
    public class FindTutorIndexTests
    {
        private IIndexer _findTutorIndexer;

        [TestInitialize]
        public void Initialize()
        {
            _findTutorIndexer = new FindTutorIndex(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
        }

        [TestMethod]
        public void DeleteIndex()
        {
            Assert.AreEqual(true, _findTutorIndexer.Delete());
        }

        [TestMethod]
        public void CreateIndex()
        {
            _findTutorIndexer.Create();
        }

        [TestMethod]
        public void UpdateIndex()
        {
            _findTutorIndexer.Update();
        }

        [TestMethod]
        public void GetFindTutorIndex()
        {
            IAzureSearchRepository azureFindTutorRepository = new AzureSearchFindTutorRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
            var result = azureFindTutorRepository.GetIndex("findtutor");
            const int expected = 7;
            var actual = result.Fields.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
