//using ConsoleApp.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Serializer;

namespace projekatWPF.Model
{
    public class Katedra : Serializable
    {
        int sifra;
        string naziv;
        Profesor sef;
        List<Profesor> spisakProfesora;

        public Katedra()
        {
            sifra = 0; 
            naziv = "";
            sef = new Profesor();
            spisakProfesora = new List<Profesor>();
        }

        public Katedra(int sifra, string naziv, Profesor sef)
        {
            this.sifra = sifra;
            this.naziv = naziv;
            this.sef = sef;
            spisakProfesora = new List<Profesor>();
        }

        public int Sifra
        {
            get { return sifra; }
            set { sifra = value; }
        }

        public string Naziv
        {
            get { return naziv; }
            set { naziv = value; }
        }

        public Profesor Sef
        {
            get { return sef; }
            set { sef = value; }
        }
        public string profesorBrLk()
        {
            string s = "";
            foreach (Profesor p in spisakProfesora)
                s += p.BrojLicneKarte + ",";
            if(s.Length>0)
                s = s.Remove(s.Length - 1);
            return s;
        }

        public List<Profesor> SpisakProfesora
        {
            get { return spisakProfesora; }
            set { spisakProfesora = value; }
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                sifra.ToString(),
                naziv,
                sef.BrojLicneKarte,
                profesorBrLk()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            sifra = int.Parse(values[0]);
            naziv = values[1];
            sef.BrojLicneKarte = values[2];
            if (values[3].Length > 0)
            {
                string[] profesori = values[3].Split(',');
                for (int i = 0; i < profesori.Length; i++)
                {
                    Profesor p = new Profesor();
                    p.BrojLicneKarte = profesori[i];
                    spisakProfesora.Add(p);
                }
            }
        }
        override public string ToString()
        {
            string prof = "";
            if (spisakProfesora.Count > 0)
            {
                foreach (Profesor p in spisakProfesora)
                    prof += p.Ime + " " + p.Prezime + ",";
                prof = prof.Remove(prof.Length - 1);
            }
            return "-------------------------------------------------------------------\n" + "Katedra: " + naziv + "\nSifra katedre: " + sifra + "\nSef katedre: " + sef.Ime + " " + sef.Prezime + "\nSpisak profesora: " + prof;
        }
    }
}
