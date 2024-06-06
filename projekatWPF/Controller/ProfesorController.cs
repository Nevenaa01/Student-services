using System;
using System.Collections.Generic;
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
    public class ProfesorController
    {
        private readonly ProfesorDAO _profesori;
        private readonly AdresaDAO _adresa;

        public ProfesorController()
        {
            _profesori = new ProfesorDAO();
            _adresa = new AdresaDAO();
        }

        public List<Profesor> GetAllProfesors()
        {
            return _profesori.GetAll();
        }
        public List<Adresa> GetAllAdrese()
        {
            return _adresa.GetAll();
        }
        public void Create(Profesor profesor)
        {
            _profesori.Add(profesor);
        }

        public void Delete(Profesor profesor)
        {
            _profesori.Remove(profesor);
        }
        public void Postavljanje_predmeta(string prof, string predmet, List<Profesor> Profesori, List<Predmet> Predmeti)
        {
             _profesori.Postavljanje_predmeta(prof, predmet,Profesori,Predmeti);
        }
        /*public void Vrati_predmet(Profesor profesor, Predmet predmet)
        {
            _profesori.Vrati_predmet(profesor,predmet);
        }
        public void Postavljanje_predmeta_prividno(string prof, string predmet)
        {
            _profesori.Postavljanje_predmeta_prividno(prof, predmet);
        }*/
        public void Uklanjanje_predmeta(string prof, string predmet, List<Profesor> Profesori)
        {
            _profesori.Uklanjanje_predmeta(prof, predmet, Profesori);
        }
        /*public void Uklanjanje_predmeta_Prividno(string prof, string predmet)
        {
            _profesori.Uklanjanje_predmeta_Prividno(prof, predmet);
        }*/
        public bool Izmeni(string prezime, string ime, DateOnly datumRodjenja, string telefon, string email, string brlk, int godinestaza, string adresaKancelarije, string zvanje, string adresaStanovanja)
        {
            return _profesori.Izmeni(prezime, ime, datumRodjenja, telefon, email, brlk, godinestaza, adresaKancelarije, zvanje, adresaStanovanja);
        }
        public void Subscribe(IObserver observer)
        {
            _profesori.Subscribe(observer);
        }

        public void Save(List<Profesor> profesori)
        {
            _profesori.Save(profesori);
        }
    }
}
