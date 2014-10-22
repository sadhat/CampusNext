using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using CampusNext.Entity;
using CampusNext.Services.Attributes;

namespace CampusNext.Services.Controllers.Authoring
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [FacebookTokenValidation]
    public abstract class ApiControllerWithFacebookAuthorization : ApiController
    {
        public Profile Profile
        {
            get
            {
                var campusNextPrincipal = User as CampusNextPrincipal;
                if (campusNextPrincipal == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                if (campusNextPrincipal.Profile == null)
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                if (String.IsNullOrWhiteSpace(campusNextPrincipal.Profile.CampusCode))
                {
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                }

                return campusNextPrincipal.Profile;
            }
        }
    }
}