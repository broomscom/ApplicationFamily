// <copyright file="ConfigHubAuthorize.cs" company="Broomscom.com">Copyright (c) Broomscom.com</copyright>

namespace CFG.Hub.Attributes
{
    using System.Configuration;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using System.Web.Http.Controllers;    
    using Docker.Models;
    /// <summary>
    /// Authorization attribute
    /// </summary>
    /// <seealso cref="System.Web.Http.AuthorizeAttribute" />
    public class ConfigHubAuthorize : AuthorizeAttribute
    {
        /// <summary>
        /// Indicates whether the specified transaction is authorized.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        /// <returns>
        /// true if the transaction is authorized; otherwise, false.
        /// </returns>
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

        /// <summary>
        /// Processes requests that fail authorization.
        /// </summary>
        /// <param name="actionContext">The context.</param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            // Report unauthorized
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.OK, 
                new ServiceResponse()
            {
                Type = ResponseType.Error,
                Message = "Authorization failed for configuration hub",
                Payload = null
            });
        }
    }
}