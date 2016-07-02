// <copyright file="DockController.cs" company="DockController.com">Copyright (c) Broomscom.com</copyright>

namespace CFG.Hub.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Attributes;
    using Models;

    /// <summary>
    /// Configuration hub endpoint controller
    /// </summary>
    /// <seealso cref="System.Web.Http.ApiController" />
    public class DockController : ApiController
    {
        /// <summary>
        /// Pings this instance.
        /// </summary>
        /// <returns>Pong Service Response</returns>
        [Route("Dock/Ping"), HttpGet, ConfigHubAuthorize]
        public HttpResponseMessage Ping()
        {            
            // Return pong response
            return Request.CreateResponse(
                HttpStatusCode.OK, 
                new ServiceResponse()
            {
                Type = ResponseType.Success,
                Message = "Pong",
                Payload = null
            });
        }
    }
}
