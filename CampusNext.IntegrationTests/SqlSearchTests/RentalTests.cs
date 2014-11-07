using System;
using System.Linq;
using CampusNext.DataAccess.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CampusNext.IntegrationTests.SqlSearchTests
{
    [TestClass]
    public class RentalTests
    {
        private RentalRepository _rentalRepository;

        [TestInitialize]
        public void Initialize()
        {
            _rentalRepository = new RentalRepository();
        }
        [TestMethod]
        public void SearchTest()
        {
            var list = _rentalRepository.Search("UOFT", "40", "60");
            Assert.AreEqual(1, list.Count());
        }

        [TestMethod]
        public void SearchReturnResultWithAdditionalDataTest()
        {
            var list = _rentalRepository.Search("UOFT", "40", "60", additionalInfo: "go");
            Assert.AreEqual(1, list.Count());
        }
    }
}
