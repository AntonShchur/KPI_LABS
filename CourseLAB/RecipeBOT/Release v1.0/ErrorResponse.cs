using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class ErrorResponse
    {  
        public int status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string link { get; set; }
       
    }
}
