using CFG.Docker.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CFG.Docker
{
    public class StandardConfigHubDocker : IDocker
    {
        private string BaseURL = null;
        private string Token = null;

        public void Setup(string serverNameAndPort, string token, bool useHttps)
        {
            // Setup
            BaseURL = (useHttps ? "https://" : "http://") + serverNameAndPort + "/Dock/";
            Token = token;
        }

        public List<string> Find(string configurationSearchPattern)
        {
            throw new NotImplementedException();
        }

        public List<string> ListChildren(string ofConfigurationPath)
        {
            throw new NotImplementedException();
        }

        public string Ping()
        {
            // Return ping result
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("token", "25E9BDF2-DF1E-42BD-BD58-1CAFFDC554A2");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));                    
                    string rawResponse = client.GetAsync(BaseURL + "Ping").Result.Content.ReadAsStringAsync().Result;
                    ServiceResponse response = JsonConvert.DeserializeObject<ServiceResponse>(rawResponse);
                    if(response.Type == ResponseType.Error)
                    {
                        throw new CFGDockerException(response.Message, null);
                    }
                    else
                    {
                        return response.Message;
                    }
                }
            }
            catch(Exception err)
            {
                throw new CFGDockerException("Network error for URL '" + BaseURL + "Ping" + "'", err);
            }           
        }

        public void Publish(string configurationPath, string configurationValue)
        {
            throw new NotImplementedException();
        }

        public T Resolve<T>(string configurationPath)
        {
            throw new NotImplementedException();
        }
    }
}
