// <copyright file="DockController.cs" company="DockController.com">Copyright (c) Broomscom.com</copyright>

namespace CFG.Hub.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Attributes;
    using Docker.Models;
    /// <summary>
    /// Configuration hub endpoint controller.
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

        /// <summary>
        /// Resolves a configuration value (always string).
        /// </summary>
        /// <returns>Specified value</returns>
        [Route("Dock/Ping"), HttpPost, ConfigHubAuthorize]
        public HttpResponseMessage Resolve(string path)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new ServiceResponse()
                {
                    Type = ResponseType.Success,
                    Message = "Pong",
                    Payload = null
                });
        }

        /// <summary>
        /// Publishes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Route("Dock/Ping"), HttpPost, ConfigHubAuthorize]
        public HttpResponseMessage Publish(string path, string value)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new ServiceResponse()
                {
                    Type = ResponseType.Success,
                    Message = "Pong",
                    Payload = null
                });
        }

        /// <summary>
        /// Lists the children for the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        [Route("Dock/Ping"), HttpPost, ConfigHubAuthorize]
        public HttpResponseMessage ListChildren(string path)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new ServiceResponse()
                {
                    Type = ResponseType.Success,
                    Message = "Pong",
                    Payload = null
                });
        }

        /// <summary>
        /// Lists the configuration atoms for the specified pattern (* is wildcard).
        /// </summary>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        [Route("Dock/Ping"), HttpPost, ConfigHubAuthorize]
        public HttpResponseMessage Find(string pattern)
        {
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
