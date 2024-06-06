using projekatWPF.Controller;
using projekatWPF.Model;
using projekatWPF.Observer;
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
    /// Interaction logic for UnosOcene.xaml
    /// </summary>
    public partial class UnosOcene : Window,IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly StudentController _studentcontroller;
        private readonly OcenaController _ocenaController;
        private readonly PredmetController _predmetController;
        public Predmet Izabrani_predmet { get; set; }
        public Ocena Ocena { get; set; }
        public UnosOcene(PredmetController _pcon,StudentController _scon, OcenaController _ocon)
        {

            Ocena = new Ocena();
            Ocena.DatumPom = DateTime.Now;
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            _studentcontroller = _scon;
            _predmetController = _pcon;
            _ocenaController = _ocon;
            _ocenaController.Subscribe(this);
            _predmetController.Subscribe(this);
            _studentcontroller.Subscribe(this);
            Izabrani_predmet = _predmetController.Nadji_predmet(StudentIzmena.Izabrani_Predmet.ToString());
            tSifra.IsEnabled = false;
            tNaziv.IsEnabled = false;
            //tOcena.SelectedIndex = 0;
            dPotvrdi.IsEnabled = false;
        }

        public void Update()
        {
        }

        private void dPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            if(app.getCultureInfo() == SRB)
                if (MessageBox.Show("Da li ste sigurni da hocete da upisete ocenu?", "Upozorenje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _ocenaController.Dodaj_Ocenu_Studentu(MainWindow.brindexa,Izabrani_predmet.Sifra.ToString(),Ocena.DatumPom,Ocena.Vrednost);
                    _studentcontroller.Polozio(MainWindow.brindexa, Izabrani_predmet.Sifra.ToString(), Ocena);
                    _predmetController.Uredi_polaganje(Izabrani_predmet.Sifra.ToString(), MainWindow.brindexa);
                    this.Close();
                }
                else { }
            else
            {
                if (MessageBox.Show("Are you sure you wannt to insert that grade?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _ocenaController.Dodaj_Ocenu_Studentu(MainWindow.brindexa, Izabrani_predmet.Sifra.ToString(), Ocena.DatumPom, Ocena.Vrednost);
                    _studentcontroller.Polozio(MainWindow.brindexa, Izabrani_predmet.Sifra.ToString(), Ocena);
                    _predmetController.Uredi_polaganje(Izabrani_predmet.Sifra.ToString(), MainWindow.brindexa);
                    this.Close();
                }
            }
        }
        private void tDatum_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Ocena.IsValid)
                dPotvrdi.IsEnabled = true;
            else
                dPotvrdi.IsEnabled = false;
        }

        private void dOdustani_Click(object sender, RoutedEventArgs e)
        {
            if (app.getCultureInfo() == SRB)
                if (MessageBox.Show("Da li zelite da odustanete?", "Upozorenje", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
                else { }
            else
            {
                if (MessageBox.Show("Are you sure u want to exit", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void tOcena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(tOcena.Items[tOcena.SelectedIndex].ToString().Split(" ")[1]);
            Ocena.Vrednost = int.Parse(tOcena.Items[tOcena.SelectedIndex].ToString().Split(" ")[1]);
            //MessageBox.Show(Ocena.Vrednost.ToString());
            if (Ocena.IsValid)
                dPotvrdi.IsEnabled = true;
            else
                dPotvrdi.IsEnabled = false;
        }
    }
}
