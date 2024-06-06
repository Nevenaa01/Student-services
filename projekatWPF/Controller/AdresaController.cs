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
    public class AdresaController
    {
        private readonly AdresaDAO _adrese;

        public AdresaController()
        {
            _adrese = new AdresaDAO();
        }

        public List<Adresa> GetAllAdrese()
        {
            return _adrese.GetAll();
        }

        public void Create(string ulica, string broj, string grad, string drzava)
        {
            _adrese.Add(ulica, broj, grad, drzava);
        }

        public void Delete(Adresa adresa)
        {
            _adrese.Remove(adresa);
        }

        public void Subscribe(IObserver observer)
        {
            _adrese.Subscribe(observer);
        }

        public void Save(List<Adresa> adrese)
        {
            _adrese.Save(adrese);
        }
    }
}
