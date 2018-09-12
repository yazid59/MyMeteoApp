using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp2.Models
{
    public class CityModel
    {                                           //différentes propriétés propres aux villes : chaque ville a un CityModel
        public string name { get; set; }
        public string country { get; set; }
        public Coordinates coord { get; set; }         
    }
}
