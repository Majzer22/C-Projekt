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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logistic
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        const decimal paletteWeight = 25;
        const decimal paletteLength = 1200;
        const decimal paletteWidth = 800;
        const decimal paletteHeight = 144;
        const decimal palettePatch = 960000; 

        decimal MaxPackagesByLength, MaxPackagesByWidth, MaxPackagesPerLayer, MaxCargoHeight, MaxPackagesHeight, MaxCargoLayers, CargoWeight;

        bool conditionByLength, conditionByWidth;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            conditionByLength = PackagesByLength(Decimal.Parse(PackagesLength.Text));
            conditionByWidth = PackagesByWidth(Decimal.Parse(PackagesWidth.Text));
            PackagesPerLayer();
            CargoLayersByMaxHeight();
            CargoWeightByLayers();
        }

        private bool PackagesByLength(decimal PackageLength)
        {
            decimal condition = Math.Floor(paletteLength / PackageLength);
            if ((PackageLength * condition) > paletteLength) {
                MaxPackagesByLength = condition;
                return true; 
            }
            else return false;
        }

        private bool PackagesByWidth(decimal PackageWidth)
        {
            decimal condition = Math.Floor(paletteWidth / PackageWidth);
            if ((PackageWidth * condition) > paletteWidth)
            {
                MaxPackagesByWidth = condition;
                return true;
            }
            else return false;
        }

        private void PackagesPerLayer()
        {
            if (conditionByLength && conditionByWidth)
            {
                decimal PackagePatch = (Decimal.Parse(PackagesLength.Text) * Decimal.Parse(PackagesWidth.Text));
                MaxPackagesPerLayer = Math.Floor(palettePatch / PackagePatch);
                ThrowMessage("Liczba paczek na palecie w 1 warstwie = " + MaxPackagesPerLayer);
            }
        }

        private void CargoWeightByLayers()
        {
            MaxCargoHeight = Decimal.Parse(PackagesHeight.Text) * Decimal.Parse(PackagesLayer.Text);
            ThrowMessage("Wysokość ładunku  = " + MaxCargoHeight);
        }

        private void PackagesHeightByLayers()
        {
            MaxPackagesHeight = MaxCargoHeight + paletteHeight;
        }

        private void CargoLayersByMaxHeight()
        {
           MaxCargoLayers = Math.Floor(Decimal.Parse(CargoMaxHeight.Text) / Decimal.Parse(PackagesHeight.Text));
            ThrowMessage("Liczba warstw = " + MaxCargoLayers);
        }

        private void ThrowMessage(string message)
        {
            Output.AppendText(message);
        }
    }

}
