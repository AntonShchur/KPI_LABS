using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    
    public class FullRecipeInformation
    {
        public Extendedingredient[] extendedIngredients { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string sourceUrl { get; set; }
        public string image { get; set; }
        public string summary { get; set; }
        public string instructions { get; set; }
        public string spoonacularSourceUrl { get; set; }
    }

    public class Extendedingredient
    {
        public int? id { get; set; }
        public string aisle { get; set; } 
        public string consistency { get; set; }
        public string name { get; set; }
        public string original { get; set; }
        public string originalString { get; set; }
        public string originalName { get; set; }
        public float amount { get; set; }
        public string unit { get; set; }
    }
}
