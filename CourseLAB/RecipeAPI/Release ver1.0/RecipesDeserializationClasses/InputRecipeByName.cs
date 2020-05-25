using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{

    public class InputRecipeByName
    {
        public Result[] results { get; set; }

        public class Result
        {
            public int id { get; set; }       
        }
    }
}