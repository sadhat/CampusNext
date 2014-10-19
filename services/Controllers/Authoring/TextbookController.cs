using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.OData;
using CampusNext.AzureSearch.Repository;
using CampusNext.DataAccess;
using CampusNext.Entity;
using CampusNext.Services.Attributes;

namespace CampusNext.Services.Controllers.Authoring
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [FacebookTokenValidation]
    public class TextbookController : ApiController
    {
        
        [EnableQuery]
        // GET: api/Textbooks
        public async Task<IQueryable<Textbook>> GetTextbooks()
        {
            return await new TextbookRepository().GetAllFor(User.Identity.Name);
        }

        // GET: api/Textbooks/5
        [ResponseType(typeof(Textbook))]
        public async Task<IHttpActionResult> GetTextbook(int id)
        {
            Textbook textbook = await new TextbookRepository().Get(id);
            if (textbook == null)
            {
                return NotFound();
            }

            return Ok(textbook);
        }

        // PUT: api/Textbooks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTextbook(int id, Textbook textbook)
        {
            
            string userId = User.Identity.Name;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != textbook.Id)
            {
                return BadRequest();
            }

            textbook.UserId = userId;

            var textbookRepository = new TextbookRepository();
            await textbookRepository.SaveAsync(textbook);
            var azureSearchTextbookRepository = new AzureSearchTextbookRepository();
            azureSearchTextbookRepository.Update(textbook);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Textbooks
        [ResponseType(typeof(Textbook))]
        public async Task<IHttpActionResult> PostTextbook(Textbook textbook)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            textbook.UserId = User.Identity.Name;
            textbook.CreatedDate = textbook.ModifiedDate = DateTime.Now.Date;

            await new TextbookRepository().AddAsync(textbook);
            var azureSearchTextbookRepository = new AzureSearchTextbookRepository();
            azureSearchTextbookRepository.Add(textbook);

            return CreatedAtRoute("DefaultApi", new { id = textbook.Id }, textbook);
        }

        // DELETE: api/Textbooks/5
        [ResponseType(typeof(Textbook))]
        public async Task<IHttpActionResult> DeleteTextbook(int id)
        {
            var textbook = new Textbook {Id = id};

            var textbookRepository = new TextbookRepository();

            await textbookRepository.DeleteAsync(textbook);

            return Ok(textbook);
        }

        
    }
}