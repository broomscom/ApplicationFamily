using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CFG.Hub.Attributes;
using Newtonsoft.Json;

namespace CFG.Hub.Controllers
{
    public class DockController : ApiController
    {
        [Route("Dock/Ping"), HttpGet, ConfigHubAuthorize]
        public HttpResponseMessage Ping()
        {            
            return Request.CreateResponse(HttpStatusCode.OK, JsonConvert.SerializeObject(ActionContext.RequestContext.Principal));
        }
    }
}
