using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATABASE
{
    public class dataBaseRecipe
    {
        public int? id { get; set; }
        public string name { get; set; }   
        public int recipeCode { get; set; }
        public int UserId { get; set; }
        public virtual dataBaseUser User { get; set; }
    }
}
