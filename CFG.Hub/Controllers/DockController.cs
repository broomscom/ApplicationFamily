// <copyright file="DockController.cs" company="DockController.com">Copyright (c) Broomscom.com</copyright>

namespace CFG.Hub.Controllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Attributes;
    using Docker.Models;
    using System;
    using System.Configuration;
    using System.Collections.Generic;
    using System.Linq;/// <summary>
                      /// Configuration hub endpoint controller.
                      /// </summary>
                      /// <seealso cref="System.Web.Http.ApiController" />
    public class DockController : ApiController
    {
        private static bool SendClientDebugMessage = false;

        public DockController()
        {
            // Resolve SendClientDebugMessages flag
            try
            {
                SendClientDebugMessage = bool.Parse(ConfigurationManager.AppSettings["SendClientDebugMessages"]);
            }
            catch
            {
                // Don't care
            }
        }

        /// <summary>
        /// Pings this instance.
        /// </summary>
        /// <returns>Pong Service Response</returns>
        [Route("Dock/Ping"), HttpGet, ConfigHubAuthorize]
        public HttpResponseMessage Ping()
        {
            // Return pong response
            try
            {
                return GenerateSuccessResponse("Pong", null);
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Unexpected failure" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        /// <summary>
        /// Resolves a configuration value (always string).
        /// </summary>
        /// <returns>Specified value (always string)</returns>
        [Route("Dock/Resolve"), HttpPost, ConfigHubAuthorize]
        public HttpResponseMessage Resolve(string path)
        {
            try
            {
                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Parse the path
                    CFGHub_SystemComponent component = null;
                    CFGHub_ConfigAtom configAtom = ResolveAtomOrBuild(path, context, out component);
                    context.SaveChanges();

                    // Respond
                    return GenerateSuccessResponse("Sending configuration atom '" + path + "'", configAtom.Value);
                }                
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Failed to find resolve path '" + path + "'" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        /// <summary>
        /// Publishes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        [Route("Dock/Publish"), HttpPost, ConfigHubAuthorize]
        public HttpResponseMessage Publish(string path, string value)
        {
            try
            {
                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Parse the path
                    CFGHub_SystemComponent component = null;
                    CFGHub_ConfigAtom configAtom = ResolveAtomOrBuild(path, context, out component);
                    configAtom.Value = value;
                    context.CFGHub_ConfigAtom.Add(configAtom);
                    context.SaveChanges();

                    // Response
                    return GenerateSuccessResponse("Publish succeeded", null);
                }                
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Failed to find publish path '" + path + "'" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        /// <summary>
        /// Lists the children for the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        [Route("Dock/ListChildren"), HttpPost, ConfigHubAuthorize]
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
        [Route("Dock/Find"), HttpPost, ConfigHubAuthorize]
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

        #region Cores
        private HttpResponseMessage GenerateSuccessResponse(string message, object payload)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new ServiceResponse()
                {
                    Type = ResponseType.Success,
                    Message = message,
                    Payload = payload
                });
        }
        private HttpResponseMessage GenerateExceptionResponse(string message)
        {
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new ServiceResponse()
                {
                    Type = ResponseType.Error,
                    Message = message,
                    Payload = null
                });
        }
        private CFGHub_ConfigAtom ResolveAtomOrBuild(string path, ConfigHubEFEntities context, out CFGHub_SystemComponent pathComponentResolve)
        {
            // Declare return atom
            CFGHub_ConfigAtom currentAtom = null;

            // Resolve component, tail and tip of path atom names
            List<string> trailAtomPath = path.Split('.').ToList();            
            string tipAtomName = trailAtomPath[trailAtomPath.Count - 1];
            trailAtomPath.RemoveAt(trailAtomPath.Count - 1);
            string componentName = trailAtomPath[0];
            trailAtomPath.RemoveAt(0);

            // Get the path component
            pathComponentResolve = context.CFGHub_SystemComponent.Where(item => item.Name == componentName).FirstOrDefault();

            // Resolve the tail atom            
            foreach(string atomName in trailAtomPath)
            {
                if (currentAtom == null)
                {
                    currentAtom = context.CFGHub_ConfigAtom
                        .Where(item => item.Name == componentName).FirstOrDefault();
                }
                else
                {
                    currentAtom = context.CFGHub_ConfigAtom
                        .Where(item => item.ParentID == currentAtom.ID &&
                        item.Name == atomName).FirstOrDefault();

                    if (currentAtom == null)
                    {
                        // Must have the tail to store
                        throw new Exception("Invalid atom '" + atomName + "'");
                    }
                }
            }            

            // Resolve if exists or create if does not
            CFGHub_ConfigAtom tipAtom = context.CFGHub_ConfigAtom
                        .Where(item => item.ParentID == currentAtom.ID &&
                        item.Name == tipAtomName).FirstOrDefault();
            if (tipAtom == null)
            {
                tipAtom = new CFGHub_ConfigAtom()
                {
                    ComponentID = pathComponentResolve.ID,
                    ParentID = currentAtom.ID,
                    ID = Guid.NewGuid(),
                    Name = tipAtomName,                                  
                };                              
                return tipAtom;
            }                
            else
            {
                return tipAtom;
            }            
        }        
        #endregion
    }
}
