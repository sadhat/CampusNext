﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using CampusNext.AzureSearch.Repository;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;

namespace CampusNext.Services.Controllers.Authoring
{
    public class TextbookController : ApiControllerWithFacebookAuthorization
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != textbook.Id)
            {
                return BadRequest();
            }
            

            textbook.CampusCode = Profile.CampusCode;
            textbook.UserId = Profile.UserId;

            var textbookRepository = new TextbookRepository();
            var azureSearchTextbookRepository = new AzureSearchTextbookRepository();
            
            var tasks = new List<Task>
            {
                textbookRepository.SaveAsync(textbook),
                azureSearchTextbookRepository.UpdateAsync(textbook)
            };

            await Task.WhenAll(tasks);

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
            var textbookRepository = new TextbookRepository();
            var azureSearchTextbookRepository = new AzureSearchTextbookRepository();

            var tasks = new List<Task>
            {
                textbookRepository.AddAsync(textbook),
                azureSearchTextbookRepository.AddAsync(textbook)
            };

            await Task.WhenAll(tasks);

            return CreatedAtRoute("DefaultApi", new { id = textbook.Id }, textbook);
        }

        // DELETE: api/Textbooks/5
        [ResponseType(typeof(Textbook))]
        public async Task<IHttpActionResult> DeleteTextbook(int id)
        {
            var textbook = new Textbook {Id = id};

            var textbookRepository = new TextbookRepository();
            var azureSearchTextbookRepository = new AzureSearchTextbookRepository();
            
            var tasks = new List<Task>
            {
                textbookRepository.DeleteAsync(textbook),
                azureSearchTextbookRepository.DeleteAsync(textbook)
            };

            await Task.WhenAll(tasks);

            return Ok(textbook);
        }      
    }
}