using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFG.Docker
{
    public interface IDocker
    {
        string Ping();

        T Resolve<T>(string configurationPath);

        void Publish(string configurationPath, string configurationValue);

        List<string> ListChildren(string ofConfigurationPath);

        List<string> Find(string configurationSearchPattern);

        void Setup(string serverNameAndPort, string token, bool useHttps);
    }
}
