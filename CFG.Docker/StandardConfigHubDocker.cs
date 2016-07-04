using CFG.Docker.Models;
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

        #region Merchant
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

        public void RegisterComponent(string componentName)
        {
            // Execute
            ExecuteComponentQuery(componentName, "RegisterComponent", null);            
        }

        public void DeleteComponent(string componentName)
        {
            // Execute
            ExecuteComponentQuery(componentName, "DeleteComponent", null);
        }
        #endregion

        #region Cores
        private void ExecuteComponentQuery(string componentNamePattern, string componentAction, string componentDescriptionPattern)
        {
            // Declare component definition
            ComponentQuery thisComponentQuery = null;

            // Build client
            using (HttpClient client = new HttpClient())
            {
                // Client setup                
                SetupClient(client, PublishToken);

                // Build component query
                thisComponentQuery = new ComponentQuery();
                thisComponentQuery.ComponentNamePattern = componentNamePattern;
                thisComponentQuery.ComponentDescriptionPattern = componentDescriptionPattern;

                // Service call
                ServiceResponse response = null;
                try
                {
                    // Service transaction
                    response = client.PostAsync<ComponentQuery>(
                        BaseURI + componentAction,
                        thisComponentQuery,
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
            }
        }
        private void SetupClient(HttpClient toSetup, string usingToken)
        {
            // Setup the client
            toSetup.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            toSetup.DefaultRequestHeaders.Add("token", usingToken);
        }
        #endregion
    }
}
