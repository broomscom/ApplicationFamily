﻿using CFG.Docker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CFG.Docker
{
    public class StandardConfigHubDocker : IDocker
    {
        #region Constants
        private const string NetworkFailureBaseMessage = "Network error for specified URI";
        #endregion

        #region State
        private string BaseURI = null;
        private string PublishToken = null;
        private string ReadToken = null;
        #endregion

        #region Setup
        public void Setup(string serverNameAndPort, string readToken, string publishToken, bool useHttps)
        {
            // Validate
            if (serverNameAndPort == null)
            {
                throw new CFGDockerException("Server and port cannot be null", null);
            }
            else if (readToken == null && publishToken == null)
            {
                throw new CFGDockerException("One or two tokens must be specified", null);
            }

            // Chop off http/https if its there
            serverNameAndPort = serverNameAndPort.ToLower();
            if (serverNameAndPort.StartsWith("http://"))
            {
                serverNameAndPort = serverNameAndPort.Replace("http://", "");
            }
            else if(serverNameAndPort.StartsWith("https://"))
            {
                serverNameAndPort = serverNameAndPort.Replace("https://", "");
            }

            // Remove ending slash, if there
            if (serverNameAndPort.EndsWith("/"))
            {
                serverNameAndPort = serverNameAndPort.Substring(0, serverNameAndPort.Length - 1);
            }

            // Build base URI
            BaseURI = (useHttps ? "https://" : "http://") + serverNameAndPort + "/Dock/";

            // Setup tokens
            PublishToken = publishToken;
            ReadToken = readToken;
        }
        #endregion

        #region Component Merchant        
        public void RegisterComponent(string componentName)
        {
            // Execute
            string stringOutput = null;
            ExecuteConfigQuery("RegisterComponent", componentName, null, PublishToken, out stringOutput);            
        }
        public void DeleteComponent(string componentName)
        {
            // Execute
            string stringOutput = null;
            ExecuteConfigQuery("DeleteComponent", componentName, null, PublishToken, out stringOutput);
        }
        public List<string> ListComponents()
        {
            // Execute
            string stringOutput = null;
            return ExecuteConfigQuery("ListComponents", null, null, ReadToken, out stringOutput);
        }
        #endregion        

        #region Atom Merchant
        public void PublishConfigurationAtom(string path, string value)
        {
            // Execute
            string stringOutput = null;
            ExecuteConfigQuery("PublishConfigurationAtom", path, value, PublishToken, out stringOutput);
        }
        public void DeleteConfigurationAtom(string atomPath)
        {
            // Execute
            string stringOutput = null;
            ExecuteConfigQuery("DeleteConfigurationAtom", atomPath, null, PublishToken, out stringOutput);
        }
        public List<string> ListSubAtoms(string atomPath)
        {
            // Execute
            string stringOutput = null;
            return ExecuteConfigQuery("ListSubAtoms", atomPath, null, ReadToken, out stringOutput);
        }
        public string ResolveAtomAsString(string atomPath)
        {
            // Execute
            string stringOutput = null;
            ExecuteConfigQuery("ResolveAtomAsString", atomPath, null, ReadToken, out stringOutput);
            return stringOutput;
        }
        public T Resolve<T>(string atomPath)
        {
            // Execute
            string resolvedString = ResolveAtomAsString(atomPath);

            // Super resolver


            return default(T);
        }
        #endregion

        #region Diagnostic Merchant
        public string Ping()
        {
            // Build client
            using (HttpClient client = new HttpClient())
            {
                // Client setup                
                SetupClient(client, ReadToken);

                // Service call
                ServiceResponse response = null;
                try
                {
                    // Service transaction
                    response = client.PostAsync<object>(
                        BaseURI + "Ping", null,
                        new JsonMediaTypeFormatter())
                        .Result.Content.ReadAsAsync<ServiceResponse>().Result;
                }
                catch (Exception err)
                {
                    throw new CFGDockerException(NetworkFailureBaseMessage, err);
                }

                // Handle response
                if (response.Type == ResponseType.Error)
                {
                    // Hand exception back
                    throw new CFGDockerException(response.Message, null);
                }
                else
                {
                    // Hand response back
                    return response.Message;
                }
            }
        }
        #endregion

        #region Cores
        private List<string> ExecuteConfigQuery(string componentActionURIPart, string componentNamePattern, string valuePattern, string useToken, out string retunStringValue)
        {
            // Declare string list
            List<string> returnStringList = new List<string>();

            // Default output string
            retunStringValue = null;

            // Declare component definition
            ConfigHubQuery thisComponentQuery = null;

            // Build client
            using (HttpClient client = new HttpClient())
            {
                // Client setup                
                SetupClient(client, useToken);

                // Build component query
                thisComponentQuery = new ConfigHubQuery();
                thisComponentQuery.NamePattern = componentNamePattern;
                thisComponentQuery.ValuePattern = valuePattern;

                // Service call
                ServiceResponse response = null;
                try
                {
                    // Service transaction
                    response = client.PostAsync<ConfigHubQuery>(
                            BaseURI + componentActionURIPart,
                            thisComponentQuery,
                            new JsonMediaTypeFormatter())
                            .Result.Content.ReadAsAsync<ServiceResponse>().Result;
                    
                    // Set returns if applicable
                    try
                    {
                        retunStringValue = response.Payload.ToString();
                    }
                    catch
                    {
                        // Don't care
                    }
                    try
                    {                        
                        returnStringList = JsonConvert.DeserializeObject<List<string>>(response.Payload.ToString());                        
                    }
                    catch
                    {
                        // Don't care
                    }
                }
                catch (Exception err)
                {
                    throw new CFGDockerException(NetworkFailureBaseMessage, err);
                }

                // Handle response
                if (response.Type == ResponseType.Error)
                {
                    // Hand exception back
                    throw new CFGDockerException(response.Message, null);
                }
            }

            // Return
            return returnStringList;
        }
        private void SetupClient(HttpClient toSetup, string useToken)
        {
            // Setup the client
            toSetup.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));      
            toSetup.DefaultRequestHeaders.Add("token", useToken);
        }                       
        #endregion
    }
}
