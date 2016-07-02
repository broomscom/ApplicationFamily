// <copyright file="Global.asax.cs" company="Broomscom.com">Copyright (c) Broomscom.com</copyright>

namespace CFG.Hub
{
    using System.Web.Http;

    /// <summary>
    /// Application starting point
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application start.
        /// </summary>
        protected void Application_Start()
        {
            // Kick off
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
