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

        void DeleteComponent(string componentName);

        void PublishConfigurationAtom(string atomPath, string value);

        void DeleteConfigurationAtom(string atomPath);

        List<string> ListComponents();

        List<string> ListSubAtoms(string atomPath);

        string ResolveAtomAsString(string atomPath);

        T Resolve<T>(string atomPath);

        void Setup(string serverNameAndPort, string readerToken, string publisherToken, bool useHttps);
    }
}
