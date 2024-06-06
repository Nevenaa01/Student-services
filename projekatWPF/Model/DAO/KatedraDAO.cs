using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Storage;
using projekatWPF.Model;
using projekatWPF.Observer;
using System.Windows;

namespace projekatWPF.Model.DAO
{
    class KatedraDAO : ISubject
    {
        private readonly List<IObserver> _observers;

        private readonly KatedraStorage _storage;
        private readonly List<Katedra> _katedre;

        public KatedraDAO()
        {
            _storage = new KatedraStorage();
            _katedre = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Add(int sifra, string naziv)
        {
            Profesor p = new Profesor("neko", "neko", new DateOnly(1977, 01, 01), new Adresa("neka", "neka", "neka", "neka"), "0679431275", "email@email.com", new Adresa("nesto", "nesto", "nesto", "nesto"), "465783461", "neko", 1);

            Katedra katedra = new Katedra(sifra, naziv, p);
            _katedre.Add(katedra);
            _storage.Save(_katedre);
            NotifyObservers();
        }

        public void Remove(Katedra katedra)
        {
            _katedre.Remove(katedra);
            _storage.Save(_katedre);
            NotifyObservers();
        }

        public bool Izemni(int sifra, string naziv)
        {
            Katedra k = _katedre.Find(k => k.Sifra == sifra);
            if(k == null)
            {
                MessageBox.Show("Ne moze se menjati sifra katedre!");
            }
            else
            {
                k.Sifra = sifra;
                k.Naziv = naziv;
                _storage.Save(_katedre);
                NotifyObservers();
                return true;
            }

            return false;
        }

        public List<Katedra> GetAll()
        {
            return _katedre;
        }

        public void Save(List<Katedra> katedre)
        {
            _storage.Save(katedre);
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
