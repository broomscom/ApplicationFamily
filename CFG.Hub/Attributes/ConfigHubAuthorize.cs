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

    public class ConfigHubAuthorize : AuthorizeAttribute
    {
        #region Enumeration
        public enum ConfigHubClaims { Unset, Read, Publish }
        #endregion

        #region State
        private ConfigHubClaims Claim = ConfigHubClaims.Unset;
        #endregion

        #region Authorization
        public ConfigHubAuthorize(ConfigHubClaims claim)
        {
            // Set the claim for authorize
            Claim = claim;
        }
        
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            // Ensure the token
            try
            {
                string tokenConfigName = Claim == ConfigHubClaims.Read ? "ReadToken" : "PublishToken";
                if (actionContext.Request.Headers.GetValues("token").FirstOrDefault() == ConfigurationManager.AppSettings[tokenConfigName])
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
            actionContext.Response = actionContext.Request.CreateResponse(
                HttpStatusCode.OK, 
                new ServiceResponse()
            {
                Type = ResponseType.Error,
                Message = "Authorization failed for configuration hub",
                Payload = null
            });
        }
        #endregion
    }
}