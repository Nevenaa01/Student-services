using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Observer;
using projekatWPF.Storage;
using projekatWPF.Model;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using projekatWPF.Controller;

namespace projekatWPF.Model.DAO
{
    class PredmetDAO : ISubject
    {
        private readonly List<IObserver> _observers;

        private readonly PredmetStorage _storage;
        private readonly ProfesorStorage _profstorage;
        private readonly List<Predmet> _predmeti;

        public PredmetDAO()
        {
            _storage = new PredmetStorage();
            _profstorage=new ProfesorStorage();
            _predmeti = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Add(Predmet predmet)
        {
            _predmeti.Add(predmet);
            _storage.Save(_predmeti);
            NotifyObservers();
        }

        public void Remove(Predmet predmet)
        {
            _predmeti.Remove(predmet);
            _storage.Save(_predmeti);
            NotifyObservers();
        }

        public bool Izmeni(string sifra, string naziv, int espb, string semestar, int godina)
        {
            Predmet izmena = _predmeti.Find(p => p.Sifra == sifra);
            if (izmena == null)
                MessageBox.Show("Ne mozes menjati sifru predmeta");
            else
            {
                izmena.Naziv = naziv;
                izmena.Sifra = sifra;
                izmena.ESPB = espb;
                izmena.GodinaIzvodjenja = godina;
                if (semestar.ToUpper() == "ZIMSKI")
                    izmena.Semestar = Semestar.ZIMSKI;
                else
                    izmena.Semestar = Semestar.LETNJI;
                _storage.Save(_predmeti);
                NotifyObservers();
                return true;
            }
            return false;
        }
        public List<Predmet> GetAll()
        {
            return _predmeti;
        }

        public void Save(List<Predmet> predmet)
        {
            _storage.Save(predmet);
        }

        public ObservableCollection<Predmet> Moguci_Predmeti(ObservableCollection<Predmet> predmeti, Student student)
        {
            string[] polozeni = student.SifraPolozenihIspita().Split(",");
            string[] nepolozeni = student.SifraNepolozenihPredmeta().Split(",");
            ObservableCollection<Predmet> svi = new ObservableCollection<Predmet>();
            foreach (Predmet p in predmeti)
            {
                bool pol=false, nepol=false;
                for (int i = 0; i < polozeni.Length; i++)
                    if (p.Sifra.ToString() == polozeni[i])
                        pol = true;
                for (int i = 0; i < nepolozeni.Length; i++)
                    if (p.Sifra.ToString() == nepolozeni[i])
                        nepol = true;
                if (!nepol && !pol)
                {
                    if (student.Koja_godina() >= p.GodinaIzvodjenja)
                        svi.Add(p);
                }
            }
            return svi;
        }

        public void Postavljanje_profesora(string prof, string predmet, List<Profesor> Profesori, List<Predmet> Predmeti)
        {
            Predmet izmena = Predmeti.Find(pred => pred.Sifra.ToString() == predmet);
            Profesor profesor =Profesori.Find(pro=>pro.BrojLicneKarte==prof);
            izmena.Profesor = profesor;
            NotifyObservers();
        }
        /*public void Postavljanje_profesora_prividno(string prof, string predmet)
        {
            Predmet izmena = _predmeti.Find(pred => pred.Sifra.ToString() == predmet);
            ProfesorController pc = new ProfesorController();
            Profesor profesor = pc.GetAllProfesors().Find(pro => pro.BrojLicneKarte == prof);
            izmena.Profesor = profesor;
        }*/
        public void Uklanjanje_profesora(string predmet, List<Predmet> Predmeti)
        {
            Predmet izmena =Predmeti.Find(pred => pred.Sifra.ToString() == predmet);
            Profesor p = new Profesor(); 
            izmena.Profesor = p;//vidi da li radi
            izmena.Profesor.ImePrezime = string.Empty;
            NotifyObservers();
        }
        /*
        public void Vrati_profesora(Profesor profesor, Predmet predmet)
        {
            Predmet izmena = _predmeti.Find(pred => pred.Sifra.ToString() == predmet.Sifra.ToString());
            izmena.Profesor = profesor;
            _storage.Save(_predmeti);
            NotifyObservers();
        }
        public void Uklanjanje_profesora_prividno(string predmet)
        {
            Predmet izmena = _predmeti.Find(pred => pred.Sifra.ToString() == predmet);
            Profesor p = new Profesor();
            izmena.Profesor = p;//vidi da li radi
            izmena.Profesor.ImePrezime = "";
        }*/
        public void Dodaj_Studenta(Student s,Predmet p)
        {
            Predmet izmena = _predmeti.Find(pred=> pred.Sifra==p.Sifra);
            izmena.dodajNisuPolozili(s);
            _storage.Save(_predmeti);
            NotifyObservers();
        }
        public Predmet Nadji_Predmet(string sifra)
        {
            try
            {
                Predmet p = _predmeti.Find(pred => pred.Sifra.ToString() == sifra);
                return p;
            }
            catch
            {
                MessageBox.Show("Greska pri izboru predmeta!");
                Predmet p = new Predmet();
                return  p;
            }
        }

        public void Ponisti_ocenu(string p, string s)
        {
            Predmet predemt = Nadji_Predmet(p);
            StudentController sc = new StudentController();
            Student student = sc.GetAllStudente().Find(st => st.BrIndeksa == s);

            predemt.dodajNisuPolozili(student);
            predemt.Polozili_predmet.RemoveAll(st => st.BrIndeksa == student.BrIndeksa);
            _storage.Save(_predmeti);
            NotifyObservers();
        }
        public void Uredi_polaganje(string p, string s) 
        {
            Predmet predemt = Nadji_Predmet(p);
            StudentController sc = new StudentController();
            Student student = sc.GetAllStudente().Find(st=> st.BrIndeksa==s);
            predemt.dodajPolozili(student);
            predemt.Nisu_polozili_predmet.RemoveAll(st => st.BrIndeksa == student.BrIndeksa);
            _storage.Save(_predmeti);
            NotifyObservers();
        }
        public void Unsubscribe(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update();
            }
        }
    }
}
