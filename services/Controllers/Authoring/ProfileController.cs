using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.OData;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;
using CampusNext.Services.Attributes;

namespace CampusNext.Services.Controllers.Authoring
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [FacebookTokenValidation]
    public class ProfileController : ApiController
    {
        // GET: api/Profiles
        [EnableQuery]
        public async Task<IHttpActionResult> GetProfiles()
        {
            var userId = User.Identity.Name;
            var result = await new ProfileRepository().Get(userId);
            var profile = result.FirstOrDefault();
            if (profile == null)
            {
                return Ok();
            }

            return Ok(profile);
        }

        // POST: api/Textbooks
        [ResponseType(typeof (Profile))]
        public async Task<IHttpActionResult> PostTextbook(Profile newProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userId = User.Identity.Name;
            newProfile.UserId = userId;

            var repository = new ProfileRepository();
            var result = await repository.Get(userId);
            var profile = result.FirstOrDefault();
            if (profile == null)
            {
                await repository.AddAsync(newProfile);
            }
            else
            {
                await repository.SaveAsync(newProfile);
            }

            return Ok(newProfile);
        }
    }
}
