using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;

namespace CampusNext.Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class CategoryCatalogController : ApiController
    {
        [EnableQuery]
        // GET: api/CategoryCatalog
        public async Task<CategoryCatalog> Get([FromUri] CategoryCatalogOption categoryCatalogOption)
        {
            return await new CategoryCatalogRepository().All(categoryCatalogOption.CampusName);
        }
    }
}
