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
    using System.Linq;

    public class DockController : ApiController
    {
        #region Constants
        private const string PathAllowableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_. "; 
        #endregion

        #region State
        private static bool SendClientDebugMessage = false;
        #endregion

        #region Construction
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
        #endregion

        #region Diagnostics        
        [Route("Dock/Ping"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Read)]
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
        #endregion

        #region Publishing
        [Route("Dock/RegisterComponent"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Publish)]
        public HttpResponseMessage RegisterComponent(ComponentQuery usingQuery)
        {
            try
            {
                // Verify component name
                if (usingQuery.ComponentNamePattern == null)
                {
                    return GenerateExceptionResponse("Component name cannot be null");
                }

                // Verify legal path
                if (!VerifyPathIsLegal(usingQuery.ComponentNamePattern))
                {
                    return GenerateExceptionResponse("Illegal component name");
                }

                // Trim up
                TrimUpComponentQuery(usingQuery);                                

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Validation
                    if (context.CFGHub_SystemComponent.Where(item => item.Name == usingQuery.ComponentNamePattern).FirstOrDefault() != null)
                    {
                        return GenerateExceptionResponse("Component name '" + usingQuery.ComponentNamePattern + "' is already in use");
                    }
                    else
                    {
                        // Make key
                        Guid uid = Guid.NewGuid();

                        // Add
                        context.CFGHub_SystemComponent.Add(new CFGHub_SystemComponent()
                        {
                            ID = uid,
                            Name = usingQuery.ComponentNamePattern
                        });

                        // Store
                        context.SaveChanges();
                    }                    

                    // Response
                    return GenerateSuccessResponse("Publish succeeded", usingQuery);
                }
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Unexpected failure" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        [Route("Dock/DeleteComponent"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Publish)]
        public HttpResponseMessage DeleteComponent(ComponentQuery usingQuery)
        {
            try
            {
                // Verify component name
                if (usingQuery.ComponentNamePattern == null)
                {
                    return GenerateExceptionResponse("Component name cannot be null");
                }

                // Verify legal path
                if (!VerifyPathIsLegal(usingQuery.ComponentNamePattern))
                {
                    return GenerateExceptionResponse("Illegal component name");
                }

                // Trim up
                TrimUpComponentQuery(usingQuery);

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Validation
                    if (context.CFGHub_SystemComponent.Where(item => item.Name == usingQuery.ComponentNamePattern).FirstOrDefault() != null)
                    {
                        return GenerateExceptionResponse("Component name '" + usingQuery.ComponentNamePattern + "' is already in use");
                    }
                    else
                    {
                        // Make key
                        Guid uid = Guid.NewGuid();

                        // Add
                        context.CFGHub_SystemComponent.Add(new CFGHub_SystemComponent()
                        {
                            ID = uid,
                            Name = usingQuery.ComponentNamePattern
                        });

                        // Store
                        context.SaveChanges();
                    }

                    // Response
                    return GenerateSuccessResponse("Publish succeeded", usingQuery);
                }
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Unexpected failure" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }
        #endregion

        #region Reading        
        #endregion             

        #region Cores
        private HttpResponseMessage GenerateSuccessResponse(string message, object payload)
        {
            // Build and return
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new ServiceResponse()
                {
                    Type = ResponseType.Success,
                    Message = message,
                    Payload = payload
                }, "application/json");
        }
        private HttpResponseMessage GenerateExceptionResponse(string message)
        {
            // Build and return
            return Request.CreateResponse(
                HttpStatusCode.OK,
                new ServiceResponse()
                {
                    Type = ResponseType.Error,
                    Message = message,
                    Payload = null
                }, "application/json");
        }
        private void TrimUpComponentQuery(ComponentQuery query)
        {
            // Trim up name pattern
            if (query.ComponentNamePattern != null)
            {
                query.ComponentNamePattern = query.ComponentNamePattern.Trim();
            }

            // Trim up description pattern
            if (query.ComponentDescriptionPattern != null)
            {
                query.ComponentDescriptionPattern = query.ComponentDescriptionPattern.Trim();
            }
        }
        private bool VerifyPathIsLegal(string path)
        {
            // Check path for illegals
            if (path == null)
            {
                return true;
            }
            bool pathIsLegal = true;
            path.ToList().ForEach((char characterInPath) =>
            {
                if (!PathAllowableCharacters.Contains(characterInPath))
                {
                    pathIsLegal = false;
                }
            });
            return pathIsLegal;
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
                    // Resolve first atom
                    currentAtom = context.CFGHub_ConfigAtom
                        .Where(item => item.Name == componentName).FirstOrDefault();
                }
                else
                {
                    // Resolve first atom
                    currentAtom = context.CFGHub_ConfigAtom
                        .Where(item => item.ParentID == currentAtom.ID &&
                        item.Name == atomName).FirstOrDefault();

                    // Check atom missing
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
                // Build and return
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
                // Return
                return tipAtom;
            }            
        }        
        #endregion
    }
}
