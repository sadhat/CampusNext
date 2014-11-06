using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using CampusNext.AzureSearch.Repository;
using CampusNext.Entity;

namespace CampusNext.Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ShareRideSearchController : ODataController
    {
        private readonly AzureSearchShareRideRepository _shareRideRepository;
        //Read Data from list    

        public ShareRideSearchController()
        {
            _shareRideRepository = new AzureSearchShareRideRepository();
        }
        public async Task<IQueryable<ShareRide>> Get([FromUri] ShareRideSearchOption searchOption)

        { 
            var result = await _shareRideRepository.Search(searchOption.Keyword, searchOption.CampusName, GetFilterDictionary(searchOption));
            return
                result.Cast<ShareRide>().AsQueryable();
        }

        private IDictionary<string, string> GetFilterDictionary(ShareRideSearchOption searchOption)
        {
            IDictionary<string, string> dictionary = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(searchOption.FromLocation))
                dictionary.Add("fromLocation", searchOption.FromLocation);
            if (!string.IsNullOrWhiteSpace(searchOption.ToLocation))
                dictionary.Add("toLocation", searchOption.ToLocation);
            if (!string.IsNullOrWhiteSpace(searchOption.StartDateTime))
                dictionary.Add("startDateTime", searchOption.StartDateTime);
            if (!string.IsNullOrWhiteSpace(searchOption.ReturnDateTime))
                dictionary.Add("returnDateTime", searchOption.ReturnDateTime);

            return dictionary.Count > 0 ? dictionary : null;
        } 
    }
}
