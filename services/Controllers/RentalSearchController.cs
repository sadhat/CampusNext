using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using CampusNext.AzureSearch.Repository;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;

namespace CampusNext.Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class RentalSearchController : ODataController
    {
        private readonly RentalRepository _shareRideRepository;
        //Read Data from list    

        public RentalSearchController()
        {
            _shareRideRepository = new RentalRepository();
        }
        public IQueryable<Rental> Get([FromUri] RentalSearchOption searchOption)

        {
            var result = _shareRideRepository.Search(searchOption.CampusName, searchOption.RentRangeFrom,
                searchOption.RentRangeTo, searchOption.Rooms, searchOption.AdditionalInfo);
            return result.AsQueryable();
        }
    }
}
