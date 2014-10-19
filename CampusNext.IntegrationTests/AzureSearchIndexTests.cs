using System;
using CampusNext.AzureSearch.Indexer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests
{
    [TestClass]
    public class AzureSearchIndexTests
    {
        string serviceName = "campusnext";
        string serviceApiKey = "2CD1601EF0EAB39FBBB84F2E95EA727F";

        [TestMethod]
        public void DeleteTextbookIndex()
        {
            IIndexer textBookIndexer = new TextbookIndex(serviceName, serviceApiKey);
            Assert.AreEqual(true, textBookIndexer.Delete());
        }

        [TestMethod]
        public void CreateTextbookIndex()
        {
            IIndexer textBookIndexer = new TextbookIndex(serviceName, serviceApiKey);
            textBookIndexer.Create();
        }

        [TestMethod]
        public void UpdateTextbookIndex()
        {
            IIndexer textBookIndexer = new TextbookIndex(serviceName, serviceApiKey);
            textBookIndexer.Update();
        }
    }
}
