using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DATABASE
{
    public class dataBaseUser
    {
        public int? id { get; set; }
        public string name { get; set; }
        public long userCode { get; set; }
        public virtual ICollection<dataBaseRecipe> Recipes { get; set; }
    }
}
