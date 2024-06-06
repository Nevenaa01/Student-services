using projekatWPF.Controller;
using projekatWPF.Model;
using projekatWPF.Observer;
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

namespace projekatWPF.View
{
    /// <summary>
    /// Interaction logic for DodavanjeProfesoraPredmetu.xaml
    /// </summary>
    public partial class DodavanjeProfesoraPredmetu : Window,IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly ProfesorController _profesorController;
        private readonly PredmetController _predmetController;
        public ObservableCollection<Profesor> Profesori { get; set; }
        public Profesor SelectedProfesor { get; set; }
        public static string brlkrofesora;
        public List<Profesor> Profesori_novi { get; set; }
        public List<Predmet> Predmeti_novi { get; set; }
        public static bool desila_izmena;
        public DodavanjeProfesoraPredmetu(ProfesorController _controller, PredmetController predmetController,List<Profesor> ProfesoriN,List<Predmet> PredmetiN)
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;

            _profesorController = _controller;
            Profesori = new ObservableCollection<Profesor>(_profesorController.GetAllProfesors());
            _profesorController.Subscribe(this);
            _predmetController = predmetController;

            Profesori_novi = ProfesoriN;
            Predmeti_novi = PredmetiN;
            desila_izmena = false;
        }

        public void Update()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li sgirno zelite da dodate profesora?", "Upozorenje", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to add professor?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            { 
            
                 _predmetController.Postavljanje_profesora(SelectedProfesor.BrojLicneKarte, MainWindow.sifra.ToString(),Profesori_novi,Predmeti_novi);
                _profesorController.Postavljanje_predmeta(SelectedProfesor.BrojLicneKarte, MainWindow.sifra.ToString(), Profesori_novi, Predmeti_novi);
                brlkrofesora = SelectedProfesor.BrojLicneKarte;
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result;

            if (app.getCultureInfo() == SRB)
                result = MessageBox.Show("Da li sgirno zelite da izadjete?", "Upozorenje", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to exit", "Warning", MessageBoxButton.YesNo);

            if ( result == MessageBoxResult.Yes)
                this.Close();
        }
    }
}
