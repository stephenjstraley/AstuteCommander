using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AstuteCommander
{
    public class JsonMessage
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public JsonMessage(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
