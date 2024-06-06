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
    public class PredmetController
    {
        private readonly PredmetDAO _predmeti;

        public PredmetController()
        {
            _predmeti = new PredmetDAO();
        }

        public List<Predmet> GetAllPredmeti()
        {
            return _predmeti.GetAll();
        }

        public void Create(Predmet predmet)
        {
            _predmeti.Add(predmet);
        }

        public bool Izmeni(string sifra, string naziv, int espb, string semestar, int godina)
        {
            return _predmeti.Izmeni(sifra, naziv, espb, semestar, godina);
        }
        public ObservableCollection<Predmet> Moguci_Predmeti(ObservableCollection<Predmet> predmeti, Student student)
        {
            return _predmeti.Moguci_Predmeti(predmeti,student);
        }
        public void Delete(Predmet predmet)
        {
            _predmeti.Remove(predmet);
        }
       /* public void Postavljanje_profesora_prividno(string prof, string predmet)
        {
            _predmeti.Postavljanje_profesora_prividno(prof,predmet);
        }*/
        public void Postavljanje_profesora(string prof, string predmet, List<Profesor> Profesori, List<Predmet> Predmeti)
        {
            _predmeti.Postavljanje_profesora(prof, predmet,Profesori,Predmeti);
        }
        public void Uklanjanje_profesora(string predmet,List<Predmet> Predmeti)
        {
            _predmeti.Uklanjanje_profesora(predmet, Predmeti);
        }
        /*public void Uklanjanje_profesora_prividno(string predmet)
        {
            _predmeti.Uklanjanje_profesora_prividno(predmet);
        }*/
        public void Uredi_polaganje(string p, string s)
        {
            _predmeti.Uredi_polaganje(p,s);
        }
        public Predmet Nadji_predmet(string sifra)
        {
            return _predmeti.Nadji_Predmet(sifra);
        }
        public void Dodaj_Studenta( Student s,Predmet p)
        {
            _predmeti.Dodaj_Studenta(s,p);
        }
        /*public void Vrati_profesora(Profesor profesor, Predmet predmet)
        {
            _predmeti.Vrati_profesora(profesor, predmet);
        }*/
        public void Ponisti_ocenu(string p, string s)
        {
            _predmeti.Ponisti_ocenu(p, s);
        }
        public void Subscribe(IObserver observer)
        {
            _predmeti.Subscribe(observer);
        }

        public void Save(List<Predmet> predmet)
        {
            _predmeti.Save(predmet);
        }
    }
}
