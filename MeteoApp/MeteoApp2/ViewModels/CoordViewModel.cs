using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Net.Http;
using System.Windows.Controls;

namespace MeteoApp2.ViewModels
{
    class CoordViewModel : ViewModelBase
    {
        HttpClient client;

        public Action CloseAction { get; set; }

        public RelayCommand<Button> Proceed { get; set; }
        public RelayCommand<Button> Back { get; set; }

        float lon;
        float lat;

        public float Lon { get => lon; set { lon = value; RaisePropertyChanged(); } }
        public float Lat { get => lat; set { lat = value; RaisePropertyChanged(); } }


        public CoordViewModel()
        {
            Lat = 12.34f;
            Lon = 12.34f;

            Proceed = new RelayCommand<Button>(ButtonProceed);
            Back = new RelayCommand<Button>(ButtonBack);
        }


        private void ButtonProceed(Button param)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("http://api.openweathermap.org/data/2.5/weather?lat=" + Lat + "&lon=" + Lon + "&units=metric&lang=fr&APPID=3e934e36e836e44e076f42503c308385");

            MainViewModel.Search(client);
            CloseAction();
        }

        private void ButtonBack(Button param)
        {
            new MainWindow().Show();
            CloseAction();
        }
    }

}
