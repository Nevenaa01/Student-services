using System;
//using ConsoleApp.Serialization;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using projekatWPF.Serializer;
using projekatWPF.View;

namespace projekatWPF.Model
{   
    public enum Semestar {LETNJI, ZIMSKI};
    public class Predmet : Serializable, INotifyPropertyChanged, IDataErrorInfo
    {
        string sifra_predmeta;
        string naziv_predmeta;
        Semestar semestar;
        int godina_izvodjenja_predmeta;
        Profesor profesor;
        int ESPB_bodovi;
        List<Student> polozili_predmet;
        List<Student> nisu_polozili_predmet;

        public Predmet()
        {
            sifra_predmeta = "";
            naziv_predmeta = "";
            semestar = Semestar.LETNJI;
            godina_izvodjenja_predmeta = 0;
            profesor = new Profesor();
            ESPB_bodovi = 0;
            polozili_predmet = new List<Student>();
            nisu_polozili_predmet = new List<Student>();
        }

        public Predmet(string sifra_predmeta, string naziv_predmeta, Semestar semestar, int godina_izvodjenja_predmeta, Profesor profesor, int ESPB_bodovi)//a za profesora?
        {
            this.sifra_predmeta = sifra_predmeta;
            this.naziv_predmeta = naziv_predmeta;
            this.semestar = semestar;
            this.godina_izvodjenja_predmeta = godina_izvodjenja_predmeta;
            this.profesor = profesor;
            this.ESPB_bodovi = ESPB_bodovi;
            polozili_predmet = new List<Student>();
            nisu_polozili_predmet = new List<Student>();
        }
        public string Sifra
        {
            get => sifra_predmeta;
            set
            {
                if(value != sifra_predmeta)
                {
                    sifra_predmeta = value;
                    OnPropertyChanged();
                }
                
            }
        }

        public string Naziv
        {
            get => naziv_predmeta;
            set
            {
                if (value != naziv_predmeta)
                {
                    naziv_predmeta = value;
                    OnPropertyChanged();
                }

            }
        }

        public Semestar Semestar
        {
            get => semestar;
            set
            {
                if (value != semestar)
                {
                    semestar = value;
                    OnPropertyChanged();
                }

            }
        }

        public int GodinaIzvodjenja
        {
            get => godina_izvodjenja_predmeta;
            set
            {
                if (value != godina_izvodjenja_predmeta)
                {
                    godina_izvodjenja_predmeta = value;
                    OnPropertyChanged();
                }

            }
        }

        public Profesor Profesor 
        { 
            get => profesor; 
            set => profesor = value; 
        }//kom

        public int ESPB
        {
            get => ESPB_bodovi;
            set
            {
                if (value != ESPB_bodovi)
                {
                    ESPB_bodovi = value;
                    OnPropertyChanged();
                }

            }
        }
        
        public string SifraNaziv
        {
            get => Sifra + " - " + Naziv;
            set
            {
                if (value != SifraNaziv)
                {
                    SifraNaziv = value;
                    OnPropertyChanged();
                }

            }
        }
        public List<Student> Polozili_predmet
        {
            get { return polozili_predmet; }    
            set { polozili_predmet = value; }
        }

        public List<Student> Nisu_polozili_predmet
        {
            get { return nisu_polozili_predmet; }
            set
            {
                nisu_polozili_predmet = value;
            }
        }

        private Regex _sifraRegex = new Regex("^[0-9A-Za-z]+$");
        private Regex _nazivRegex = new Regex("^[A-ZČĆŠĐŽa-zčćšđž0-9 ]+$");
        private Regex _espbRegex = new Regex("^[1-9][0-9]?$");

        public string this[string columnName]
        {
            get
            {
                if (columnName == "Sifra")
                {
                    if (string.IsNullOrEmpty(Sifra))
                        return "*sifra";

                    Match match = _sifraRegex.Match(Sifra);
                    if (!match.Success)
                        return "format: brojevi (i velika slova)";
                }
                else if (columnName == "Naziv")
                {
                    if (string.IsNullOrEmpty(Naziv))
                        return "*naziv";

                    Match match = _nazivRegex.Match(Naziv);
                    if (!match.Success)
                        return "format: Naziv";
                }
                else if (columnName == "Semestar")
                {
                    if (string.IsNullOrEmpty(Semestar.ToString()))
                        return "*datum rodjenja";
                }
                else if (columnName == "GodinaIzvodjenja")
                {
                    if (string.IsNullOrEmpty(GodinaIzvodjenja.ToString()))
                        return "*godina izvodjenja";
                }
                else if (columnName == "ESPB")
                {
                    if (string.IsNullOrEmpty(ESPB.ToString()))
                        return "*ESPB";

                    Match match = _espbRegex.Match(ESPB.ToString());
                    if (!match.Success)
                        return "format: 1 broj od 4 - 24";

                    if (ESPB > 24 || ESPB < 4)
                        return "broj mora biti izmedju 4 i 24";
                }
                return null;
            }
        }
        public string Error => null;

