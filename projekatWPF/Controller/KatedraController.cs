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
    public class KatedraController
    {
        private readonly KatedraDAO _katedre;

        public KatedraController()
        {
            _katedre = new KatedraDAO();
        }

        public List<Katedra> GetAllKatedre()
        {
            return _katedre.GetAll();
        }

        public void Create(int sifra, string naziv)
        {
            _katedre.Add(sifra, naziv);
        }

        public void Delete(Katedra katedra)
        {
            _katedre.Remove(katedra);
        }

        public bool Izmeni(int sifra, string naziv)
        {
            return _katedre.Izemni(sifra, naziv);
        }

        public void Subscribe(IObserver observer)
        {
            _katedre.Subscribe(observer);
        }

        public void Save(List<Katedra> katedre)
        {
            _katedre.Save(katedre);
        }
    }
}
