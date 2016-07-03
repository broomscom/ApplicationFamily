using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFG.Docker
{
    public class StandardConfigHubDocker : IDocker
    {
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
            throw new NotImplementedException();
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
