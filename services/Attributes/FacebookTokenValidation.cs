using System;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using CampusNext.DataAccess.Repository;
using CampusNext.Entity;
using Facebook;

namespace CampusNext.Services.Attributes
{
    public class FacebookTokenValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;
            var authorization = request.Headers.Authorization;
            if (authorization == null || String.IsNullOrWhiteSpace(authorization.Parameter))
            {
                throw new HttpResponseException(HttpStatusCode.Unauthorized);
            }

            if (authorization.Scheme != "Bearer")
            {
                throw new HttpResponseException(HttpStatusCode.Forbidden);
            }

            var accessToken = authorization.Parameter;

            var client = new FacebookClient(accessToken);
            dynamic result = client.Get("me", new {fields = "name,id"});
            string id = result.id;
            string[] rolesArray = {};
            IPrincipal principal = new CampusNextPrincipal(new GenericIdentity(id, "facebook"), rolesArray);
            System.Threading.Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
        }
    }

    public class CampusNextPrincipal : GenericPrincipal
    {
        private readonly Profile _profile;
        public CampusNextPrincipal(IIdentity identity, string[] roles) : base(identity, roles)
        {
            var profileRepository = new ProfileRepository();
            var result = profileRepository.Get(identity.Name).Result;
            _profile = result.FirstOrDefault();
        }

        public Profile Profile { get { return _profile; } }
    }
}