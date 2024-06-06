using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Model;
using projekatWPF.Model.DAO;
using projekatWPF.Observer;
using projekatWPF.Storage;

namespace projekatWPF.Controller
{
    public class OcenaController
    {
        private readonly OcenaDAO _ocene;

        public OcenaController()
        {
            _ocene = new OcenaDAO();
        }

        public List<Ocena> GetAllLOcene()
        {
            return _ocene.GetAll();
        }

        public void Create(int vrednost, DateOnly datumPolaganja)
        {
            _ocene.Add(vrednost, datumPolaganja);
        }

        public void Delete(Ocena ocene)
        {
            _ocene.Remove(ocene);
        }
        public void Dodaj_Ocenu_Studentu(string s, string p, DateTime dt, int vrednost)
        {
            _ocene.Dodaj_Ocenu_Studentu(s,p,dt,vrednost);
        }
        public void Subscribe(IObserver observer)
        {
            _ocene.Subscribe(observer);
        }

        public void Save(List<Ocena> ocene)
        {
            _ocene.Save(ocene);
        }
    }
}
