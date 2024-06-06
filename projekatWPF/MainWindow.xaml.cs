using projekatWPF.Controller;
using projekatWPF.Model;
using projekatWPF.Model.DAO;
using projekatWPF.Observer;
using projekatWPF.Storage;
using projekatWPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace projekatWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private App app;
        private const string SRB = "sr-Latn-RS";
        private const string ENG = "en-US";

        public static string brindexa;
        public static string brlicne;
        public static string sifra;

        private readonly StudentController _studentController;
        private readonly ProfesorController _profesorController;
        private readonly PredmetController _predmetController;
        private readonly KatedraController _katedraController;
        private readonly OcenaController _ocenaController;
        private readonly AdresaController _adresaController;
        public ObservableCollection<Student> Students { get; }
        public Student SelectedStudent { get; set; }
        public ObservableCollection<Profesor> Profesors { get; set; }
        public Profesor SelectedProfesor { get; set; }
        public ObservableCollection<Predmet> Predmets { get; set; }
        public Predmet SelectedPredmet { get; set; }
        public ObservableCollection<Katedra> Katedras { get; set; }
        public ObservableCollection<Ocena> Ocenas { get; set; }
        public ObservableCollection<Adresa> Adresas { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            app = (App)Application.Current;
            app.ChangeLanguage(SRB);

            _studentController = new StudentController();
            _profesorController = new ProfesorController();
            _predmetController = new PredmetController();
            _katedraController = new KatedraController();
            _ocenaController = new OcenaController();
            _adresaController = new AdresaController();

            _studentController.Subscribe(this);
            _predmetController.Subscribe(this);
            _profesorController.Subscribe(this);
            _katedraController.Subscribe(this);
            _ocenaController.Subscribe(this);
            _adresaController.Subscribe(this);


            Students = new ObservableCollection<Student>(_studentController.GetAllStudente());
            Profesors = new ObservableCollection<Profesor>(_profesorController.GetAllProfesors());
            Predmets = new ObservableCollection<Predmet>(_predmetController.GetAllPredmeti());
            Katedras = new ObservableCollection<Katedra>(_katedraController.GetAllKatedre());
            Ocenas = new ObservableCollection<Ocena>(_ocenaController.GetAllLOcene());
            Adresas = new ObservableCollection<Adresa>(_adresaController.GetAllAdrese());


            DispatcherTimer LiveTime = new DispatcherTimer();
            LiveTime.Interval = TimeSpan.FromSeconds(1);
            LiveTime.Tick += timer_Tick;
            LiveTime.Start();

            this.Focus();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            StatusBarDatumIVreme.Content = DateTime.Now.ToString("HH:mm:ss") + "\t" + DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void MenuItem_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            this.tabovi.SelectedIndex = 0;
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.tabovi.SelectedIndex = 1;
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.tabovi.SelectedIndex = 2;
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            this.tabovi.SelectedIndex = 3;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4;
            this.Height = System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4;
            this.Top = System.Windows.SystemParameters.PrimaryScreenHeight / 2 - this.Height / 2;
            this.Left = System.Windows.SystemParameters.PrimaryScreenWidth / 2 - this.Width / 2;
            status.Width = this.Width - 18;
            tulbar.Width = this.Width - 18;
            tabovi.Width = this.Width - 18;
            tabovi.Height = this.Height - tulbar.Height - meni.Height - status.Height - 38;
            studdata.Width = tabovi.Width * 3 / 4;
            studdata.Height = tabovi.Height * 3 / 4;
            profdata.Height = tabovi.Height * 3 / 4;
            profdata.Width = tabovi.Width * 3 / 4;
            preddata.Height = tabovi.Height * 3 / 4;
            preddata.Width = tabovi.Width * 3 / 4;
            doktul.Width = tulbar.Width - 25;
            Izgled(studdata);
            Izgled(profdata);
            Izgled(preddata);
        }

        private void MenuItem_Click_New(object sender, RoutedEventArgs e)
        {
            if(this.tabovi.SelectedIndex == 0)
            {
                Window dodavanjeStudenta = new StudentDodavanje(_studentController);
                dodavanjeStudenta.Owner = this;
                dodavanjeStudenta.Show();
            }
            else 
                if(this.tabovi.SelectedIndex == 1)
                {
                    var dodavanjeProfesora = new ProfesoriDodavanje(_profesorController);
                    dodavanjeProfesora.Owner = this;
                    dodavanjeProfesora.Show();
                }
                else
                    if (this.tabovi.SelectedIndex == 2)
                    {
                        var dodavanjePredmeta = new PredmetiDodavanje(_predmetController);
                        dodavanjePredmeta.Owner = this;
                        dodavanjePredmeta.Show();
                    }
        }

        private void plus_Click(object sender, RoutedEventArgs e)
        {
            if (this.tabovi.SelectedIndex == 0)
            {
                var dodavanjeStudenta = new StudentDodavanje(_studentController);
                dodavanjeStudenta.Owner = this;
                dodavanjeStudenta.Show();
            }
            else
                if (this.tabovi.SelectedIndex == 1)
                {
                    var dodavanjeProfesora = new ProfesoriDodavanje(_profesorController);
                    dodavanjeProfesora.Owner = this;
                    dodavanjeProfesora.Show();
                }
                else
                    if (this.tabovi.SelectedIndex == 2)
                    {
                        var dodavanjePredmeta = new PredmetiDodavanje(_predmetController);
                        dodavanjePredmeta.Owner = this;
                        dodavanjePredmeta.Show();
                    }
        }

        private void olovka_Click(object sender, RoutedEventArgs e)
        {
            if (this.tabovi.SelectedIndex == 0)
            {
                if (SelectedStudent != null)
                {
                    Student brin = SelectedStudent;
                    brindexa = brin.BrIndeksa;
                    var izmenastudenta = new StudentIzmena(_studentController, _ocenaController, _predmetController);
                    izmenastudenta.Owner = this;
                    izmenastudenta.Show();
                }
                else
                {
                    if (app.getCultureInfo() == SRB)
                        MessageBox.Show("Morate selektovati studenta!", "Upozorenje", MessageBoxButton.OK);
                    else
                        MessageBox.Show("You have to select a student!", "Warning", MessageBoxButton.OK);
                }
            }
            if (this.tabovi.SelectedIndex == 1)
            {
                if (SelectedProfesor != null)
                {
                    Profesor brlic = SelectedProfesor;
                    brlicne = brlic.BrojLicneKarte;
                    var izmenaprofesora = new ProfesorIzmena(_profesorController, _katedraController, _predmetController);
                    izmenaprofesora.Owner = this;
                    izmenaprofesora.Show();
                }
                else
                {
                    if (app.getCultureInfo() == SRB)
                        MessageBox.Show("Morate selektovati profesora!", "Upozorenje", MessageBoxButton.OK);
                    else
                        MessageBox.Show("You have to select a professor!", "Warning", MessageBoxButton.OK);
                }
            }
            if (this.tabovi.SelectedIndex == 2)
            {
                if (SelectedPredmet != null)
                {
                    Predmet pred = SelectedPredmet;
                    sifra = pred.Sifra;
                    var izmenapredmeta = new PredmetIzmena(_predmetController, _profesorController);
                    izmenapredmeta.Owner = this;
                    izmenapredmeta.Show();
                }
                else
                {
                    if (app.getCultureInfo() == SRB)
                        MessageBox.Show("Morate selektovati predmet!", "Upozorenje", MessageBoxButton.OK);
                    else
                        MessageBox.Show("You have to select a subject!", "Warning", MessageBoxButton.OK);
                }
            }
        }
        private void UpdateStudentsList()
        {
            Students.Clear();
            foreach (var student in _studentController.GetAllStudente())
            {
                student.ProsecnaOcena = student.Prosek();
                Students.Add(student);
            }
        }
        private void UpdateProfesorsList()
        {
            Profesors.Clear();
            foreach (var prof in _profesorController.GetAllProfesors())
                Profesors.Add(prof);
        }
        public void Izgled(DataGrid d)
        {
            for (int i = 0; i < d.Columns.Count; i++)
                d.Columns[i].Width = (d.Width) / d.Columns.Count;
        }

        public void UpadtePredmetiList()
        {
            Predmets.Clear();
            foreach (var pred in _predmetController.GetAllPredmeti())
            {
                Predmets.Add(pred);
            }
        }
        public void Update()
        {
            UpdateStudentsList();
            UpdateProfesorsList();
            UpadtePredmetiList();
            this.Focus();
        }

        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (this.tabovi.SelectedIndex == 0)
            {
                MessageBoxResult result;
                if (app.getCultureInfo() == SRB)
                    result = MessageBox.Show("Da li sigurno zelite da obrišete studenta?", "Provera", MessageBoxButton.YesNo);
                else
                    result = MessageBox.Show("Are you sure you want to delete student", "Warning", MessageBoxButton.YesNo);

                    if(result == MessageBoxResult.Yes)
                    {
                        if (SelectedStudent != null)
                        {
                            _studentController.Delete(SelectedStudent);
                        }
                        else
                        {
                            if (app.getCultureInfo() == SRB)
                                MessageBox.Show("Morate prvo izabrati studenta kog želite da obrišete, bolan!");
                            else
                                MessageBox.Show("You have to select a studnet you want to delete, bolan(al na eng)");
                        }
                    }
            }
            else
                if(this.tabovi.SelectedIndex == 1)
                {
                    MessageBoxResult result = MessageBox.Show("Da li sigurno zelite da obrišete profesora?", "Provera", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    /*if (SelectedProfesor != null)
                    {
                        int i = 0;
                        foreach(Predmet predmet in Predmets)
                        {
                            if(predmet.Profesor.BrojLicneKarte == SelectedProfesor.BrojLicneKarte)
                            {
                                MessageBox.Show("Ne možete obrisati izabranog profesora zato što je on šef katedre");
                                break;
                            }
                            i++;
                        }

                    if(i == Predmets.Count)*/
                    _profesorController.Delete(SelectedProfesor);

                }
                else
                {
                    MessageBox.Show("Morate prvo izabrati profesora kog želite da obrišete!");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Da li sigurno zelite da obrišete predmet?", "Provera", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    if (SelectedPredmet != null)
                    {
                        _predmetController.Delete(SelectedPredmet);
                    }
                    else
                    {
                        MessageBox.Show("Morate prvo izabrati predmet kog želite da obrišete!");
                    }
                }
            }
        }

        private void glavni_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.N))
                MenuItem_Click_New(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.S))
                MenuItem_Click_Save(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.C))
                MenuItem_Click_Close(sender, e);
            if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.D))
                MenuItem_Click_Delete(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.E))
                MenuItem_Click_Edit(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.A))
                MenuItem_Click_About(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.R))
                MenuItem_Click_Serbian(sender, e);
            else if (Keyboard.IsKeyDown(Key.LeftCtrl) && Keyboard.IsKeyDown(Key.G))
                MenuItem_Click_English(sender, e);
        }

        private void MenuItem_Click_Save(object sender, RoutedEventArgs e)
        {
            _studentController.Save(Students.ToList());
            _profesorController.Save(Profesors.ToList());
            _predmetController.Save(Predmets.ToList());
            _ocenaController.Save(Ocenas.ToList());
            _katedraController.Save(Katedras.ToList());
            _adresaController.Save(Adresas.ToList());
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            Button_Click_Delete(sender, e);
        }

        private void MenuItem_Click_Edit(object sender, RoutedEventArgs e)
        {
            olovka_Click(sender, e);
        }

        private void MenuItem_Click_About(object sender, RoutedEventArgs e)
        {
            if (app.getCultureInfo() == SRB)
                MessageBox.Show("Radili:\n\tNevena Gligorov RA7/2020\n\tStrahinja Banjanac RA3/2020", "Informacije", MessageBoxButton.OK);
            else
                MessageBox.Show("Did a fine job:\n\tNevena Gligorov RA7/2020\n\tStrahinja Banjanac RA3/2020", "Information", MessageBoxButton.OK);
        }

        private void tabovi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabovi.SelectedIndex == 0)
            {
                if (app.getCultureInfo() == SRB)
                    StatusBarTab.Content = "Studenti";
                else
                    StatusBarTab.Content = "Students";
            }
            else if (tabovi.SelectedIndex == 1)
            {
                if (app.getCultureInfo() == SRB)
                    StatusBarTab.Content = "Profesori";
                else
                    StatusBarTab.Content = "Professors";
            }
            else if (tabovi.SelectedIndex == 2)
            {
                if (app.getCultureInfo() == SRB)
                    StatusBarTab.Content = "Predmeti";
                else
                    StatusBarTab.Content = "Subjects";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tabovi.SelectedIndex == 0)
            {
                if (string.IsNullOrEmpty(tSearchBar.Text))
                {
                    studdata.ItemsSource = Students;
                }
                else
                {
                    string[] pretraga = tSearchBar.Text.Split(',');
                    int i = 0;
                    foreach (string s in pretraga)   //uklanjanje space-ova pre/posle zareza
                        pretraga[i++] = s.Trim();

                    if (pretraga.Length == 1)    //uneto je samo prezime studenta
                    {
                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);
                        pretraga[0] = regex.Replace(pretraga[0], " ");

                        List<Student> filtriraniStudenti = Students.Where(s => s.Prezime.ToUpper().Contains(pretraga[0].ToUpper())).ToList();
                        studdata.ItemsSource = filtriraniStudenti;

                        TextInfo textInfo = new CultureInfo("sr-Latn-CS", false).TextInfo;
                        pretraga[0] = textInfo.ToTitleCase(pretraga[0].ToLower());

                        tSearchBar.Text = pretraga[0];
                    }
                    else if (pretraga.Length == 2)   //uneto je prezime i ime studenta
                    {
                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);

                        pretraga[0] = regex.Replace(pretraga[0], " ");
                        pretraga[1] = regex.Replace(pretraga[1], " ");


                        List<Student> filtriraniStudenti = Students.Where(s => s.Prezime.ToUpper().Contains(pretraga[0].ToUpper()) && s.Ime.ToUpper().Contains(pretraga[1].ToUpper())).ToList();
                        studdata.ItemsSource = filtriraniStudenti;

                        TextInfo textInfo = new CultureInfo("sr-Latn-CS", false).TextInfo;
                        pretraga[1] = textInfo.ToTitleCase(pretraga[1].ToLower());
                        pretraga[0] = textInfo.ToTitleCase(pretraga[0].ToLower());

                        tSearchBar.Text = pretraga[1] + " " + pretraga[0];
                    }
                    else   //uneti su indeks, prezime i ime studenta
                    {
                        RegexOptions options = RegexOptions.None;
                        Regex regex = new Regex("[ ]{2,}", options);

                        pretraga[0] = regex.Replace(pretraga[0], " ");
                        pretraga[1] = regex.Replace(pretraga[1], " ");
                        pretraga[2] = regex.Replace(pretraga[2], " ");

                        List<Student> filtriraniStudenti = Students.Where(s => s.BrIndeksa.ToUpper().Contains(pretraga[0].ToUpper()) && s.Ime.ToUpper().Contains(pretraga[1].ToUpper()) && s.Prezime.ToUpper().Contains(pretraga[2].ToUpper())).ToList();
                        studdata.ItemsSource = filtriraniStudenti;

                        TextInfo textInfo = new CultureInfo("sr-Latn-CS", false).TextInfo;
                        pretraga[1] = textInfo.ToTitleCase(pretraga[1].ToLower());
                        pretraga[2] = textInfo.ToTitleCase(pretraga[2].ToLower());

                        tSearchBar.Text = pretraga[2] + " " + pretraga[1] + " " + pretraga[0].ToUpper();

                        /*foreach (Student s in studenti)
                        {
                            if (s.BrIndeksa.ToUpper() == pretraga[0].ToUpper() && s.Ime.ToUpper() == pretraga[1].ToUpper() && s.Prezime.ToUpper() == pretraga[2].ToUpper())
                                Students.Add(s);
                        }*/
                    }


                }
            }
            else
            {
                if (tabovi.SelectedIndex == 1)
                {
                    string pretraga = tSearchBar.Text.ToLower();
                    if (string.IsNullOrEmpty(pretraga))
                        profdata.ItemsSource = Profesors;
                    else
                    {
                        string[] reci = new string[3];
                        List<Profesor> filtrirani_Profesori = new List<Profesor>();
                        if (pretraga.Contains(","))
                        {
                            reci = pretraga.Split(",");
                            for (int i = 0; i < reci.Length; i++)
                            {
                                reci[i] = reci[i].TrimStart();
                                reci[i] = reci[i].TrimEnd();
                            }
                            if (reci.Length == 2)
                            {
                                //if (reci[1].Contains(" "))
                                //{
                                    string[] imena = reci[1].Split(" ");
                                    string[] prezimena = reci[0].Split(" ");

                                    string[] imenapom = new string[10];
                                    string[] prezimenapom = new string[10];
                                    int j = 0;
                                    for (int i = 0; i < imena.Length; i++)
                                    {
                                        imena[i] = imena[i].TrimEnd();
                                        imena[i] = imena[i].TrimStart();
                                        if (imena[i] != "")
                                            imenapom[j++] = imena[i];
                                    }
                                    int k = 0;
                                    for (int i = 0; i < prezimena.Length; i++)
                                    {
                                        prezimena[i] = prezimena[i].TrimEnd();
                                        prezimena[i] = prezimena[i].TrimStart();
                                        if (prezimena[i] != "")
                                            prezimenapom[k++] = prezimena[i];
                                    }
                                    foreach (Profesor p in _profesorController.GetAllProfesors())
                                    {
                                        bool provera_imena = false;
                                        bool provera_prezimena = false;
                                        string[] imenaprof = p.Ime.ToLower().Split(" ");

                                        if (imenaprof.Length >= j)
                                        {
                                            for (int i = 0; i < j; i++)
                                            {
                                                if (imenaprof[i].Contains(imenapom[i]))
                                                    provera_imena = true;
                                                else
                                                    provera_imena = false;
                                            }

                                        }
                                        if (j == 0)
                                            provera_imena = true;
                                        string[] prezimenaprof = p.Prezime.ToLower().Split(" ");

                                        if (prezimenaprof.Length >= k)
                                            for (int i = 0; i < k; i++)
                                            {
                                                if (prezimenaprof[i].Contains(prezimenapom[i]))
                                                    provera_prezimena = true;
                                                else
                                                    provera_prezimena = false;
                                            }

                                        if (k == 0)
                                            provera_prezimena = true;
                                        if (provera_imena && provera_prezimena)
                                            filtrirani_Profesori.Add(p);
                                    }

                                    profdata.ItemsSource = filtrirani_Profesori;
                                //}
                            }
                        }
                        else
                        {
                            pretraga = pretraga.TrimStart();
                            pretraga = pretraga.TrimEnd();
                            string[] prezProf = pretraga.Split(" ");
                            string[] prezimena_profesora=new string[10];
                            int brojac = 0;
                            for (int i = 0; i < prezProf.Length; i++)
                            {
                                if (prezProf[i] != "")
                                    prezimena_profesora[brojac++] = prezProf[i];
                            }
                            
                            foreach (Profesor p in _profesorController.GetAllProfesors())
                            {
                                string[] Prof_prezimena = p.Prezime.Split(" ");
                                bool prezime = false;
                                if (Prof_prezimena.Length >= brojac)
                                {
                                    for (int i = 0; i < brojac; i++)
                                    {
                                        if (Prof_prezimena[i].ToLower().Contains(prezimena_profesora[i].ToLower()))
                                            prezime = true;
                                        else
                                            prezime = false;
                                    }
                                }
                                if(brojac==0)
                                    prezime=true;
                                if (prezime)
                                {
                                    filtrirani_Profesori.Add(p);
                                }
                            }
                            profdata.ItemsSource = filtrirani_Profesori;
                        }
                    }
                }
                else
                {
                    if (tabovi.SelectedIndex == 2)
                    {
                        string pretraga = tSearchBar.Text.ToLower();
                        if (string.IsNullOrEmpty(pretraga))
                            preddata.ItemsSource = Predmets;
                        else
                        {
                            string[] reci = new string[2];
                            List<Predmet> filtrirani_Predmeti = new List<Predmet>();
                            if (pretraga.Contains(","))
                            {
                                reci = pretraga.Split(",");
                                for (int i = 0; i < reci.Length; i++)
                                {
                                    reci[i] = reci[i].TrimStart();
                                    reci[i] = reci[i].TrimEnd();
                                }
                                if (reci.Length == 2)
                                {
                                    bool naziv = false, sifra = false;
                                    foreach (Predmet p in _predmetController.GetAllPredmeti())
                                    {
                                        if (p.Naziv.ToLower().Contains(reci[0]))
                                            naziv = true;
                                        if (p.Sifra.ToString().ToLower().Contains(reci[1]))
                                            sifra = true;
                                        if (sifra && naziv)
                                        {
                                            filtrirani_Predmeti.Add(p);

                                        }
                                        naziv = sifra = false;
                                    }
                                    preddata.ItemsSource = filtrirani_Predmeti;
                                }
                            }
                            else
                            {
                                pretraga = pretraga.TrimStart();
                                pretraga = pretraga.TrimEnd();
                                bool naziv = false;
                                string[] nazivi;
                                string[] nazivipom=new string[10];
                                if (pretraga.Contains(" "))
                                {
                                    nazivi = pretraga.Split(" ");
                                    int j = 0;
                                    for (int i = 0; i < nazivi.Length; i++)
                                    {
                                        nazivi[i]=nazivi[i].TrimEnd();
                                        nazivi[i] = nazivi[i].TrimStart();
                                        if (nazivi[i]!="")
                                            nazivipom[j++]=nazivi[i];
                                    }
                                    
                                    bool[] prevanaz = new bool[nazivi.Length];
                                    bool konacan = false,provera=true;
                                    foreach (Predmet p in _predmetController.GetAllPredmeti())
                                    {
                                        string[] nazivipred;
                                        if (p.Naziv.Contains(" "))
                                        {
                                            nazivipred = p.Naziv.ToLower().Split(" ");
                                            if (j > nazivipred.Length)
                                                provera=false;
                                            if (provera)
                                            {
                                                for (int i = 0; i < j; i++)
                                                    if (nazivipred[i].Contains(nazivipom[i]))
                                                        prevanaz[i] = true;
                                                for (int i = 0; i < j; i++)
                                                {
                                                    if (prevanaz[i])
                                                        konacan = true;
                                                    else
                                                    {
                                                        konacan = false;
                                                        break;
                                                    }
                                                }
                                            }
                                            if (konacan)
                                                 filtrirani_Predmeti.Add(p);
                                            for (int i = 0; i < j; i++)
                                                 prevanaz[i] = false;
                                            konacan = false;
                                            provera = true;
                                            
                                        }
                                        else
                                        {
                                            if (p.Naziv.ToLower().Contains(pretraga))
                                                naziv = true;
                                            if (naziv)
                                            {
                                                filtrirani_Predmeti.Add(p);

                                            }
                                            naziv = false;
                                        }
                                    }
                                    preddata.ItemsSource = filtrirani_Predmeti;

                                }
                                foreach (Predmet p in _predmetController.GetAllPredmeti())
                                {
                                    if (p.Naziv.ToLower().Contains(pretraga))
                                        naziv = true;
                                    if (naziv)
                                    {
                                        filtrirani_Predmeti.Add(p);

                                    }
                                    naziv = false;
                                }
                                preddata.ItemsSource = filtrirani_Predmeti;
                            }
                        }
                    }
                }
            }
           
        }

        private void tSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(tSearchBar.Text))
            {
                studdata.ItemsSource = Students;
            }
            if (string.IsNullOrEmpty(tSearchBar.Text))
            {
                profdata.ItemsSource = Profesors;

            }
            if (string.IsNullOrEmpty(tSearchBar.Text))
            {
                preddata.ItemsSource = Predmets;
            }
        }

        private void MenuItem_Click_Serbian(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(SRB);

            this.Title = "Studentska služba";

            if (tabovi.SelectedIndex == 0)
            {
                ssluzba.Content = "Studentska služba -";
                StatusBarTab.Content = "Studenti";
            }
            else if (tabovi.SelectedIndex == 1)
            {
                ssluzba.Content = "Studentska služba -";
                StatusBarTab.Content = "Profesori";
            }
            else if (tabovi.SelectedIndex == 2)
            {
                ssluzba.Content = "Studentska služba -";
                StatusBarTab.Content = "Predmeti";    
            }
        }

        private void MenuItem_Click_English(object sender, RoutedEventArgs e)
        {
            app.ChangeLanguage(ENG);

            this.Title = "Student services";

            if (tabovi.SelectedIndex == 0)
            {
                ssluzba.Content = "Student services -";
                StatusBarTab.Content = "Students";
            }
            else if (tabovi.SelectedIndex == 1)
            {
                ssluzba.Content = "Student services -";
                StatusBarTab.Content = "Professors";
            }
            else if (tabovi.SelectedIndex == 2)
            {
                ssluzba.Content = "Student services -";
                StatusBarTab.Content = "Predmeti";
            }
        }

        private void columnHeader_Click(object sender, RoutedEventArgs e)
        {
            var columnHeader = sender as DataGridColumnHeader;
            if (columnHeader.TabIndex == 0)
            {
                studdata.CanUserSortColumns = false;
                string[] indexi = new string[Students.Count];
                int i = 0;
                foreach (Student s in Students)
                    indexi[i++] = s.BrIndeksa;

                if (indexi[0].Split('/')[1].CompareTo(indexi[Students.Count - 1].Split('/')[1]) < 0)
                {
                    //MessageBox.Show(indexi[0].Split('/')[1] + "\n" + indexi[Students.Count - 1].Split('/')[1]);
                    for (int prvi = 0; prvi < Students.Count; prvi++)
                    {
                        for (int drugi = 0; drugi < Students.Count; drugi++)
                        {
                            string pom;

                            if (indexi[prvi].Split('/')[1].CompareTo(indexi[drugi].Split('/')[1]) > 0)
                            {
                                pom = indexi[prvi];
                                indexi[prvi] = indexi[drugi];
                                indexi[drugi] = pom;
                            }
                            else if (indexi[prvi].Split('/')[1].CompareTo(indexi[drugi].Split('/')[1]) == 0)
                            {
                                if (indexi[prvi].Split('/')[0].CompareTo(indexi[drugi].Split('/')[0]) > 0 || indexi[prvi].Length > indexi[drugi].Length)
                                {
                                    pom = indexi[prvi];
                                    indexi[prvi] = indexi[drugi];
                                    indexi[drugi] = pom;
                                }
                            }
                        }

                    }
                }
                else
                {
                    for (int prvi = 0; prvi < Students.Count; prvi++)
                    {
                        for (int drugi = 0; drugi < Students.Count; drugi++)
                        {
                            string pom;

                            if (indexi[prvi].Split('/')[1].CompareTo(indexi[drugi].Split('/')[1]) < 0)
                            {
                                pom = indexi[prvi];
                                indexi[prvi] = indexi[drugi];
                                indexi[drugi] = pom;
                            }
                            else if (indexi[prvi].Split('/')[1].CompareTo(indexi[drugi].Split('/')[1]) == 0)
                            {
                                if (indexi[prvi].Split('/')[0].CompareTo(indexi[drugi].Split('/')[0]) < 0 || indexi[prvi].Length < indexi[drugi].Length)
                                {
                                    pom = indexi[prvi];
                                    indexi[prvi] = indexi[drugi];
                                    indexi[drugi] = pom;
                                }
                            }
                        }
                    }

                }

                int brojac = 0;
                List<Student> zaSortiranje = new List<Student>();
                
                    foreach(string ind in indexi)
                        foreach (Student s in Students)
                            if (ind.CompareTo(s.BrIndeksa) == 0)
                            {
                                zaSortiranje.Add(s);
                                break;
                            }

                Students.Clear();

                foreach (Student s in zaSortiranje)
                    Students.Add(s);

                studdata.ItemsSource = null;
                studdata.ItemsSource = Students;

            }
            else
            {
                studdata.CanUserSortColumns = true;
            }
        }
    }
}
