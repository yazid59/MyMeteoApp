using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using MeteoApp2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeteoApp2.ViewModels
{
    public class DuplicateCityViewModel : ViewModelBase
    {
        CityViewModel parentWindow;

        public Action CloseAction { get; set; }

        public RelayCommand<Button> Proceed { get; set; }
        public RelayCommand<Button> Back { get; set; }

        string city;
        string country;

        public string Country { get => country; set { country = value; RaisePropertyChanged(); } }


        public DuplicateCityViewModel(CityViewModel parentWindow, string city)
        {
            this.parentWindow = parentWindow;
            this.city = city;
            Proceed = new RelayCommand<Button>(ButtonProceed_Click);
            Back = new RelayCommand<Button>(ButtonBack);

        }


        private void ButtonProceed_Click(Button param)          // Méthode de validation rattachée au bouton
        {
            if (Country != "" && Country != null)
            {
                parentWindow.GetWeatherByCityAndCountry(this.city, Country);
                if (parentWindow.found == true)
                {
                    CloseAction();
                    parentWindow.CloseAction();
                }
            }
            else MessageBox.Show("Veuillez saisir l'identifiant du pays selon les normes ISO 3166\n"+new Uri("http://www.trucsweb.com/tutoriels/internet/iso_3166/"), "Erreur");
        }


        private void ButtonBack(Button param)
        {
            new CityWindow().Show();
            CloseAction();
        }


    }
}
