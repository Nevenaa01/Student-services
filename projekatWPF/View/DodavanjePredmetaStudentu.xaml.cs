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
    /// Interaction logic for DodavanjePredmeta.xaml
    /// </summary>
    public partial class DodavanjePredmetaStudentu : Window, IObserver
    {
        private readonly PredmetController _controller;
        private readonly StudentController _studentconroller;
        public ObservableCollection<Predmet> Predmeti { get; set; }
        public ObservableCollection<Predmet> PredmetiPrikaz { get; set; }
        public string SelectedPredmet { get; set; }
        public Student Student { get; set; }
        public DodavanjePredmetaStudentu(PredmetController controller,StudentController scontroller)
        {
            _controller = controller;
            _studentconroller = scontroller;

            _controller.Subscribe(this);
            _studentconroller.Subscribe(this);
            Predmeti =new ObservableCollection<Predmet>(_controller.GetAllPredmeti());
            Student = _studentconroller.GetAllStudente().Find(s => s.BrIndeksa == MainWindow.brindexa);
            PredmetiPrikaz = _controller.Moguci_Predmeti(Predmeti, Student);
            InitializeComponent();
            DataContext = this;
            
        }

        public void Update()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] predmet=SelectedPredmet.Split(" ");
            Predmet p = _controller.GetAllPredmeti().Find(pred => pred.Sifra.ToString() == predmet[0]);
            _studentconroller.Dodaj_predmet(Student.BrIndeksa,p);
            _controller.Dodaj_Studenta(Student,p);
            this.Close();
        }
    }
}
