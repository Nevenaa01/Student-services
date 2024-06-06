using projekatWPF.Controller;
using projekatWPF.Model;
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

namespace projekatWPF.View
{
    /// <summary>
    /// Interaction logic for PredmetiDodavanje.xaml
    /// </summary>
    public partial class PredmetiDodavanje : Window
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly PredmetController _controller;

        public Predmet Predmet { get; set; }
                

        public PredmetiDodavanje(PredmetController controller)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            Predmet = new Predmet();

            _controller = controller;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li sigurno zelite da izađete?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li sigurno zelite da dodate novi predmet?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to add new subject?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {

                if (Provera_Sifre())
                    MessageBox.Show("Postoji predmet s tom sifrom!", "Upozorenje", MessageBoxButton.OK);
                else
                {
                    
                    _controller.Create(Predmet);

                    this.Close();
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tGodinaIzvodjenja.SelectedIndex = 0;
        }

        private void tSifra_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Predmet.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tNaziv_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Predmet.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tESPB_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Predmet.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }
        private bool Provera_Sifre()
        {
            List<Predmet> predmeti = _controller.GetAllPredmeti();
            List<string> sifre = new List<string>();
            foreach (Predmet p in predmeti)
                sifre.Add(p.Sifra);

            foreach (string s in sifre)
            {
                if (s == Predmet.Sifra)
                    return true;
            }
            return false;
        }
    }
}
