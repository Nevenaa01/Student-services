using System;
//using ConsoleApp.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using projekatWPF.Serializer;

namespace projekatWPF.Model
{
    public class Adresa : Serializable
    {
        string ulica;
        string broj;
        string grad;
        string drzava;

        public Adresa()
        {
            ulica = "";
            broj = "";
            grad = "";
            drzava = "";
        }

        public Adresa(string ulica, string broj, string grad, string drzava)
        {
            this.ulica = ulica;
            this.broj = broj;
            this.grad = grad;
            this.drzava = drzava;
        }
        public string Ulica
        {
            get { return ulica; }
            set { ulica = value; }
        }
        public string Broj
        {
            get { return broj; }
            set { broj = value; }
        }
        public string Grad
        {
            get { return grad; }
            set { grad = value; }
        }
        public string Drzava
        {
            get { return drzava; }
            set { drzava = value; }
        }
        public void FromCSV(string[] values)
        {
                ulica = values[0];
                broj = values[1];
                grad = values[2];
                drzava = values[3];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                ulica,
                broj,
                grad,
                drzava
            };
            return csvValues;
        }
        public string ToString()
        {
            return ulica + "," + broj + "," + grad + "," + drzava;
        }
    }
}
