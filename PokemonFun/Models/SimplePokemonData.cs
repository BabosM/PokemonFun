using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokemonFun.Models
{
    public class SimplePokemonData
    {
         public PokemonRoot[] results { get; set; }
       
        public class PokemonRoot
        {
            public string name { get; set; }
            public string url { get; set; }
        }
    }
}
