using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeteoApp2.Models
{
    public class Json
    {                                               //Objet construit à partir du Json issu de la requête sur l'API openweathermap.
        public Coordinates coord { get; set; }
        public Weather[] weather { get; set; }
        public string Base { get; set; }
        public Main main { get; set; }
        public Wind wind { get; set; }
        public Cloud cloud { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }


    }

    public class Coordinates
    {
        public float lon { get; set; }      
        public float lat { get; set; }      
    }

    public class Weather
    {
        public int id { get; set; }     
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
    }

    public class Main
    {
        public float temp { get; set; }
        public float pressure { get; set; }
        public float humidity { get; set; }
        public float temp_min { get; set; }
        public float temp_max { get; set; }
    }

    public class Wind
    {
        public float speed { get; set; }
        public float deg { get; set; }
    }

    public class Cloud
    {
        public string all { get; set; }
    }

    public class Sys
    {
        public int type { get; set; }
        public int id { get; set; }
        public float message { get; set; }
        public string country { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
    }


}
