using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using CampusNext.AzureSearch.Repository;
using CampusNext.Entity;
using CampusNext.Services.BusinessLayer;

namespace CampusNext.Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TextbookSearchController : ODataController
    {
        private readonly AzureSearchTextbookRepository _textbookRepository;
        //Read Data from list    

        public TextbookSearchController()
        {
            _textbookRepository = new AzureSearchTextbookRepository();
        }
        public async Task<IQueryable<Textbook>> Get([FromUri] TextbookSearchOption searchOption)

        {
            var result = await _textbookRepository.Search(searchOption.Keyword, searchOption.CampusName);
            return
                result.Cast<Textbook>().AsQueryable();
        }

        public async Task<IHttpActionResult> Post(Textbook textbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _textbookRepository.AddAsync(textbook);
            return Ok();
        }
    }
}
