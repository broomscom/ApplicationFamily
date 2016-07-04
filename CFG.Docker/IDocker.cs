using CFG.Docker.Models;
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

        void RegisterComponent(string componentName);        

        void Setup(string serverNameAndPort, string readerToken, string publisherToken, bool useHttps);
    }
}
