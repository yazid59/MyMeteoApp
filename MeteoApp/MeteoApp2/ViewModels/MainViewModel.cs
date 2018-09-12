
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using Windows.Devices.Geolocation;
using System;
using System.Net.Http;
using MeteoApp2.Models;
using Newtonsoft.Json;
using System.IO;
using System.Windows;

namespace MeteoApp2.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

        public static CityModel[] CityList;
        public static CountryModel[] CountryList;


        public Action CloseAction { get; set; }

        public RelayCommand<Button> ByCity { get; set; }
        public RelayCommand<Button> ByCoord { get; set; }
        public RelayCommand<Button> LocateMe { get; set; }
        public RelayCommand<Button> Exit { get; set; }

        public MainViewModel()
        {
            ByCity = new RelayCommand<Button>(SearchByCity);
            ByCoord = new RelayCommand<Button>(SearchByCoord);
            LocateMe = new RelayCommand<Button>(SearchByLocalisationAsync);
            Exit = new RelayCommand<Button>(ButtonExit);

            if (CityList==null) CityList = JsonConvert.DeserializeObject<CityModel[]>(new StreamReader("..//..//Resources//city.list.json").ReadToEnd());
            if (CountryList == null) CountryList = JsonConvert.DeserializeObject<CountryModel[]>(new StreamReader("..//..//Resources//countries.json").ReadToEnd());

        }



        private void SearchByCity(Button param)          // Méthode de validation rattachée au bouton recherche par ville
        {
           new CityWindow().Show();
           CloseAction();
        }

        private void SearchByCoord(Button param)          // Méthode de validation rattachée au bouton recherche par coordonnées
        {
            new CoordWindow().Show();
            CloseAction();
        }

        private async void SearchByLocalisationAsync(Button param)          // Méthode de validation rattachée au bouton recherche par géolocalisation
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            HttpClient client;

            if (accessStatus==GeolocationAccessStatus.Allowed)
            {
                Geoposition geoposition = await new Geolocator().GetGeopositionAsync();

                client = new HttpClient();
                client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather" + "?lat=" + geoposition.Coordinate.Latitude + "&lon=" + geoposition.Coordinate.Longitude + "&units=metric&lang=fr&APPID=3e934e36e836e44e076f42503c308385");

                Search(client);
            }
            else
            {
                
                HttpClient ipLocateClient = new HttpClient { BaseAddress = new Uri("http://ip-api.com/json") };

                var request = ipLocateClient.GetStringAsync(ipLocateClient.BaseAddress);
                request.Wait();

                IpLocateModel iplocate = JsonConvert.DeserializeObject<IpLocateModel>(request.Result);

                client=new HttpClient { BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather" + "?lat=" + iplocate.lat + "&lon=" + iplocate.lon + "&units=metric&lang=fr&APPID=3e934e36e836e44e076f42503c308385") };
                Search(client);
            }
            CloseAction();
        }

        private void ButtonExit(Button param)       // Quitter le programme
        {
            CloseAction();
        }

        public static void Search(HttpClient client)            //Méthode qui effectue la recherche de météo
        {
            var request = client.GetStringAsync(client.BaseAddress);
            request.Wait();

            Json result = JsonConvert.DeserializeObject<Json>(request.Result);
            new WeatherWindow(result).Show();
        }

    }
}
