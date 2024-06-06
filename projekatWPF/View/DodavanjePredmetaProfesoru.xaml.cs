using projekatWPF.Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using projekatWPF.Controller;
using projekatWPF.Model;
using projekatWPF.Observer;

namespace projekatWPF.View
{
    /// <summary>
    /// Interaction logic for DodavanjePredmetaProfesoru.xaml
    /// </summary>
    public partial class DodavanjePredmetaProfesoru : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly PredmetController _predmetController;
        private readonly ProfesorController _profesorController;
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public ObservableCollection<Predmet> Dodeljeni { get; set; }
        public Predmet SelectedPredmet { get; set; }
        public DodavanjePredmetaProfesoru(PredmetController controller, ProfesorController profesorController, ObservableCollection<Predmet> dodeljeni)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            _predmetController = controller;
            _profesorController = profesorController;

            _predmetController.Subscribe(this);
            _profesorController.Subscribe(this);

            Predmeti = new ObservableCollection<Predmet>(_predmetController.GetAllPredmeti());

            Dodeljeni = dodeljeni;
        }

        private void bOdustani_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li ste sigurni da želite da izađete?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                this.Close();
        }

        public void Update()
        {
            
        }

        private void lListaPredmeta_Loaded(object sender, RoutedEventArgs e)
        {
            List<Predmet> nedodat = Predmeti.Where(p => !Dodeljeni.Contains(p)).ToList();

            foreach(Predmet p in nedodat)
            {
                lListaPredmeta.Items.Add(p.Sifra + " - " + p.Naziv);
            }
        }

        private void bPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if (lListaPredmeta.SelectedItems != null)
            {
                string[] izabran = new string[lListaPredmeta.Items.Count];

                int i = 0;
                foreach (var item in lListaPredmeta.SelectedItems)
                {
                    izabran[i++] = item.ToString().Split(" - ")[0];
                }

                Predmet predmet = new Predmet();
                foreach (string iz in izabran)
                {
                    foreach (Predmet p in Predmeti)
                    {
                        if (p.Sifra == iz)
                        {
                            if (!string.IsNullOrEmpty(p.Profesor.BrojLicneKarte))
                            {
                                MessageBoxResult result;
                                if (app.getCultureInfo() == SRB)
                                    result = MessageBox.Show("Na selektovanom predmetu već postoji profesor.\n" +
                                        "Da li želite da ga zamenite selektovanim?", "Upozorenje", MessageBoxButton.YesNo);
                                else
                                    result = MessageBox.Show("There is already professor who is teaching that class.\n" +
                                        "Do you wish to switch it with the selected one?", "Warning", MessageBoxButton.YesNo);

                                if (result == MessageBoxResult.Yes)
                                {
                                    predmet = p;

                                    Profesor profModif = _profesorController.GetAllProfesors().Find(pom => pom.BrojLicneKarte == predmet.Profesor.BrojLicneKarte);
                                    Predmet predmetZaProfModif = profModif.Spisak_predmeta.Find(pom => pom.Sifra == predmet.Sifra);
                                    profModif.Spisak_predmeta.Remove(predmetZaProfModif);
                                    _profesorController.Izmeni(profModif.Prezime, profModif.Ime, profModif.DatumRodjenja,
                                        profModif.Telefon, profModif.Email, profModif.BrojLicneKarte, profModif.GodineStaza, profModif.AdresaKancelarije,
                                        profModif.Zvanje, profModif.AdresaStanovanja);

                                    predmet.Profesor.BrojLicneKarte = MainWindow.brlicne;

                                    Dodeljeni.Add(predmet);
                                    _predmetController.Izmeni(predmet.Sifra, predmet.Naziv, predmet.ESPB, predmet.Semestar.ToString(), predmet.GodinaIzvodjenja);

                                    Profesor prof = _profesorController.GetAllProfesors().Find(pom => pom.BrojLicneKarte == MainWindow.brlicne);
                                    prof.Spisak_predmeta.Add(predmet);
                                    _profesorController.Izmeni(prof.Prezime, prof.Ime, prof.DatumRodjenja, prof.Telefon, prof.Email, prof.BrojLicneKarte, prof.GodineStaza, prof.AdresaKancelarije, prof.Zvanje, prof.AdresaStanovanja);

                                    break;
                                }
                            }
                            else
                            {
                                predmet = p;
                                predmet.Profesor.BrojLicneKarte = MainWindow.brlicne;
                                Dodeljeni.Add(predmet);
                                _predmetController.Izmeni(predmet.Sifra, predmet.Naziv, predmet.ESPB, predmet.Semestar.ToString(), predmet.GodinaIzvodjenja);

                                Profesor prof = _profesorController.GetAllProfesors().Find(pom => pom.BrojLicneKarte == MainWindow.brlicne);
                                prof.Spisak_predmeta.Add(predmet);
                                _profesorController.Izmeni(prof.Prezime, prof.Ime, prof.DatumRodjenja, prof.Telefon, prof.Email, prof.BrojLicneKarte, prof.GodineStaza, prof.AdresaKancelarije, prof.Zvanje, prof.AdresaStanovanja);

                                break;
                            }
                        }
                    }
                }

                this.Close();
            }
            else
            {
                if (app.getCultureInfo() == SRB)
                    MessageBox.Show("Morate selektovati minimalno jedna predmet", "Upozorenje", MessageBoxButton.OK);
                else
                    MessageBox.Show("You have to select at least one subject", "Warning", MessageBoxButton.OK);
            }
        }
    }
}
