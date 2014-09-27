using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.OData;
using CampusNext.Services.BusinessLayer;
using CampusNext.Services.Models;

namespace CampusNext.Services.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class TextbookSearchController : ODataController
    {
        private readonly ITextbookRepository _textbookRepository;
        //Read Data from list    

        public TextbookSearchController(ITextbookRepository textbookRepository)
        {
            _textbookRepository = textbookRepository;
        }
        public IQueryable<Textbook> Get([FromUri] TextbookSearchOption searchOption)    

        {    
            return _textbookRepository.All(searchOption);    
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
