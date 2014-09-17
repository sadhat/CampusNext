using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using CampusNext.Services.BusinessLayer;
using CampusNext.Services.Models;

namespace CampusNext.Services.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using CampusNext.Services.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Textbook>("Textbooks");
    config.Routes.MapODataRoute("odata", "odata", builder.GetEdmModel());
    */
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TextbookSearchController : ODataController
    {
        private readonly ITextbookRepository _textbookRepository;
        //Read Data from list    

        public TextbookSearchController(ITextbookRepository textbookRepository)
        {
            _textbookRepository = textbookRepository;
        }
        public IQueryable<Textbook> Get()    

        {    
            return _textbookRepository.All(null);    
        }

        public IHttpActionResult Post(Textbook textbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _textbookRepository.Add(textbook);
            return Ok();
        }
    }
}
