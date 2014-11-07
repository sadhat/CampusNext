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
    public class RentalController : ApiControllerWithFacebookAuthorization
    {
        private readonly RentalRepository _databaseRepository;
        private readonly IAzureSearchRepository _azureSearchRepository;
        public RentalController()
        {
            _databaseRepository = new RentalRepository();
            _azureSearchRepository = new AzureSearchRentalRepository();
        }

        [EnableQuery]
        // GET: api/Rentals
        public async Task<IQueryable<Rental>> GetRentals()
        {
            return await _databaseRepository.GetAllFor(User.Identity.Name);
        }

        // GET: api/Rental/5
        [ResponseType(typeof(Rental))]
        public async Task<IHttpActionResult> GetRental(int id)
        {
            var rental = await _databaseRepository.Get(id);
            if (rental == null)
            {
                return NotFound();
            }

            return Ok(rental);
        }

        // PUT: api/Rental/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutRental(int id, Rental rental)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rental.Id)
            {
                return BadRequest();
            }


            rental.CampusCode = Profile.CampusCode;
            rental.UserId = Profile.UserId;

            
            var tasks = new List<Task>
            {
                _databaseRepository.SaveAsync(rental),
                //_azureSearchRepository.UpdateAsync(rental)
            };

            await Task.WhenAll(tasks);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Rental
        [ResponseType(typeof(Rental))]
        public async Task<IHttpActionResult> PostRental(Rental rental)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            rental.UserId = User.Identity.Name;
            rental.CreatedDate = rental.ModifiedDate = DateTime.Now.Date;
           
            var tasks = new List<Task>
            {
                _databaseRepository.AddAsync(rental),
                //_azureSearchRepository.AddAsync(rental)
            };

            await Task.WhenAll(tasks);

            return CreatedAtRoute("DefaultApi", new { id = rental.Id }, rental);
        }

        // DELETE: api/Rental/5
        [ResponseType(typeof(Rental))]
        public async Task<IHttpActionResult> DeleteRental(int id)
        {
            var rental = new Rental {Id = id};


            var tasks = new List<Task>
            {
                _databaseRepository.DeleteAsync(rental),
                //_azureSearchRepository.DeleteAsync(rental)
            };

            await Task.WhenAll(tasks);

            return Ok(rental);
        }      
    }
}