using projekatWPF.Controller;
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
using System.Xml.Serialization;
using projekatWPF.Model;

namespace projekatWPF.View
{
    /// <summary>
    /// Interaction logic for ProfesoriDodavanje.xaml
    /// </summary>
    public partial class ProfesoriDodavanje : Window
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly ProfesorController _controller;

        public Profesor Profesor { get; set; }
         
        public ProfesoriDodavanje(ProfesorController controller)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            Profesor = new Profesor();

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
                result = MessageBox.Show("Da li sigurno zelite da dodate novog profesora?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to add new professor", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                if (Provera_Brlk())
                    MessageBox.Show("Postoji profesor s tim brojem licne karte!", "Upozozrenje", MessageBoxButton.OK);
                else
                {
                    if (Provera_Adrese_K())
                        MessageBox.Show("Postoji ta adresa kancelarije!", "Upozozrenje", MessageBoxButton.OK);
                    else
                    {
                        if (Provera_Adrese_S())
                            MessageBox.Show("Postoji ta adresa stanovanje!", "Upozozrenje", MessageBoxButton.OK);
                        else
                        {
                            _controller.Create(Profesor);

                            this.Close();
                        }
                    }

                }
            }
        }

        private void tIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else 
                tPotvrdi.IsEnabled = false;
        }

        private void tPrezime_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tDatumRodjenja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tBrojLicneKarte_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tZvanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tGodineStaza_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tDatumRodjenja_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tUlicaStanovanja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tBrojUliceStanovanja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tGradStanovanja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tDrzavaStanovanja_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tUlicaKancelarije_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tBrojUliceKancelarije_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tGradKancelarije_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private void tDrzavaKancelarije_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Profesor.IsValid)
                tPotvrdi.IsEnabled = true;
            else
                tPotvrdi.IsEnabled = false;
        }

        private bool Provera_Brlk()
        {
            List<Profesor> profesori = _controller.GetAllProfesors();
            List<string> lk = new List<string>();
            foreach (Profesor p in profesori)
                lk.Add(p.BrojLicneKarte);

            foreach (string s in lk)
            {
                if (s.Equals(Profesor.BrojLicneKarte))
                    return true;
            }
            return false;
        }

        private bool Provera_Adrese_K()
        {
            List<Adresa> adrese = _controller.GetAllAdrese();
            foreach (Adresa a in adrese)
                if (a.ToString().Equals(Profesor.AdresaKancelarije))
                    return true;
            return false;
        }

        private bool Provera_Adrese_S()
        {
            List<Adresa> adrese = _controller.GetAllAdrese();
            foreach (Adresa a in adrese)
                if (a.ToString().Equals(Profesor.AdresaStanovanja))
                    return true;
            return false;
        }
    }
}
