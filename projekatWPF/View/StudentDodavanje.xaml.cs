using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Serialization;
using projekatWPF;
using projekatWPF.Controller;
using projekatWPF.Model;

namespace projekatWPF.View
{
    /// <summary>
    /// Interaction logic for StudentDodavanje.xaml
    /// </summary>
    public partial class StudentDodavanje : Window, INotifyPropertyChanged
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        private readonly StudentController _controller;

        public Student Student { get; set; }
   
        public StudentDodavanje(StudentController controller)
        {
            InitializeComponent();
            this.DataContext = this;  //?

            app = (App)Application.Current;

            Student = new Student();

            _controller = controller;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
                result = MessageBox.Show("Da li sigurno zelite da dodate novog studenta?", "Provera", MessageBoxButton.YesNo);
            else
                result = MessageBox.Show("Are you sure you want to add new student?", "Warning", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                {
                    if (Provera_Indeksa())
                        MessageBox.Show("Postoji taj broj indeksa!", "Upozorenje", MessageBoxButton.OK);
                    else
                    {
                        if (Provera_Adrese())
                            MessageBox.Show("Postoji ta adresa!", "Upozorenje", MessageBoxButton.OK);
                        else
                        {
                                _controller.Create(Student);
                                this.Close();
                        }
                    }
                }
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tTrenutnaGodinaStudija.SelectedIndex = 0;
            tStatus.SelectedIndex = 0;
        }

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
        private bool Provera_Indeksa()
        {
            List<Student> students = _controller.GetAllStudente();
            List<string> indeksi = new List<string>();
            foreach (Student s in students)
                indeksi.Add(s.BrIndeksa);

            foreach (string s in indeksi)
            {
                if (s.Equals(Student.BrIndeksa))
                    return true;
            }
            return false;
        }

        private bool Provera_Adrese()
        {
            List<Adresa> adrese = _controller.GetAllAdrese();
            foreach (Adresa a in adrese)
                if (a.ToString().Equals(Student.AdresaStanovanja))
                    return true;
            return false;
        }

        
    }
}
