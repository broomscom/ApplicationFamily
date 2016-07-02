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

namespace CFG.Hub.Attributes
{
    public class ConfigHubAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            return true;
        }
    }
}