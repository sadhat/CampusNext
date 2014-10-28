using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.OData;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;

namespace CampusNext.Services.Controllers.Authoring
{
    public class ProfileController : ApiControllerWithFacebookAuthorization
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
