using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFG.Docker
{
    public class CFGDockerException : Exception
    {        
        public CFGDockerException(string message, Exception innerException) : base(message, innerException)
        {
            // Nothing to do here
        }
    }
}
