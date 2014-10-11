using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
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
            dynamic result = client.Get("me", new { fields = "name,id" });
            string id = result.id;
            string[] rolesArray = { };
            IPrincipal principal = new GenericPrincipal(new GenericIdentity(id, "facebook"), rolesArray);
            System.Threading.Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;
        }
    }
}