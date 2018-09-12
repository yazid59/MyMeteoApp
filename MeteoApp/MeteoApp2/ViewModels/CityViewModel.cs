using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MeteoApp2.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;

namespace MeteoApp2.ViewModels
{
    public class CityViewModel : ViewModelBase
    {
        DuplicateCityWindow duplicatewindow;
        
        HttpClient client = new HttpClient();

        public Action CloseAction { get; set; }

        public RelayCommand<Button> Valider { get; set; }
        public RelayCommand<Button> Back { get; set; }

        public CityModel choosedCity = new CityModel();

        string boxPays;
        string boxVille;

        public bool found;

        public string BoxPays { get => boxPays; set { boxPays = value; RaisePropertyChanged(); } }
        public string BoxVille { get => boxVille; set { boxVille = value; RaisePropertyChanged(); } }



        public CityViewModel()      
        {
            Valider = new RelayCommand<Button>(ButtonProceed);
            Back = new RelayCommand<Button>(ButtonBack);
        }

        public string City_TextTransform(string city)       //Méthode retournant le texte de la ville transformé pour correspondre aux normes du fichier Json
        {
            if (city != null && city != "" && city.Length > 1) city = city[0].ToString().ToUpper() + city.Substring(1).ToLower();

            else city = " ";

            return city;
        }

        public string Country_TextTransform(string country)     //Méthode retournant le texte du pays transformé pour correspondre aux normes du fichier Json
        {
            if (country != null && country != "" && country.Length > 1)
            {
                country = country[0].ToString().ToUpper() + country.Substring(1).ToLower();
                foreach(CountryModel countryToCompare in MainViewModel.CountryList)             // cette boucle apporte un peu de flexibilité au niveau de l'écriture du pays
                {
                    if (countryToCompare.translations.fra.common == country || countryToCompare.translations.fra.official == country) country = countryToCompare.cca2;
                }

                country = country.ToUpper();
            }
            return country;
        }

        public void GetWeatherByCityName(string cityName)       //Méthode générant l'URL de recherche de la météo en fonction du nom de ville
        {
            CityModel cityTest = new CityModel();
            List<CityModel> listCity = new List<CityModel>();
            found = false;
            int compteur = 0;
            cityName = City_TextTransform(cityName);


            foreach (CityModel city in MainViewModel.CityList)
            {
                if (cityName == city.name) listCity.Add(city);
            }

            if (listCity.Count == 0)
            {
                cityTest = GetCloserCity(cityName);
                if (cityTest.name != null)
                {
                    if (MessageBox.Show("Aucune ville nommée " + cityName + ", vouliez-vous dire "+cityTest.name+" ? ", "Erreur", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + cityTest.name + "&units=metric&lang=fr&APPID=3e934e36e836e44e076f42503c308385");
                        MainViewModel.Search(client);
                        CloseAction();
                    }
                }
                else MessageBox.Show("Aucune ville nommée " + cityName + ".", "Erreur");
            }
            else if (listCity.Count == 1)  
            {
                choosedCity = listCity[0];
                found = true;
            }
            else
            {
                for (int i = 1; i < listCity.Count; i++)  // le but de ce bloc est de gérer les doublons existants dans la liste de ville si la ville et le pays           
                {                                         // de la liste sont identiques, on suppose que ce sont des doublons et n'importe quelle ville de cette liste fera l'affaire. 
                    if (listCity[i].country == listCity[i - 1].country && listCity[i].name == listCity[i - 1].name) compteur++;
                }
                if (compteur == listCity.Count)
                {
                    choosedCity = listCity[0];
                    found = true;
                }
                else
                {                                           // dans le cas contraire, nous sommes en présence de villes aux noms identiques mais des pays différents.
                    duplicatewindow = new DuplicateCityWindow(this, cityName);
                    duplicatewindow.Show();
                }
            }

            if (found == true)
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "&units=metric&lang=fr&APPID=3e934e36e836e44e076f42503c308385");
                MainViewModel.Search(client);
            }
            CloseAction();
        }

        public void GetWeatherByCityAndCountry(string cityName, string countryName)         // Méthode générant l'URL de recherche de la météo en fonction du nom de ville et pays
        {
            CityModel cityTest = new CityModel();
            List<CityModel> listCity = new List<CityModel>();
            found = false;

            cityName = City_TextTransform(cityName);
            countryName = Country_TextTransform(countryName);

            foreach (CityModel city in MainViewModel.CityList)
            {
                if (cityName == city.name && countryName == city.country) listCity.Add(city);
            }

            if (listCity.Count == 0)
            {
                cityTest = GetCloserCity(cityName, countryName);
                if (cityTest.name != null)
                {
                    if (MessageBox.Show("Aucune ville nommée " + cityName + " dans le pays " + BoxPays + " vouliez-vous dire " + cityTest.name + " ? ", "Erreur", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + cityTest.name + "," + cityTest.country + "&units=metric&lang=fr&APPID=3e934e36e836e44e076f42503c308385");
                        MainViewModel.Search(client);
                        CloseAction();
                    }
                }
                else MessageBox.Show("Aucune ville nommée " + cityName + " dans le pays "+countryName+".", "Erreur");
            }
            else
            {
                choosedCity = listCity[0];
                found = true;
            }

            if (found == true)
            {
                client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?q=" + cityName + "," + countryName + "&units=metric&lang=fr&APPID=3e934e36e836e44e076f42503c308385");
                MainViewModel.Search(client);
                CloseAction();
            }

        }

        public CityModel GetCloserCity(string cityName, string country = null)     // Methode permettant de se référer à la ville portant le nom se rapprochant le plus à celui saisi par l'utilisateur
        {                                                                                   // si un nom de pays est également spécifié, la recherche s'effectue uniquement pour les villes de ce pays
            CityModel cityTest = new CityModel();
            int sameLetters = 0;
            int previousSameLetters = 0;

            if (country == null)
            {
                foreach (CityModel city in MainViewModel.CityList)
                {
                    if (city.name.Length > 1)
                    {
                        sameLetters = 0;
                        for (int i = 0; (i < cityName.Length && i < city.name.Length); i++)
                        {
                            if (city.name[i] == cityName[i]) sameLetters++;
                        }
                        if (sameLetters > previousSameLetters)
                        {
                            cityTest = city;
                            previousSameLetters = sameLetters;
                        }
                    }
                }
            }
            else
            {
                foreach (CityModel city in MainViewModel.CityList)
                {
                    if (city.country == country && city.name.Length > 1)
                    {
                        sameLetters = 0;
                        for (int i = 0; (i < cityName.Length && i < city.name.Length); i++)
                        {
                            if (city.name[i] == cityName[i]) sameLetters++;
                        }
                        if (sameLetters > previousSameLetters)
                        {
                            cityTest = city;
                            previousSameLetters = sameLetters;
                        }
                    }
                }
            }
            return cityTest;
        }

        private void ButtonProceed(Button param)           // Méthode de validation rattachée au bouton 
        {
            if ((BoxVille != "" && BoxVille != null) && (BoxPays == "" || BoxPays == null)) GetWeatherByCityName(BoxVille);
            else if ((BoxVille != "" && BoxVille != null) || (BoxPays != "" && BoxPays != null)) GetWeatherByCityAndCountry(BoxVille, BoxPays);
            else MessageBox.Show("Veuillez compléter les champs", "Erreur");
        }

        private void ButtonBack(Button param)
        {
            new MainWindow().Show();
            CloseAction();
        }
    }
}
