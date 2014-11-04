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
    public class ShareRideController : ApiControllerWithFacebookAuthorization
    {
        private readonly ShareRideRepository _databaseRepository;
        private readonly IAzureSearchRepository _azureSearchRepository;
        public ShareRideController()
        {
            _databaseRepository = new ShareRideRepository();
            _azureSearchRepository = new AzureSearchShareRideRepository();
        }

        [EnableQuery]
        // GET: api/ShareRides
        public async Task<IQueryable<ShareRide>> GetShareRides()
        {
            return await _databaseRepository.GetAllFor(User.Identity.Name);
        }

        // GET: api/ShareRide/5
        [ResponseType(typeof(ShareRide))]
        public async Task<IHttpActionResult> GetShareRide(int id)
        {
            ShareRide shareRide = await _databaseRepository.Get(id);
            if (shareRide == null)
            {
                return NotFound();
            }

            return Ok(shareRide);
        }

        // PUT: api/shareRide/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutShareRide(int id, ShareRide shareRide)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != shareRide.Id)
            {
                return BadRequest();
            }
            

            shareRide.CampusCode = Profile.CampusCode;
            shareRide.UserId = Profile.UserId;

            
            var tasks = new List<Task>
            {
                _databaseRepository.SaveAsync(shareRide),
                _azureSearchRepository.UpdateAsync(shareRide)
            };

            await Task.WhenAll(tasks);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ShareRide
        [ResponseType(typeof(ShareRide))]
        public async Task<IHttpActionResult> PostShareRide(ShareRide shareRide)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            shareRide.UserId = User.Identity.Name;
            shareRide.CreatedDate = shareRide.ModifiedDate = DateTime.Now.Date;
           
            var tasks = new List<Task>
            {
                _databaseRepository.AddAsync(shareRide),
                _azureSearchRepository.AddAsync(shareRide)
            };

            await Task.WhenAll(tasks);

            return CreatedAtRoute("DefaultApi", new { id = shareRide.Id }, shareRide);
        }

        // DELETE: api/ShareRide/5
        [ResponseType(typeof(ShareRide))]
        public async Task<IHttpActionResult> DeleteShareRide(int id)
        {
            var shareRide = new ShareRide {Id = id};


            var tasks = new List<Task>
            {
                _databaseRepository.DeleteAsync(shareRide),
                _azureSearchRepository.DeleteAsync(shareRide)
            };

            await Task.WhenAll(tasks);

            return Ok(shareRide);
        }      
    }
}