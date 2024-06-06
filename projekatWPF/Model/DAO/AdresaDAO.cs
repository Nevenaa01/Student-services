using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Storage;
using projekatWPF.Model;
using projekatWPF.Observer;

namespace projekatWPF.Model.DAO
{
    class AdresaDAO : ISubject
    {
        private readonly List<IObserver> _observers;

        private readonly AdresaStorage _storage;
        private readonly List<Adresa> _adrese;

        public AdresaDAO()
        {
            _storage = new AdresaStorage();
            _adrese = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(string ulica, string broj, string grad, string drzava)
        {
            Adresa adresa = new Adresa(ulica, broj, grad, drzava);
            _adrese.Add(adresa);
            _storage.Save(_adrese);
            NotifyObservers();
        }

        public void Remove(Adresa adresa)
        {
            _adrese.Remove(adresa);
            _storage.Save(_adrese);
            NotifyObservers();
        }

        public void Subscribe(IObserver observer)
        {
            _observers.Add(observer);
        }

        public List<Adresa> GetAll()
        {
            return _adrese;
        }

        public void Save(List<Adresa> adrese)
        {
            _storage.Save(adrese);
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
