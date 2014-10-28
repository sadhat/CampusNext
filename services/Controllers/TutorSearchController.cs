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
    public class TutorSearchController : ODataController
    {
        private readonly AzureSearchFindTutorRepository _azureSearchFindTutorRepository;
        //Read Data from list    

        public TutorSearchController()
        {
            _azureSearchFindTutorRepository = new AzureSearchFindTutorRepository();
        }
        public async Task<IQueryable<FindTutor>> Get([FromUri] TutorSearchOption searchOption)
        {
            var result = await _azureSearchFindTutorRepository.Search(searchOption.Keyword, searchOption.CampusName);
            return
                result.Cast<FindTutor>().AsQueryable();
        }
    }
}