        private readonly string[] _validatedProperties = { "Sifra", "Naziv", "Semestar", "GodinaIzvodjenja", "ESPB" };

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }

                return true;
            }
        }
        public void dodajPolozili(Student s)
        {
            polozili_predmet.Add(s);
        }

        public void dodajNisuPolozili(Student s)
        {
            nisu_polozili_predmet.Add(s);
        }

        public void FromCSV(string[] values)
        {
            sifra_predmeta = values[0];
            naziv_predmeta = values[1];
            if (values[2] == "Zimski")
                semestar = Semestar.ZIMSKI;
            else
                semestar = Semestar.LETNJI;
            godina_izvodjenja_predmeta = int.Parse(values[3]);
            profesor.BrojLicneKarte = values[4];
            ESPB_bodovi = int.Parse(values[5]);
            if (values[6].Length > 0)
            { 
                string[] polozili = values[6].Split(',');
                for (int i = 0; i < polozili.Length; i++)
                {
                    Student s = new Student();
                    polozili_predmet.Add(s);
                    s.BrIndeksa = polozili[i];
                }
            }
            if (values[7].Length > 0)
            {
                string[] nisu_polozili = values[7].Split(',');
                for (int i = 0; i < nisu_polozili.Length; i++)
                {
                    Student s = new Student();
                    nisu_polozili_predmet.Add(s);
                    s.BrIndeksa = nisu_polozili[i];
                }
            }
        }

        public string[] ToCSV()
        {
            string s;
            if (semestar == Semestar.ZIMSKI)
                s = "Zimski";
            else
                s = "Letnji";
            string polozeni="";
            if (polozili_predmet.Count > 0)
            {
                foreach (Student polozen in polozili_predmet)
                {
                    polozeni += polozen.BrIndeksa + ",";
                }
                if (polozeni.Length > 0)
                    polozeni = polozeni.Remove(polozeni.Length - 1);
            }
            string nepolozeni = "";
            if (nisu_polozili_predmet.Count > 0)
            {
                foreach (Student nepolozen in nisu_polozili_predmet)
                {
                    nepolozeni += nepolozen.BrIndeksa + ",";
                }
                if (nepolozeni.Length > 1)
                    nepolozeni = nepolozeni.Remove(nepolozeni.Length - 1);
            }
            string[] csvValues =
            {
                    sifra_predmeta,
                    naziv_predmeta,
                    s,
                    godina_izvodjenja_predmeta.ToString(),
                    profesor.BrojLicneKarte,
                    ESPB_bodovi.ToString(),
                    polozeni,
                    nepolozeni
            };
            return csvValues;
        }

        
        /*public string ToString()
        {
            string sp = "";
            string np = "";
            if (polozili_predmet.Count > 0)
            {
                foreach (Student s in polozili_predmet)
                {
                    sp += s.Ime + " " + s.Prezime + ", ";
                }
                sp = sp.Remove(sp.Length - 2);
            }
            if(nisu_polozili_predmet.Count > 0)
            {
                foreach (Student s in nisu_polozili_predmet)
                {
                    np += s.Ime + " " + s.Prezime + ", ";
                }
                np = np.Remove(np.Length - 2);
            }
            string seme = "";
            if (semestar == Semestar.LETNJI)
                seme = "Letnji";
            else
                seme = "Zimski";
            return "-------------------------------------------------------------------\n" + "Predmet: " + " " + naziv_predmeta + "\nSifra predmeta: " + sifra_predmeta.ToString() + "\nSemestar: " + seme + "\nGodina izvodjenja predmeta: " + godina_izvodjenja_predmeta.ToString() + "\nProfesor: " + profesor.Ime + " " + profesor.Prezime + "\nESPB: " + ESPB_bodovi.ToString() + "\nPolozili predmet: " + sp + "\nNisu polozili predmet: " + np;
        }*/
    }
}
