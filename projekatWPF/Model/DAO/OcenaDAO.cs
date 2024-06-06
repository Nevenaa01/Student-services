using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Observer;
using projekatWPF.Storage;
using projekatWPF.Model;
using projekatWPF.Controller;

namespace projekatWPF.Model.DAO
{
    class OcenaDAO : ISubject
    {
        private readonly List<IObserver> _observers;

        private readonly OcenaStorage _storage;
        private readonly List<Ocena> _ocene;

        public OcenaDAO()
        {
            _storage = new OcenaStorage();
            _ocene = _storage.Load();
            _observers = new List<IObserver>();
        }

        public void Add(int vrednost, DateOnly datumPolaganja)
        {
            Profesor prof = new Profesor("neko", "neko", new DateOnly(1977, 01, 01), new Adresa("neka", "neka", "neka", "neka"), "0679431275", "email@email.com", new Adresa("nesto", "nesto", "nesto", "nesto"), "123", "neko", 1);


            Predmet predmet = new Predmet("1", "nesto", Semestar.LETNJI, 123, prof, 8);
            DateOnly datumRodjenja = new DateOnly(2000, 01, 01);
            Adresa adresaStanovanja = new Adresa("ulica", "brUlice", "grad", "drzava");

            Student student = new Student("randomic", "random", datumRodjenja, adresaStanovanja, "0679431279", "email", "RA01/2020", 1, "I (prva)", Status.BUDŽET, 10.0);

            Ocena o = new Ocena(predmet, vrednost, datumPolaganja);
            o.Student = student;

            _ocene.Add(o);
            _storage.Save(_ocene);
            NotifyObservers();
        }

        public void Dodaj_Ocenu_Studentu(string s, string p, DateTime dt, int vrednost)
        {
            StudentController sc = new StudentController();
            PredmetController pc=new PredmetController();
            Student student = sc.GetAllStudente().Find(st=> st.BrIndeksa==s);
            Predmet predmet = pc.Nadji_predmet(p);
            Ocena ocena=new Ocena();
            ocena.Predmet= predmet;
            ocena.Student = student;
            ocena.DatumPom = dt;
            ocena.Vrednost = vrednost;
            _ocene.Add(ocena);
            _storage.Save(_ocene);
            NotifyObservers();
        }
        public void Remove(Ocena ocena)
        {
            _ocene.Remove(ocena);
            _storage.Save(_ocene);
            NotifyObservers();
        }

        public List<Ocena> GetAll()
        {
            return _ocene;
        }

        public void Save(List<Ocena> ocene)
        {
            _storage.Save(ocene);
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
