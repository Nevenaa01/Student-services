using projekatWPF.Controller;
using projekatWPF.Model;
using projekatWPF.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
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
    /// Interaction logic for PredmetIzmena.xaml
    /// </summary>
    public partial class PredmetIzmena : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly PredmetController _controller;
        private readonly ProfesorController _profesorcontroller;

        public bool izmena { get; set; }

        public List<Predmet> Novi_predmeti { get; set; }
        public List<Profesor> Novi_profesori { get; set; }
        public Predmet Predmet { get; set; }
        //private int izmena_bila { get; set; }
        public PredmetIzmena(PredmetController controller, ProfesorController profesorController)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            _controller = controller;
            _profesorcontroller = profesorController;

            Predmet = new Predmet();

            _controller.Subscribe(this);
            _profesorcontroller.Subscribe(this);

            
            izmena = false;
            Novi_predmeti = new List<Predmet>();
            Novi_profesori = new List<Profesor>();
            Novi_predmeti.Clear();
            Novi_profesori.Clear();
            foreach (Predmet p in _controller.GetAllPredmeti())
                Novi_predmeti.Add(p);
            foreach (Profesor p in _profesorcontroller.GetAllProfesors())
                Novi_profesori.Add(p);
            Inicijalizacija();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (tProfesor.Text!=" ")
            {
                MessageBoxResult result;
                if (app.getCultureInfo() == SRB)
                    result = MessageBox.Show("Da li sigurno zelite da izmenite predmet?", "Provera", MessageBoxButton.YesNo);
                else
                    result = MessageBox.Show("Are you sure you want to change subject info?", "Warning", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    if (Predmet.IsValid)
                    {
                        _controller.Izmeni(tSifra.Text, tNaziv.Text, int.Parse(tESPB.Text), tSemestar.Text, int.Parse(tGodinaIzvodjenja.Text));
                        izmena = true;
                        this.Close();
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult result;
            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li zelite da izadjete?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to exit?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                this.Close();
            } 

        }
        private void Inicijalizacija()
        {
            tProfesor.IsEnabled = false;
            Predmet pravi = Novi_predmeti.Find(p => p.Sifra == MainWindow.sifra);
            Predmet.Naziv = pravi.Naziv;
            Predmet.Sifra = pravi.Sifra;
            Predmet.ESPB = pravi.ESPB;
            Predmet.GodinaIzvodjenja=pravi.GodinaIzvodjenja;
            Predmet.Semestar=pravi.Semestar;
            //MessageBox.Show(_profesorcontroller.GetAllStudents().Find(pr => pr.BrojLicneKarte == pravi.Profesor.BrojLicneKarte).ToString());
            try
            {
                Predmet.Profesor = Novi_profesori.Find(p => p.BrojLicneKarte == pravi.Profesor.BrojLicneKarte);
                if (Predmet.Profesor == null)
                {
                    Predmet.Profesor = new Profesor();
                    Predmet.Profesor.Ime = String.Empty;
                    Predmet.Profesor.Prezime = String.Empty;
                    tProfesor.Text = "";
                }
                    

                if (Predmet.Profesor.Ime!=String.Empty && Predmet.Profesor.Prezime!=String.Empty)
                {
                    bminus.IsEnabled = true;
                    bplus.IsEnabled = false;
                    tProfesor.Text = Predmet.Profesor.ImePrezime;
                }
                else
                {
                    bminus.IsEnabled = false;
                    bplus.IsEnabled = true;
                    tProfesor.Text = "";
                }
            }
            catch
            {
                bminus.IsEnabled = false;
                bplus.IsEnabled = true;
            }
        }

        private void bplus_Click(object sender, RoutedEventArgs e)
        {
            var dodavanjeProfPred = new DodavanjeProfesoraPredmetu(_profesorcontroller,_controller, Novi_profesori, Novi_predmeti);
            dodavanjeProfPred.Owner = this;
            dodavanjeProfPred.Show();
            //izmena_bila = 1;

        }

        public void Update()
        {
            Inicijalizacija(); 
        }

        private void bminus_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da zelite da uklonite profesora sa ovog predmeta?", "Upozorenje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                /*stari_profesor = Predmet.Profesor;
                stari_predmet = Predmet;*/
                _profesorcontroller.Uklanjanje_predmeta(Predmet.Profesor.BrojLicneKarte, MainWindow.sifra.ToString(),Novi_profesori);
                _controller.Uklanjanje_profesora(MainWindow.sifra.ToString(), Novi_predmeti);
               
                //izmena_bila = 2;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (izmena)
            {
                _controller.Save(Novi_predmeti);
                _profesorcontroller.Save(Novi_profesori); 
            }
        }
    }
}
