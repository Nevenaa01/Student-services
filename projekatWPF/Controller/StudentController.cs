using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using projekatWPF.Model;
using projekatWPF.Model.DAO;
using projekatWPF.Observer;
using projekatWPF.Storage;

namespace projekatWPF.Controller
{
    public class StudentController
    {
        private readonly StudentDAO _studenti;
        private readonly AdresaDAO _adresa;

        public StudentController()
        {
            _studenti = new StudentDAO();
            _adresa = new AdresaDAO();
        }

        public List<Student> GetAllStudente()
        {
            return _studenti.GetAll();
        }

        public List<Adresa> GetAllAdrese()
        {
            return _adresa.GetAll();
        }

        public void Create(Student student)
        {
            _studenti.Add(student);
        }

        public void Dodaj_predmet(string indeks, Predmet p)
        {
            _studenti.Dodaj_predmet(indeks, p);
        }

        public void Delete(Student student)
        {
            _studenti.Remove(student);
        }

        public bool Izmena(string prezime, string ime, DateOnly datumRodjenja, string telefon, string email, string brIndeksa, int godinaUpisa, string trenutnaGodinaStudija, Status status, double prosecnaOcena, string adresaStanovanja)
        {
            return _studenti.Izmeni(prezime, ime, datumRodjenja, telefon, email, brIndeksa, godinaUpisa, trenutnaGodinaStudija, status, prosecnaOcena, adresaStanovanja);
        }
        public void Polozio(string s, string p, Ocena o)
        {
            _studenti.Polozio(s, p, o);
        }
        public ObservableCollection<Predmet> Nepolozeni_predmeti(ObservableCollection<Predmet> predmeti, Student s)
        {
            return _studenti.Nepolozeni(predmeti,s);  
        }

        public void Ponisti_ocenu(string p, string s,Ocena o)
        {
            _studenti.Ponisti_ocenu(p, s,o);
        }
        public void Subscribe(IObserver observer)
        {
            _studenti.Subscribe(observer);
        }

        public void Save(List<Student> students)
        {
            _studenti.Save(students);
        }

    }
}
