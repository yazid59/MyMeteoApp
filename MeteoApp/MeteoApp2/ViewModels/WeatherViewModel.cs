using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MeteoApp2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeteoApp2.ViewModels
{
    class WeatherViewModel : ViewModelBase
    {

        Json ResultJson;

        public Action CloseAction { get; set; }

        public RelayCommand<Button> Exit { get; set; }

        ObservableCollection<String> listUnites;
        private string selectedUnite;

        private string pays;
        private string ville;

        private string lon;
        private string lat;

        private string description;

        private string temp;

        private string speed;
        private string deg;


        public ObservableCollection<string> ListUnites { get => listUnites; set => listUnites = value; }
        public string SelectedUnite { get => selectedUnite; set { selectedUnite = value; Conversion(selectedUnite); } }

        public string Pays { get => pays; set => pays = value; }
        public string Ville { get => ville; set => ville = value; }
        public string Lon { get => lon; set => lon = value; }
        public string Lat { get => lat; set => lat = value; }
        public string Description { get => description; set => description = value; }
        public string Temp { get => temp; set => temp = value; }
        public string Speed { get => speed; set => speed = value; }
        public string Deg { get => deg; set { deg = value; } }


        public WeatherViewModel(Json ResultJson)
        {
            this.ResultJson = ResultJson;

            ListUnites = new ObservableCollection<string>() { "°C", "°F" };
            SelectedUnite = ListUnites[0];

            Exit = new RelayCommand<Button>(ButtonExit);

            Ville = ResultJson.name;
            GetCountry();

            Lon = Convert.ToString(ResultJson.coord.lon);
            Lat = Convert.ToString(ResultJson.coord.lat);

            Description = Convert.ToString(ResultJson.weather[0].description);

            Temp = Convert.ToString(Convert.ToInt32(ResultJson.main.temp));

            Speed = Convert.ToString(Convert.ToInt32(ResultJson.wind.speed*3.6)+" km/h");
            Deg = DirectionVent(ResultJson.wind.deg);

        }
        
        public void GetCountry()
        {
            foreach(CountryModel country in MainViewModel.CountryList)
            {
                if (ResultJson.sys.country == country.cca2) Pays = country.translations.fra.common;
            }
        }
        
        public void Conversion(string unite)        //Méthode permettant de modifier l'unité de température en fonction du choix de l'utilisateur
        {
            if (Temp!=null)      
            { 
                if (unite == ListUnites[0])
                {
                    Temp = Convert.ToString(Convert.ToInt32((ResultJson.main.temp - 32) / 1.8f));
                    ResultJson.main.temp= (ResultJson.main.temp - 32) / 1.8f;
                }
                else if (unite == ListUnites[1])
                {
                    Temp = Convert.ToString(Convert.ToInt32(ResultJson.main.temp * 1.8f + 32));
                    ResultJson.main.temp = ResultJson.main.temp * 1.8f + 32;
                }
            }
        }

        public string DirectionVent(float direction)        // la direction est exprimée en degrés, cette méthode permet de la traduire en notion plus facile à lire
        {
            string DirectionString="";
            switch (direction)
            {
                case 0:
                    break;
                case float n when ( n > 360 || n < 23): DirectionString = "nord";
                    break;
                case float n when (n > 23 || n < 68):DirectionString = "nord-est";
                    break;
                case float n when (n > 68 || n < 113):DirectionString = "est";
                    break;
                case float n when (n > 113 || n < 158):DirectionString = "sud-est";
                    break;
                case float n when (n > 158 || n < 203):DirectionString = "sud";
                    break;
                case float n when (n > 203 || n < 248):DirectionString = "sud-ouest";
                    break;
                case float n when (n > 248 || n < 293):DirectionString = "ouest";
                    break;
                case float n when (n > 293 || n < 338):DirectionString = "nord-ouest";
                    break;
            }
            return DirectionString;
        }

        public void ButtonExit(Button param)
        {
            new MainWindow().Show();
            CloseAction();
        }


    }
}
