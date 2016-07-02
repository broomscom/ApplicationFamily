// <copyright file="WebApiConfig.cs" company="Broomscom.com">Copyright (c) Broomscom.com</copyright>

namespace CFG.Hub
{
    using System.Web.Http;

    /// <summary>
    /// Standard wire up
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Registers endpoints.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public static void Register(HttpConfiguration configuration)
        {
            // Map all routes
            configuration.MapHttpAttributeRoutes();            
        }
    }
}
