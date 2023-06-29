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

namespace Zadanie2_Tomasz_Ruszkowski
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Kalkulator Kalkulator { get; } = new Kalkulator();
        public MainWindow()
        {
            DataContext = Kalkulator;
            InitializeComponent();
        }

        private void Cyfra(object sender, RoutedEventArgs e)
        {
            Kalkulator.WprowadźCyfrę(((Button)sender).Content.ToString()
);
        }

        private void Przecinek(object sender, RoutedEventArgs e)
        {
            Kalkulator.WprowadźPrzecinek();
        }

        private void ZmianaZnaku(object sender, RoutedEventArgs e)
        {
            Kalkulator.ZmieńZnak();
        }

        private void KasowanieZnaku(object sender, RoutedEventArgs e)
        {
            Kalkulator.KasujZnak();
        }

        private void Czyszczenie(object sender, RoutedEventArgs e)
        {
            Kalkulator.CzyśćWszystko();
        }

        private void CzyszczenieWprowadzenia(object sender, RoutedEventArgs e)
        {
            Kalkulator.CzyśćWynik();
        }

        private void DziałanieDwuargumentowe(object sender, RoutedEventArgs e)
        {
            Kalkulator.WprowadźDziałanieDwuargumentowe(((Button)sender).Content.ToString());
        }

        private void DziałanieJednoargumentowe(object sender, RoutedEventArgs e)
        {
            Kalkulator.WykonajDziałanieJednoargumentowe(((Button)sender).Content.ToString());
        }

        private void RównaSię(object sender, RoutedEventArgs e)
        {
            Kalkulator.WykonajDziałanie();
        }
    }
}
