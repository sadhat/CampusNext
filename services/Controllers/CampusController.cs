using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.OData;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;

namespace CampusNext.Services.Controllers
{
    public class CampusController : ApiController
    {
        [EnableQuery]
        // GET: api/Campuses
        public async Task<IQueryable<Campus>> GetCampuses()
        {
            return await new CampusRepository().All();
        }
    }
}
