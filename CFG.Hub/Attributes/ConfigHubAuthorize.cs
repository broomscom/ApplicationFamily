using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Configuration;
using CFG.Hub.Models;

namespace CFG.Hub.Attributes
{
    public class ConfigHubAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            // Ensure the token
            try
            {
                if (actionContext.Request.Headers.GetValues("token").FirstOrDefault() == ConfigurationManager.AppSettings["AuthorizationToken"])
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            // Report unauthorized
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, new ServiceResponse()
            {
                Type = ResponseType.Error,
                Message = "Authorization failed for configuration hub",
                Payload = null
            });
        }
    }
}