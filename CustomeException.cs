using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class CustomeException:Exception
    {
        public CustomeException() { }
        public CustomeException(string message) : base(message) { }

        //public CustomeException(string message, Exception innerException) : base(message, innerException) { }

        public string Message { get;set; }
        public string CustomMessage { get;set; }    

    }
}
