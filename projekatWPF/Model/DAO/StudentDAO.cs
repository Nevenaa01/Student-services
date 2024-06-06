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
    class StudentDAO : ISubject
    {
        private readonly List<IObserver> _observers;

        private readonly StudentStorage _storage;
        private readonly List<Student> _studenti;

        public StudentDAO()
        {
            _storage = new StudentStorage();
            _studenti = _storage.Load();
            _observers = new List<IObserver>();
        }


        public void Add(Student student)
        {
            _studenti.Add(student);
            _storage.Save(_studenti);
            NotifyObservers();
        }

        public void Remove(Student student)
        {
            _studenti.Remove(student);
            _storage.Save(_studenti);
            NotifyObservers();
        }


        public bool Izmeni(string prezime, string ime, DateOnly datumRodjenja, string telefon, string email, string brIndeksa, int godinaUpisa, string trenutnaGodinaStudija, Status status, double prosecnaOcena, string adresaStanovanja)
        {
            Student izmena = _studenti.Find(s => s.BrIndeksa == brIndeksa);
            if (izmena == null)
                MessageBox.Show("Ne mozes menjati indeks.");
            else
            {
                string[] adr = adresaStanovanja.Split(",");
                Adresa adresa = new Adresa(adr[0], adr[1], adr[2], adr[3]);
                izmena.Ime = ime;
                izmena.Prezime = prezime;
                izmena.DatumRodjenja = datumRodjenja;
                izmena.Status = (status == Status.BUDŽET) ? "BUDŽET" : "SAMOFINANSIRANJE";
                izmena.Email = email;
                izmena.Telefon = telefon;
                izmena.BrIndeksa = brIndeksa;
                izmena.GodinaUpisa = godinaUpisa;
                izmena.TrenutnaGodinaStudija = trenutnaGodinaStudija;
                izmena.AdresaStanovanja = (adresa.Ulica + "," + adresa.Broj + "," + adresa.Grad + "," + adresa.Drzava);
                _storage.Save(_studenti);
                NotifyObservers();
                return true;
            }
            return false;
        }
        public void Dodaj_predmet(string indeks, Predmet p)
        {
            Student izmena = _studenti.Find(s => s.BrIndeksa == indeks);
            if (izmena == null)
                MessageBox.Show("Ne moze da se doda predmet.");
            else
            {
                izmena.NepolozeniIspiti.Add(p);
                _storage.Save(_studenti);
                NotifyObservers();
            }
        }


        public void Save(List<Student> studenti)
        {
            _storage.Save(studenti);
        }

        public void Ponisti_ocenu(string p, string s,Ocena o)
        {
            Student student = _studenti.Find(st => st.BrIndeksa == s);
            PredmetController pc = new PredmetController();
            Predmet predmet = pc.GetAllPredmeti().Find(pr => pr.Sifra.ToString() == p);
            student.PolozeniIspiti.Remove(student.Nadji_kljuc(p));

            student.NepolozeniIspiti.Add(predmet);
            _storage.Save(_studenti);
            NotifyObservers();
        }
        public void Polozio(string s, string p, Ocena o) 
        {
            Student student = _studenti.Find(st=> st.BrIndeksa==s);
            PredmetController pc = new PredmetController();
            Predmet predmet = pc.GetAllPredmeti().Find(pr=>pr.Sifra.ToString()==p);
            student.PolozeniIspiti.Add(predmet,o);
            student.NepolozeniIspiti.RemoveAll(pr=>pr.Sifra.ToString()==p);
            _storage.Save(_studenti);
            NotifyObservers();
        }
        public ObservableCollection<Predmet> Nepolozeni( ObservableCollection<Predmet> predmeti, Student student) 
        {
            if (student.SifraNepolozenihPredmeta().Length > 0)
            {
                string[] nepolozeni_predmeti = student.SifraNepolozenihPredmeta().Split(",");
                ObservableCollection<Predmet> nepolozeni = new ObservableCollection<Predmet>();
                foreach (Predmet p in predmeti)
                    for (int i = 0; i < nepolozeni_predmeti.Length; i++)
                        if (p.Sifra.ToString() == nepolozeni_predmeti[i])
                            nepolozeni.Add(p);
                return nepolozeni;
            }
            else
                return null;
        }
        public List<Student> GetAll()
        {
            return _studenti;
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
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
