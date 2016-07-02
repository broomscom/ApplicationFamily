using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CFG.Hub
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            // Map all routes
            config.MapHttpAttributeRoutes();            
        }
    }
}
