using projekatWPF.Controller;
using projekatWPF.Model;
using projekatWPF.Model.DTO;
using projekatWPF.Observer;
using projekatWPF.Storage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for StudentIzmena.xaml
    /// </summary>
    public partial class StudentIzmena : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly StudentController _controller;
        private readonly OcenaController _ocenaController;
        private readonly PredmetController _predmetController;
        public static string Izabrani_Predmet;
        private double prosek = 0;
        public ObservableCollection<Ocena> Ocene { get; set; }
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public ObservableCollection<Predmet> Nepolozeni_predmeti { get; set; }
        public ObservableCollection<PredmetOcenaDTO> PredmetiIOcene { get; set; }
        public Ocena OcenaSelektovanog { get; set; }
        public Predmet PredmetSelektovanog { get; set; }
        public Predmet SelectedNepolozenPredmet { get; set; }
        public Student Student { get; set; }
        public PredmetOcenaDTO SelectedOcena { get; set; }
        public StudentIzmena(StudentController controller, OcenaController oceanController, PredmetController predmetController)
        {
            InitializeComponent();
            DataContext = this;  //?
            _controller = controller;
            _controller.Subscribe(this);

            app = (App)Application.Current;

            Student = new Student();
            // MessageBox.Show("");
            tGodinaUpisa.Text = "";

            _ocenaController = oceanController;
            _predmetController = predmetController;

            _ocenaController.Subscribe(this);
            _predmetController.Subscribe(this);

            Ocene = new ObservableCollection<Ocena>(_ocenaController.GetAllLOcene());
            Predmeti = new ObservableCollection<Predmet>(_predmetController.GetAllPredmeti());
            PredmetiIOcene = new ObservableCollection<PredmetOcenaDTO>();

            Inicijalizacija();
            if (_controller.Nepolozeni_predmeti(Predmeti, Student) != null)
                Nepolozeni_predmeti = _controller.Nepolozeni_predmeti(Predmeti, Student);
            else
                Nepolozeni_predmeti = new ObservableCollection<Predmet>();

        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void odustajanje_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li zelite da izadjete?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
                this.Close();
        }

        private void potvrda_Click(object sender, RoutedEventArgs e)
        {
            DateTime dt;
            DateOnly datumr;
            Adresa adresa = new Adresa();
            try
            {
                dt = DateTime.Parse(tDatumRodjenja.Text);
                datumr = new DateOnly(dt.Year, dt.Month, dt.Day);
                adresa = new Adresa(tUlica.Text, tBrojUlice.Text, tGrad.Text, tDrzava.Text);
                Status s;
                if (tStatus.Text.ToLower() == "samofinansiranje")
                    s = Status.SAMOFINANSIRANJE;
                else
                    s = Status.BUDŽET;

                MessageBoxResult result;
                if (app.getCultureInfo() == SRB)
                    result = MessageBox.Show("Da li sigurno zelite da izmenite studenta?", "Provera", MessageBoxButton.YesNo);
                else
                    result = MessageBox.Show("Are you sure you want to chaneg student info?", "Warning", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    if (Student.IsValid)
                    {
                        _controller.Izmena(Student.Prezime, Student.Ime, datumr, Student.Telefon, Student.Email, Student.BrIndeksa, Student.GodinaUpisa, Student.TrenutnaGodinaStudija, s, Student.ProsecnaOcena, adresa.ToString());

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

        private void Inicijalizacija()
        {
            List<Student> studenti = _controller.GetAllStudente();
            Student pravi = new Student();
            if ((pravi = studenti.Find(s => s.BrIndeksa.ToUpper() == MainWindow.brindexa.ToUpper())) != null)
            {
                Student.Ime = pravi.Ime;
                Student.Prezime = pravi.Prezime;
                pravi.DatumPom = new DateTime(pravi.DatumRodjenja.Year, pravi.DatumRodjenja.Month, pravi.DatumRodjenja.Day);
                Student.DatumPom = pravi.DatumPom;
                //tDatumRodjenja.Text = pravi.DatumRodjenja.ToString();

                Student.AdresaStanovanja = pravi.AdresaStanovanja;
                string[] niz = pravi.AdresaStanovanja.Split(',');
                Student.Ulica = niz[0];
                Student.BrojUlice = niz[1];
                Student.Grad = niz[2];
                Student.Drzava = niz[3];
                Student.Email = pravi.Email;
                Student.BrIndeksa = pravi.BrIndeksa;
                Student.Telefon = pravi.Telefon;
                Student.GodinaUpisa = pravi.GodinaUpisa;
                Student.ProsecnaOcena = pravi.ProsecnaOcena;
                Student.TrenutnaGodinaStudija = pravi.TrenutnaGodinaStudija;
                Student.Status = pravi.Status;
                if (Student.Status.ToLower() == "budžet")
                    tStatus.SelectedItem = tStatus.Items[0];
                else
                    tStatus.SelectedItem = tStatus.Items[1];
                prosek = pravi.ProsecnaOcena;
                Student.NepolozeniIspiti = pravi.NepolozeniIspiti;
                Student.PolozeniIspiti = pravi.PolozeniIspiti;
                string indeks = "";
                indeks = MainWindow.brindexa;
            }

            Izgled(polozenidata);
            polozeniPredmeti();
        }

        public void polozeniPredmeti()
        {
            List<Predmet> predmetiStudenta = new List<Predmet>();

            foreach (KeyValuePair<Predmet, Ocena> po in Student.PolozeniIspiti)
            {
                foreach (Predmet p in Predmeti)
                    if (p.Sifra == po.Key.Sifra)
                        predmetiStudenta.Add(p);
            }

            prosek = 0;
            int ESPB = 0;
            foreach (Predmet p in predmetiStudenta)
            {
                //Ocena o = Ocene.Where(o => o.Predmet.Sifra == p.Sifra && o.Student.BrIndeksa==Student.BrIndeksa).ToList()[0];
                List<Ocena> oc=new List<Ocena>();
                foreach (Ocena oce in _ocenaController.GetAllLOcene())
                    oc.Add(oce);

                Ocena o = oc.Find(ocen => ocen.Predmet.Sifra == p.Sifra && ocen.Student.BrIndeksa == Student.BrIndeksa);

                if (o != null)
                {
                    prosek += o.Vrednost;
                    ESPB += p.ESPB;

                    PredmetOcenaDTO dto = new PredmetOcenaDTO(p.Sifra, p.Naziv, p.ESPB, o.Vrednost, o.DatumPolaganja);
                    PredmetiIOcene.Add(dto);
                }
            }

            if(predmetiStudenta.Count != 0)
                prosek /= predmetiStudenta.Count;

            prosek = Math.Round(prosek, 2);

            if (app.getCultureInfo() == SRB)
                lProsekESPB.Content = "Prosečna ocena: " + prosek + ", ukupno ESPB: " + ESPB;
            else
                lProsekESPB.Content = "Average grade: " + prosek + ", ESPB points: " + ESPB;
        }

        public void Izgled(DataGrid d)
        {
            for (int i = 0; i < d.Columns.Count; i++)
                d.Columns[i].Width = (d.Width) / d.Columns.Count;
        }
        public void Update()
        {
            UpadtePredmetiList();
            UpadtePolozeniPredmetiList();
        }
        public void UpadtePredmetiList()
        {
            //if(Nepolozeni_predmeti!=null)
             Nepolozeni_predmeti.Clear();
           // Student = _controller.GetAllStudente().Find(s=> s.BrIndeksa== MainWindow.brindexa);
           if(_controller.Nepolozeni_predmeti(Predmeti, Student) != null)
                foreach (var pred in _controller.Nepolozeni_predmeti(Predmeti, Student))
                {
                    Nepolozeni_predmeti.Add(pred);
                }
        }
        public void UpadtePolozeniPredmetiList()
        {
            //if(Nepolozeni_predmeti!=null)
            PredmetiIOcene.Clear();
            // Student = _controller.GetAllStudente().Find(s=> s.BrIndeksa== MainWindow.brindexa);
            polozeniPredmeti();
        }
        /* public List<Predmet> Svi_Predmeti()
         {
             List<Predmet> svi = new List<Predmet>();
             foreach (Predmet p in Predmeti)
                 svi.Add(p);
             return svi;
         }*/
        private void tIme_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tPrezime_TextChanged(object sender, TextChangedEventArgs e)
        {
            //MessageBox.Show("bio sam ovde");
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tDatumRodjenja_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tUlica_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tTelefon_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tBrIndeksa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tGodinaUpisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tDatumRodjenja_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tBrojUlice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tGrad_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void tDrzava_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Student.IsValid)
                bPotvrdi.IsEnabled = true;
            else
                bPotvrdi.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dodavanjePredmeta = new DodavanjePredmetaStudentu(_predmetController,_controller);
            dodavanjePredmeta.Owner = this;
            dodavanjePredmeta.Show();
        }

        private void polozenidata_Loaded(object sender, RoutedEventArgs e)
        {
            //List<Predmet> predmetiStudenta = Predmeti.Where(p => p.Polozili_predmet.Find(s => s.BrIndeksa == Student.BrIndeksa).BrIndeksa == Student.BrIndeksa).ToList();
            
        }

        private void bObrisi_Click(object sender, RoutedEventArgs e)
        {

            if (SelectedNepolozenPredmet != null)
            {
                if (MessageBox.Show("Da li ste sigurni da zelite da obrisete predmet?", "Upozorenje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Predmet p = Student.NepolozeniIspiti.Find(p => p.Sifra == SelectedNepolozenPredmet.Sifra);
                    Predmet predmetStudentUklanjanje = Predmeti.Where(pom => pom.Sifra == SelectedNepolozenPredmet.Sifra).ToList()[0];
                    Student studentUklanjanje = predmetStudentUklanjanje.Nisu_polozili_predmet.Find(s => s.BrIndeksa == Student.BrIndeksa);

                    predmetStudentUklanjanje.Nisu_polozili_predmet.Remove(studentUklanjanje);
                    Student.NepolozeniIspiti.Remove(p);

                    _controller.Izmena(Student.Prezime, Student.Ime, Student.DatumRodjenja, Student.Telefon, Student.Email, Student.BrIndeksa,
                        Student.GodinaUpisa, Student.TrenutnaGodinaStudija, (Student.Status == "Samofinansiranje") ? Status.SAMOFINANSIRANJE : Status.BUDŽET, Student.ProsecnaOcena, Student.AdresaStanovanja);
                    _predmetController.Izmeni(predmetStudentUklanjanje.Sifra, predmetStudentUklanjanje.Naziv, predmetStudentUklanjanje.ESPB, predmetStudentUklanjanje.Semestar.ToString(), predmetStudentUklanjanje.GodinaIzvodjenja);

                }

            }
            else
                MessageBox.Show("Morate izabrati predmet koji uklanjate!", "Upozorenje", MessageBoxButton.OK);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedNepolozenPredmet != null)
            {
                Izabrani_Predmet = SelectedNepolozenPredmet.Sifra;
                var dodavanjeOcene = new UnosOcene(_predmetController, _controller, _ocenaController);
                dodavanjeOcene.Owner = this;
                dodavanjeOcene.Show();
            }
            else
                MessageBox.Show("Morate selektovati predmet!");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (SelectedOcena != null)
            {
                string socena = SelectedOcena.Sifra.ToString();

                MessageBoxResult result;

                if (app.getCultureInfo() == SRB)
                    result = MessageBox.Show("Da li ste sigurni da zelite da ponistite ocenu?", "Upozorenje", MessageBoxButton.YesNo);
                else
                    result = MessageBox.Show("Are you sure you want to delete grade", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    List<Ocena> oc = new List<Ocena>();
                    foreach (Ocena oce in _ocenaController.GetAllLOcene())
                        oc.Add(oce);

                    Ocena o = oc.Find(ocen => ocen.Predmet.Sifra.ToString() == socena && ocen.Student.BrIndeksa == Student.BrIndeksa);
                    
                    _predmetController.Ponisti_ocenu(socena, Student.BrIndeksa);
                    _controller.Ponisti_ocenu(socena, Student.BrIndeksa,o);
                    _ocenaController.Delete(o);
                }
            }
            else
                MessageBox.Show("Morate selektovati predmet!");
        }
    }
}