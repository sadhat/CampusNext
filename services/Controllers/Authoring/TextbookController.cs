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
using CampusNext.DataAccess;
using CampusNext.Services.Attributes;
using Facebook;

namespace CampusNext.Services.Controllers.Authoring
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [FacebookTokenValidation]
    public class TextbookController : ApiController
    {
        private CampusNextContext db = new CampusNextContext();

        [EnableQuery]
        // GET: api/Textbooks
        public IQueryable<Textbook> GetTextbooks()
        {
            return new TextbookRepository().GetTextbook(User.Identity.Name);
        }

        // GET: api/Textbooks/5
        [ResponseType(typeof(Textbook))]
        public async Task<IHttpActionResult> GetTextbook(int id)
        {
            var accessToken = Request.Headers.GetValues("accessToken").FirstOrDefault();
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                return BadRequest("Access token is missing");
            }
            Textbook textbook = await db.Textbooks.FindAsync(id);
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
            if (!Request.Headers.Contains("accessToken"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            var accessToken = Request.Headers.GetValues("accessToken").FirstOrDefault();
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me", new { fields = "name,id" });
            string userId = result.id;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != textbook.Id)
            {
                return BadRequest();
            }

            textbook.UserId = userId;

            db.Entry(textbook).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TextbookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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

            if (!Request.Headers.Contains("accessToken"))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }
            var accessToken = Request.Headers.GetValues("accessToken").FirstOrDefault();
            if (String.IsNullOrWhiteSpace(accessToken))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me", new { fields = "name,id" });
            string userId = result.id;

            textbook.UserId = userId;
            textbook.CreatedDate = textbook.ModifiedDate = DateTime.Now.Date;

            db.Textbooks.Add(textbook);

            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = textbook.Id }, textbook);
        }

        // DELETE: api/Textbooks/5
        [ResponseType(typeof(Textbook))]
        public async Task<IHttpActionResult> DeleteTextbook(int id)
        {
            Textbook textbook = await db.Textbooks.FindAsync(id);
            if (textbook == null)
            {
                return NotFound();
            }

            db.Textbooks.Remove(textbook);
            await db.SaveChangesAsync();

            return Ok(textbook);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TextbookExists(int id)
        {
            return db.Textbooks.Count(e => e.Id == id) > 0;
        }
    }
}