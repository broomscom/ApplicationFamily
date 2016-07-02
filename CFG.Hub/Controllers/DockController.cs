using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CFG.Hub.Controllers
{
    public class DockController : ApiController
    {
        [Route("Dock/Ping"), HttpGet]
        public HttpResponseMessage Ping()
        {
            return Request.CreateResponse(HttpStatusCode.OK, new object());
        }
    }
}
