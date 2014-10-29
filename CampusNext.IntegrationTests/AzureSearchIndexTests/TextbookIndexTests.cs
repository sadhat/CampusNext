using CampusNext.AzureSearch.Indexer;
using CampusNext.AzureSearch.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests.AzureSearchIndexTests
{
    [TestClass]
    public class TextbookIndexTests
    {
        private IIndexer _textbookIndexer;

        [TestInitialize]
        public void Initialize()
        {
            _textbookIndexer = new TextbookIndex(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
        }

        [TestMethod]
        public void DeleteIndex()
        {
            Assert.AreEqual(true, _textbookIndexer.Delete());
        }

        [TestMethod]
        public void CreateIndex()
        {
            _textbookIndexer.Create();
        }

        [TestMethod]
        public void UpdateIndex()
        {
            _textbookIndexer.Update();
        }

        [TestMethod]
        public void GetTextbookIndex()
        {
            IAzureSearchRepository azureTextbookRepository = new AzureSearchTextbookRepository(AzureSearchCredential.ServiceName, AzureSearchCredential.ServiceApiKey);
            var result = azureTextbookRepository.GetIndex("textbook");
            const int expected = 11;
            var actual = result.Fields.Count;
            Assert.AreEqual(expected, actual);
        }
    }
}
