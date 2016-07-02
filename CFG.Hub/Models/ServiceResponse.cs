using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFG.Hub.Models
{
    public enum ResponseType { Success, Error }

    public class ServiceResponse
    {
        public ResponseType Type { get; set; }
        
        public string Message { get; set; }

        public object Payload { get; set; }
    }
}