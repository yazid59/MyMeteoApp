using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp2.Models
{
    public class CountryModel
    {
        public Translations translations { get; set; }
        public string cca2 { get; set; }

        public class Translations
        {
            public Fra fra { get; set; }
        }

        public class Fra
        {
            public string official { get; set; }
            public string common { get; set; }
        }
    }
}
