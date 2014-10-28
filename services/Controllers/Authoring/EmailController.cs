using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using CampusNext.Communication;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;
using Newtonsoft.Json;

namespace CampusNext.Services.Controllers.Authoring
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class EmailController : ApiController
    {
        // POST: api/Email
        [ResponseType(typeof (EmailOption))]
        public async Task<IHttpActionResult> PostTextbook(EmailOption emailOption)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var email = await new ProfileRepository().GetEmailBy(emailOption.CategoryName, emailOption.ItemId);
            if (string.IsNullOrWhiteSpace(email))
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("The person you are trying to send message, does not have email in profile."),
                    ReasonPhrase = "Email empty"
                };

                throw new HttpResponseException(resp);
            }
            emailOption.ToEmail = email;
            var messageRepository = new MessageRepository();
            await messageRepository.AddAsync(JsonConvert.SerializeObject(emailOption));
            return Ok(emailOption);
        }
    }
}
