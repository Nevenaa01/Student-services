using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Observer;
using projekatWPF.Storage;
using projekatWPF.Model;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.Windows;
using projekatWPF.Controller;

namespace projekatWPF.Model.DAO
{
    class ProfesorDAO : ISubject
    {
        private readonly List<IObserver> _observers;

        private readonly ProfesorStorage _storage;
        private readonly List<Profesor> _profesori;

        public ProfesorDAO()
        {
            _storage = new ProfesorStorage();
            _profesori = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(Profesor profesor)
        {
            _profesori.Add(profesor);
            _storage.Save(_profesori);
            NotifyObservers();
        }

        public void Remove(Profesor profesor)
        {
            _profesori.Remove(profesor);
            _storage.Save(_profesori);
            NotifyObservers();
        }
        public bool Izmeni(string prezime, string ime, DateOnly datumRodjenja, string telefon, string email, string brlk, int godinestaza, string adresaKancelarije,string zvanje , string adresaStanovanja)
        {
            Profesor izmena = _profesori.Find(s => s.BrojLicneKarte == brlk);
            if (izmena == null)
                MessageBox.Show("Ne mozes menjati broj licne karte.");
            else
            {
                string[] adrs = adresaStanovanja.Split(",");
                string[] adrk = adresaKancelarije.Split(",");
                Adresa adresak = new Adresa(adrk[0], adrk[1], adrk[2], adrk[3]);
                Adresa adresas = new Adresa(adrs[0], adrs[1], adrs[2], adrs[3]);
                izmena.Ime = ime;
                izmena.Prezime = prezime;
                izmena.DatumRodjenja = datumRodjenja;
                izmena.Zvanje = zvanje;
                izmena.Email = email;
                izmena.Telefon = telefon;
                izmena.BrojLicneKarte = brlk;
                izmena.GodineStaza = godinestaza;
                izmena.AdresaKancelarije = adresak.ToString();
                izmena.AdresaStanovanja = adresas.ToString();
                _storage.Save(_profesori);
                NotifyObservers();
                return true;
            }
            return false;
        }

        public void Postavljanje_predmeta(string prof, string predmet, List<Profesor> Profesori, List<Predmet> Predmeti)
        {
            PredmetController pc = new PredmetController();
            Profesor izmena = Profesori.Find(pro => pro.BrojLicneKarte == prof);
           
            Predmet Predmet = Predmeti.Find(pred => pred.Sifra.ToString() == predmet);
            
            izmena.Spisak_predmeta.Add(Predmet);
            NotifyObservers();
        }
       /* public void Vrati_predmet(Profesor profesor, Predmet predmet) 
        {
            Profesor izmena = _profesori.Find(pro => pro.BrojLicneKarte == profesor.BrojLicneKarte);
            if (izmena == null)
            {
                izmena = new Profesor();
                izmena.Ime = String.Empty;
                izmena.Prezime = String.Empty;
            }
            izmena = profesor;
            _storage.Save(_profesori);
            NotifyObservers();
        }*/
        public void Save(List<Profesor> profesori)
        {
            _storage.Save(profesori);
        }
       /* public void Postavljanje_predmeta_prividno(string prof, string predmet)
        {
            PredmetController pc = new PredmetController();
            Profesor izmena = _profesori.Find(pro => pro.BrojLicneKarte == prof);

            Predmet Predmet = pc.GetAllPredmeti().Find(pred => pred.Sifra.ToString() == predmet);

            izmena.Spisak_predmeta.Add(Predmet);
       
        }*/
        public void Uklanjanje_predmeta(string prof, string predmet, List<Profesor> Profesori)
        {
            Profesor izmena = Profesori.Find(pro => pro.BrojLicneKarte == prof);
            izmena.Spisak_predmeta.RemoveAll(pred => pred.Sifra.ToString() == predmet);
            NotifyObservers();

        }
/*
        public void Uklanjanje_predmeta_Prividno(string prof, string predmet)
        {
            Profesor izmena = _profesori.Find(pro => pro.BrojLicneKarte == prof);
            izmena.Spisak_predmeta.RemoveAll(pred => pred.Sifra.ToString() == predmet);
        }*/
        public List<Profesor> GetAll()
        {
            return _profesori;
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
