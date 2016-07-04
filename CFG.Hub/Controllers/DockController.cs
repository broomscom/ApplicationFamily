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

        #region Components
        [Route("Dock/RegisterComponent"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Publish)]
        public HttpResponseMessage RegisterComponent(ConfigHubQuery usingQuery)
        {
            try
            {
                // Shore up                
                if (ShoreUpComponentQuery(usingQuery, false) != null)
                {
                    return ShoreUpComponentQuery(usingQuery, false);
                }

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Validation
                    if (context.CFGHub_SystemComponent.Where(item => item.Name == usingQuery.NamePattern).FirstOrDefault() != null)
                    {
                        return GenerateExceptionResponse("Component name '" + usingQuery.NamePattern + "' is already in use");
                    }
                    else
                    {
                        // Make key
                        Guid uid = Guid.NewGuid();

                        // Add
                        context.CFGHub_SystemComponent.Add(new CFGHub_SystemComponent()
                        {
                            ID = uid,
                            Name = usingQuery.NamePattern
                        });

                        // Store
                        context.SaveChanges();
                    }                    

                    // Response
                    return GenerateSuccessResponse("Publish succeeded", null);
                }
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Unexpected failure" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        [Route("Dock/DeleteComponent"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Publish)]
        public HttpResponseMessage DeleteComponent(ConfigHubQuery usingQuery)
        {
            try
            {
                // Shore up                
                if (ShoreUpComponentQuery(usingQuery, false) != null)
                {
                    return ShoreUpComponentQuery(usingQuery, false);
                }

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Grab component
                    CFGHub_SystemComponent component = context.CFGHub_SystemComponent.Where(item => item.Name == usingQuery.NamePattern).FirstOrDefault();
                    
                    // Validation
                    if (component == null)
                    {
                        return GenerateExceptionResponse("Component name '" + usingQuery.NamePattern + "' does not exist");
                    }
                    else
                    {
                        // Ensure no configuration atoms are attached
                        int atomCount = component.CFGHub_ConfigAtom.Count;
                        if (atomCount != 0)
                        {
                            return GenerateExceptionResponse("Component name '" + usingQuery.NamePattern + "' has (" + atomCount + ") atoms, which must be removed before the component can be deleted");
                        }
                        else
                        {
                            // Add
                            context.CFGHub_SystemComponent.Remove(component);

                            // Store
                            context.SaveChanges();
                        }                        
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

        [Route("Dock/ListComponents"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Read)]
        public HttpResponseMessage ListComponents(ConfigHubQuery usingQuery)
        {
            try
            {
                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Empty set check
                    if (context.CFGHub_SystemComponent.Count() == 0)
                    {
                        return GenerateExceptionResponse("No components have been added yet");
                    }

                    // Get sorted component list and return
                    List<string> listing = context.CFGHub_SystemComponent.Select(item => item.Name).ToList();
                    listing.Sort();
                    return GenerateSuccessResponse("Sending component list", listing);
                }
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Unexpected failure" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }
        #endregion

        #region Atoms
        [Route("Dock/PublishConfigurationAtom"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Publish)]
        public HttpResponseMessage PublishConfigurationAtom(ConfigHubQuery usingQuery)
        {
            try
            {
                // Shore up                
                if (ShoreUpComponentQuery(usingQuery, false) != null)
                {
                    return ShoreUpComponentQuery(usingQuery, false);
                }

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    try
                    {
                        // Resolve, build or fail
                        CFGHub_SystemComponent component = null;
                        CFGHub_ConfigAtom atom = ResolveAtomOrBuild(usingQuery.NamePattern, context, out component, true);

                        // Store value on it
                        atom.Value = usingQuery.ValuePattern;
                        context.SaveChanges();

                        // Notify success
                        return GenerateSuccessResponse("Updated '" + usingQuery.NamePattern + "' successfully", null);
                    }
                    catch(Exception err)
                    {
                        return GenerateExceptionResponse("Cannot build without a proper tail - '" + usingQuery.NamePattern + "'" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
                    }
                }
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Unexpected failure" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        [Route("Dock/DeleteConfigurationAtom"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Publish)]
        public HttpResponseMessage DeleteConfigurationAtom(ConfigHubQuery usingQuery)
        {
            try
            {
                // Shore up                
                if (ShoreUpComponentQuery(usingQuery, false) != null)
                {
                    return ShoreUpComponentQuery(usingQuery, false);
                }

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    try
                    {
                        // Resolve, build or fail
                        CFGHub_SystemComponent component = null;
                        CFGHub_ConfigAtom atom = ResolveAtomOrBuild(usingQuery.NamePattern, context, out component, false);

                        // Check for children
                        if (atom.Children.Count() != 0)
                        {
                            return GenerateExceptionResponse("Atom '" + usingQuery.NamePattern + "' cannot be deleted before its children");
                        }

                        // Store value on it
                        context.CFGHub_ConfigAtom.Remove(atom);
                        context.SaveChanges();

                        // Notify success
                        return GenerateSuccessResponse("Deleted '" + usingQuery.NamePattern + "' successfully", null);
                    }
                    catch (Exception err)
                    {
                        return GenerateExceptionResponse("Atom '" + usingQuery.NamePattern + "' does not exist for deletion" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
                    }
                }
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Unexpected failure" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        [Route("Dock/ListSubAtoms"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Read)]
        public HttpResponseMessage ListSubAtoms(ConfigHubQuery usingQuery)
        {
            try
            {
                // Shore up                
                if (ShoreUpComponentQuery(usingQuery, false) != null)
                {
                    return ShoreUpComponentQuery(usingQuery, false);
                }

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Resolve, build or fail
                    CFGHub_SystemComponent component = null;
                    CFGHub_ConfigAtom atom = ResolveAtomOrBuild(usingQuery.NamePattern, context, out component, false);

                    // Check no component
                    if (component == null)
                    {
                        return GenerateExceptionResponse("Failed to resolve '" + usingQuery.NamePattern + "'");
                    }
                    
                    // Return listing
                    if (atom == null)
                    {
                        // Get sorted component list and return
                        List<string> listing = context.CFGHub_ConfigAtom
                            .Where(item => item.ParentID == null && item.ComponentID == component.ID)
                            .Select(item => item.Name).ToList();
                        listing.Sort();
                        return GenerateSuccessResponse("Sending sub atoms of '" + usingQuery.ValuePattern + "'", listing);
                    }
                    else
                    {
                        // Check for children
                        if (atom.Children.Count() == 0)
                        {
                            return GenerateExceptionResponse("Atom '" + usingQuery.NamePattern + "' has no children to list");
                        }
                        else
                        {
                            // Get sorted component list and return
                            List<string> listing = context.CFGHub_ConfigAtom
                                .Where(item => item.ParentID == atom.ID && item.ComponentID == component.ID)
                                .Select(item => item.Name).ToList();
                            listing.Sort();
                            return GenerateSuccessResponse("Sending sub atoms of '" + usingQuery.ValuePattern + "'", listing);
                        }
                    }
                }                                
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Atom '" + usingQuery.NamePattern + "' does not exist" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }

        [Route("Dock/ResolveAtomAsString"), HttpPost, ConfigHubAuthorize(ConfigHubAuthorize.ConfigHubClaims.Read)]
        public HttpResponseMessage ResolveAtomAsString(ConfigHubQuery usingQuery)
        {
            try
            {
                // Shore up                
                if (ShoreUpComponentQuery(usingQuery, false) != null)
                {
                    return ShoreUpComponentQuery(usingQuery, false);
                }

                // Build context
                using (ConfigHubEFEntities context = new ConfigHubEFEntities())
                {
                    // Resolve, build or fail
                    CFGHub_SystemComponent component = null;
                    CFGHub_ConfigAtom atom = ResolveAtomOrBuild(usingQuery.NamePattern, context, out component, false);

                    // Return value
                    return GenerateSuccessResponse("Sending value of atom '" + usingQuery.NamePattern + "'", atom.Value);
                }
            }
            catch (Exception err)
            {
                return GenerateExceptionResponse("Atom '" + usingQuery.NamePattern + "' does not exist" + (SendClientDebugMessage ? " - " + err.ToString() : ""));
            }
        }
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
        private HttpResponseMessage ShoreUpComponentQuery(ConfigHubQuery query, bool allowStars)
        {
            // Trim up name pattern
            if (query.NamePattern != null)
            {
                query.NamePattern = query.NamePattern.Trim();
            }       

            // Verify component name
            if (query.NamePattern == null)
            {
                return GenerateExceptionResponse("Component name cannot be null");
            }

            // Verify legal path
            if (!VerifyPathIsLegal(query.NamePattern, allowStars))
            {
                return GenerateExceptionResponse("Illegal path name");
            }

            // Return null for success
            return null;
        }
        private bool VerifyPathIsLegal(string path, bool allowStars)
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
            if (!allowStars && path.Contains("*"))
            {
                pathIsLegal = false;
            }
            return pathIsLegal;
        }

        private CFGHub_ConfigAtom ResolveAtomOrBuild(string path, ConfigHubEFEntities context, out CFGHub_SystemComponent pathComponentResolve, bool buildIfNotFound)
        {
            // Declare return atom
            CFGHub_ConfigAtom currentAtom = null;

            // Resolve component, tail and tip of path atom names            
            List<string> trailAtomPath = path.Split('.').ToList();
            
            // Resolve only component
            if (trailAtomPath.Count == 1)
            {
                string lookupName = trailAtomPath[0];
                pathComponentResolve = context.CFGHub_SystemComponent.Where(item => item.Name == lookupName).FirstOrDefault();
                return null;
            }
                        
            string tipAtomName = trailAtomPath[trailAtomPath.Count - 1];
            trailAtomPath.RemoveAt(trailAtomPath.Count - 1);
            string componentName = trailAtomPath[0];
            trailAtomPath.RemoveAt(0);

            // Get the path component
            pathComponentResolve = context.CFGHub_SystemComponent.Where(item => item.Name == componentName).FirstOrDefault();
            Guid captureComponentGuid = pathComponentResolve.ID;

            // Resolve the tail atom            
            foreach(string atomName in trailAtomPath)
            {
                if (currentAtom == null)
                {
                    // Resolve first atom
                    currentAtom = context.CFGHub_ConfigAtom
                        .Where(item => item.Name == atomName).FirstOrDefault();

                    // Check atom missing
                    if (currentAtom == null)
                    {
                        // Must have the tail to store
                        throw new Exception("Invalid atom '" + atomName + "'");
                    }
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
            CFGHub_ConfigAtom tipAtom = null;
            if (currentAtom == null)
            {
                tipAtom = context.CFGHub_ConfigAtom
                    .Where(item => item.ComponentID == captureComponentGuid && item.ParentID == null &&
                    item.Name == tipAtomName).FirstOrDefault();
            }
            else
            {
                tipAtom = context.CFGHub_ConfigAtom
                    .Where(item => item.ParentID == currentAtom.ID &&
                    item.Name == tipAtomName).FirstOrDefault();
            }
            if (tipAtom == null)
            {
                if (buildIfNotFound)
                {
                    // Build and return
                    tipAtom = new CFGHub_ConfigAtom()
                    {
                        ComponentID = pathComponentResolve.ID,
                        ID = Guid.NewGuid(),
                        Name = tipAtomName,
                    };
                    if (currentAtom != null)
                    {
                        tipAtom.ParentID = currentAtom.ID;
                    }
                    context.CFGHub_ConfigAtom.Add(tipAtom);
                    return tipAtom;
                }
                else
                {
                    // Not found
                    return null;
                }
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
