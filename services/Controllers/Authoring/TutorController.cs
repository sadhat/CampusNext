using System;
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
    public class TutorController : ApiControllerWithFacebookAuthorization
    {
        private readonly FindTutorRepository _databaseRepository;
        private readonly IAzureSearchRepository _azureSearchRepository;
        public TutorController()
        {
            _databaseRepository = new FindTutorRepository();
            _azureSearchRepository = new AzureSearchFindTutorRepository();
        }

        [EnableQuery]
        // GET: api/Tutors
        public async Task<IQueryable<FindTutor>> GetTutors()
        {
            return await _databaseRepository.GetAllFor(User.Identity.Name);
        }

        // GET: api/Tutor/5
        [ResponseType(typeof(FindTutor))]
        public async Task<IHttpActionResult> GetTutor(int id)
        {
            FindTutor tutor = await _databaseRepository.Get(id);
            if (tutor == null)
            {
                return NotFound();
            }

            return Ok(tutor);
        }

        // PUT: api/tutor/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTutor(int id, FindTutor tutor)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tutor.Id)
            {
                return BadRequest();
            }
            

            tutor.CampusCode = Profile.CampusCode;
            tutor.UserId = Profile.UserId;

            
            var tasks = new List<Task>
            {
                _databaseRepository.SaveAsync(tutor),
                _azureSearchRepository.UpdateAsync(tutor)
            };

            await Task.WhenAll(tasks);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Tutor
        [ResponseType(typeof(FindTutor))]
        public async Task<IHttpActionResult> PostTutor(FindTutor tutor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tutor.UserId = User.Identity.Name;
            tutor.CreatedDate = tutor.ModifiedDate = DateTime.Now.Date;
           
            var tasks = new List<Task>
            {
                _databaseRepository.AddAsync(tutor),
                _azureSearchRepository.AddAsync(tutor)
            };

            await Task.WhenAll(tasks);

            return CreatedAtRoute("DefaultApi", new { id = tutor.Id }, tutor);
        }

        // DELETE: api/Tutor/5
        [ResponseType(typeof(FindTutor))]
        public async Task<IHttpActionResult> DeleteTutor(int id)
        {
            var tutor = new FindTutor {Id = id};


            var tasks = new List<Task>
            {
                _databaseRepository.DeleteAsync(tutor),
                _azureSearchRepository.DeleteAsync(tutor)
            };

            await Task.WhenAll(tasks);

            return Ok(tutor);
        }      
    }
}