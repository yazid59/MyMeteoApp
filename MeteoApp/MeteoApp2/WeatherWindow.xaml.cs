using MeteoApp2.Models;
using MeteoApp2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MeteoApp2
{
    /// <summary>
    /// Logique d'interaction pour WeatherWindow.xaml
    /// </summary>
    public partial class WeatherWindow : Window
    {
        public WeatherWindow(Json j)
        {
            InitializeComponent();
            WeatherViewModel viewModel= new WeatherViewModel(j);
            this.DataContext = viewModel;
            if (viewModel.CloseAction == null) viewModel.CloseAction = new Action(Close);
        }

    }
}
