using projekatWPF.Controller;
using projekatWPF.Model;
using projekatWPF.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ProfesorIzmena.xaml
    /// </summary>
    public partial class ProfesorIzmena : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly ProfesorController _controller;
        private readonly PredmetController _predmetController;
        private readonly KatedraController _katedraController;
        private readonly ProfesorController _profesorController;

        private List<Predmet> SelectedPredmeti { get; set; }
        public Profesor Profesor { get; set; }
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public ObservableCollection<Katedra> Katedre { get; set; }

        public ProfesorIzmena(ProfesorController controller, KatedraController katedraController, PredmetController predmetController)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            _controller = controller;
            _predmetController = predmetController;
            _katedraController = katedraController;
            _profesorController = controller;

            _controller.Subscribe(this);
            _predmetController.Subscribe(this);
            _katedraController.Subscribe(this);
            _profesorController.Subscribe(this);

            Predmeti = new ObservableCollection<Predmet>();
            Katedre = new ObservableCollection<Katedra>(_katedraController.GetAllKatedre());

            Profesor = new Profesor();

            tGodineStaza.Text = "";
            Inicijalizacija();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateTime dt;
            DateOnly datumr;
            Adresa adresas = new Adresa();
            Adresa adresak = new Adresa();
            try
            {
                dt = DateTime.Parse(tDatumRodjenja.Text);
                datumr = new DateOnly(dt.Year, dt.Month, dt.Day);
                adresas = new Adresa(tUlicaStanovanja.Text, tBrojUliceStanovanja.Text, tGradStanovanja.Text, tDrzavaStanovanja.Text);
                adresak = new Adresa(tUlicaKancelarije.Text, tBrojUliceKancelarije.Text, tGradKancelarije.Text, tDrzavaKancelarije.Text);
                MessageBoxResult result;
                if(app.getCultureInfo() == SRB)
                    result = MessageBox.Show("Da li sigurno zelite da izmenite profesora?", "Provera", MessageBoxButton.YesNo);
                else
                    result = MessageBox.Show("Are you sure you want to change professor?", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    if (Profesor.IsValid)
                    {
                        _controller.Izmeni(Profesor.Prezime, Profesor.Ime, datumr, Profesor.Telefon, Profesor.Email, Profesor.BrojLicneKarte, Profesor.GodineStaza, adresak.ToString(), Profesor.Zvanje, adresas.ToString());
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Polja moraju biti adekvatno popunjena!", "Upozorenje", MessageBoxButton.OK);
                    }

                }
            }
            catch { MessageBox.Show("Datum nije dobro unet"); }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li zelite da izadjete?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButton.YesNo);
            if ( result == MessageBoxResult.Yes)
                this.Close();
        }
        private void Inicijalizacija()
        {
            List<Profesor> profesori = _controller.GetAllProfesors();
            Profesor pravi = profesori.Find(p => p.BrojLicneKarte.ToUpper() == MainWindow.brlicne.ToUpper());

            Profesor.Ime = pravi.Ime;
            Profesor.Prezime = pravi.Prezime;

            pravi.DatumPom = new DateTime(pravi.DatumRodjenja.Year, pravi.DatumRodjenja.Month, pravi.DatumRodjenja.Day);
            Profesor.DatumPom = pravi.DatumPom;

            Profesor.AdresaStanovanja = pravi.AdresaStanovanja.ToString();
            string[] niz = pravi.AdresaStanovanja.Split(',');
            Profesor.UlicaStanovanja = niz[0];
            Profesor.BrojUliceStanovanja = niz[1];
            Profesor.GradStanovanja = niz[2];
            Profesor.DrzavaStanovanja = niz[3];
            string[] niz1 = pravi.AdresaKancelarije.Split(',');
            Profesor.UlicaKancelarije = niz1[0];
            Profesor.BrojUliceKancelarije = niz1[1];
            Profesor.GradKancelarije = niz1[2];
            Profesor.DrzavaKancelarije = niz1[3];
            Profesor.Email = pravi.Email;
            Profesor.BrojLicneKarte = pravi.BrojLicneKarte;
            Profesor.Telefon = pravi.Telefon;
            Profesor.GodineStaza = pravi.GodineStaza;
            tGodineStaza.Text = Profesor.GodineStaza.ToString();
            Profesor.Zvanje = pravi.Zvanje;
            Profesor.AdresaKancelarije = pravi.AdresaKancelarije.ToString();

            List<Predmet> pomocnaListaPredmeta = _predmetController.GetAllPredmeti();
            pomocnaListaPredmeta = pomocnaListaPredmeta.Where(p => p.Profesor.BrojLicneKarte == pravi.BrojLicneKarte).ToList();

            foreach(Predmet p in pomocnaListaPredmeta)
                Predmeti.Add(p);

            foreach (Katedra k in Katedre)
            {
                foreach (Profesor p in k.SpisakProfesora)
                    if (p.BrojLicneKarte == Profesor.BrojLicneKarte && k.Sef.BrojLicneKarte != Profesor.BrojLicneKarte)
                        lKatedre.Items.Add(k.Sifra + " - " + k.Naziv);
            }

            Izgled(profesorPredmetiData);
        }
        public void Update()
        {
            
        }
        public void Izgled(DataGrid d)
        {
            for (int i = 0; i < d.Columns.Count; i++)
                d.Columns[i].Width = (d.Width) / d.Columns.Count;
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

        private void bDodavanjePredmeta_Click(object sender, RoutedEventArgs e)
        {
            Window dodavanjePredmeta = new DodavanjePredmetaProfesoru(_predmetController, _profesorController, Predmeti);
            dodavanjePredmeta.Owner = this;
            dodavanjePredmeta.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(SelectedPredmeti.Count != 0)
            {
                foreach(Predmet p in SelectedPredmeti)
                {
                    foreach(Predmet pom in Predmeti)
                    {
                        if(pom.Sifra == p.Sifra)
                        {
                            Predmeti.Remove(pom);
                            pom.Profesor.BrojLicneKarte = "";
                            _predmetController.Izmeni(pom.Sifra, pom.Naziv, pom.ESPB, pom.Semestar.ToString(), pom.GodinaIzvodjenja);

                            Profesor = _profesorController.GetAllProfesors().Find(i => i.BrojLicneKarte == Profesor.BrojLicneKarte);

                            Predmet predmetZaProfesor = Profesor.Spisak_predmeta.Find(i => i.Sifra == pom.Sifra);
                            Profesor.Spisak_predmeta.Remove(predmetZaProfesor);
                            _profesorController.Izmeni(Profesor.Prezime, Profesor.Ime, Profesor.DatumRodjenja, Profesor.Telefon, Profesor.Email, Profesor.BrojLicneKarte, Profesor.GodineStaza, Profesor.AdresaKancelarije, Profesor.Zvanje, Profesor.AdresaStanovanja);

                            break;
                        }
                    }
                }
            }
            else
            {
                if (app.getCultureInfo() == SRB)
                    MessageBox.Show("Morate izabrati najmanje 1 predmet koji uklanjate", "Upozorenje", MessageBoxButton.OK);
                else
                    MessageBox.Show("You have to select at least 1 subject before trying to remove it", "Warning", MessageBoxButton.OK);
            }
        }

        private void profesorPredmetiData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedPredmeti = profesorPredmetiData.SelectedItems.Cast<Predmet>().ToList();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if ((Profesor.Zvanje.ToUpper() == "REDOVNI_PROFESOR" || Profesor.Zvanje.ToUpper() == "VANREDNI_PROFESOR") && Profesor.GodineStaza >= 5 && lKatedre.SelectedItem != null)
            {
                int sifra = int.Parse(lKatedre.SelectedItem.ToString().Split(" - ")[0]);

                Katedra k = Katedre.Where(k => k.Sifra == sifra).ToList()[0];

                k.Sef.BrojLicneKarte = Profesor.BrojLicneKarte;
                _katedraController.Izmeni(k.Sifra, k.Naziv);

                if (app.getCultureInfo() == SRB)
                    MessageBox.Show("Uspesno postavljanje sefa katedre");
                else
                    MessageBox.Show("Head professor successufly added");
                this.Close();
            }
            else
            {
                if (app.getCultureInfo() == SRB)
                    MessageBox.Show("Morate selektovati katedru i profesora koji ima zvanje redovnog/vandrednog profesora i min 5 godina staza", "Upozorenje", MessageBoxButton.OK);
                else
                    MessageBox.Show("You have to select a department and a professor with title of \'redovan/vandredni profesor\' with a minimum of 5 years of experience", "Warning", MessageBoxButton.OK);
            }
        }
    }
}
