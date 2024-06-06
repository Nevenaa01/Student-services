//using ConsoleApp.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using projekatWPF.Controller;
using projekatWPF.Serializer;

public enum Status { BUDŽET, SAMOFINANSIRANJE}

namespace projekatWPF.Model
{
    public class Student : Serializable, INotifyPropertyChanged, IDataErrorInfo
    {
        string prezime;
        string ime;
        DateTime datumPom;
        DateOnly datumRodjenja;

        Adresa adresaStanovanja;
        string ulica;
        string brojUlice;
        string grad;
        string drzava;

        string telefon;
        string email;
        string brIndeksa;
        int godinaUpisa;
        string trenutnaGodinaStudija;
        Status status;
        double prosecnaOcena;
        Dictionary<Predmet, Ocena> polozeniIspiti;
        List<Predmet> nepolozeniIspiti;

        public Student()
        {
            prezime = ime = email = brIndeksa = "";
            telefon = "";
            godinaUpisa = 0;
            trenutnaGodinaStudija = "";
            status = global::Status.BUDŽET;
            prosecnaOcena = 0;
            datumPom = DateTime.Now;
            datumRodjenja = new DateOnly(DatumPom.Year, DatumPom.Month, DatumPom.Day);
            adresaStanovanja = new Adresa();
            brojUlice = brojUlice = grad = drzava = "";
            nepolozeniIspiti = new List<Predmet>();
            polozeniIspiti = new Dictionary<Predmet, Ocena>();
        }

        public Student(string prezime, string ime, DateOnly datumRodjenja, Adresa adresaStanovanja, string telefon, string email, string brIndeksa, int godinaUpisa, string trenutnaGodinaStudija, Status status, double prosecnaOcena)
        {
            this.prezime = prezime;
            this.ime = ime;
            this.datumRodjenja = datumRodjenja;
            this.adresaStanovanja = adresaStanovanja;
            this.telefon = telefon;
            this.email = email;
            this.brIndeksa = brIndeksa;
            this.godinaUpisa = godinaUpisa;
            this.trenutnaGodinaStudija = trenutnaGodinaStudija;
            this.status = status;
            this.prosecnaOcena = prosecnaOcena;

            nepolozeniIspiti = new List<Predmet>();
            polozeniIspiti = new Dictionary<Predmet, Ocena>();
        }

        public string Ime
        {
            get => ime;
            set
            {
                if (value != ime)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                    /*MessageBox.Show(tIme.Text);
                    MessageBox.Show(value);
                    MessageBox.Show(_ime);*/
                }
            }
        }

        public string Prezime
        {
            get => prezime;
            set
            {
                if (value != prezime)
                {
                    prezime = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime DatumPom
        {
            get => datumPom;
            set
            {
                if (value != datumPom)
                {
                    datumPom = value;
                    OnPropertyChanged();
                    DatumRodjenja = new DateOnly(DatumPom.Year, DatumPom.Month, DatumPom.Day);
                }
            }
        }
        public DateOnly DatumRodjenja
        {
            get => datumRodjenja;
            set
            {
                if (value != datumRodjenja)
                {
                    datumRodjenja = value;
                    //MessageBox.Show(datumRodjenja.ToString());
                    OnPropertyChanged();
                    
                }
            }
        }

        public string Ulica
        {
            get => ulica;
            set
            {
                if(value != ulica)
                {
                    ulica = value;
                    OnPropertyChanged();

                    if(!string.IsNullOrEmpty(ulica) && !string.IsNullOrEmpty(brojUlice) && !string.IsNullOrEmpty(grad) && !string.IsNullOrEmpty(drzava))
                    {
                        adresaStanovanja = new Adresa(ulica, brojUlice, grad, drzava);
                    }
                }
            }
        }
        public string BrojUlice
        {
            get => brojUlice;
            set
            {
                if (value != brojUlice)
                {
                    brojUlice = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulica) && !string.IsNullOrEmpty(brojUlice) && !string.IsNullOrEmpty(grad) && !string.IsNullOrEmpty(drzava))
                    {
                        adresaStanovanja = new Adresa(ulica, brojUlice, grad, drzava);
                    }
                }
            }
        }
        public string Grad
        {
            get => grad;
            set
            {
                if (value != grad)
                {
                    grad = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulica) && !string.IsNullOrEmpty(brojUlice) && !string.IsNullOrEmpty(grad) && !string.IsNullOrEmpty(drzava))
                    {
                        adresaStanovanja = new Adresa(ulica, brojUlice, grad, drzava);
                    }
                }
            }
        }
        public string Drzava
        {
            get => drzava;
            set
            {
                if (value != drzava)
                {
                    drzava = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulica) && !string.IsNullOrEmpty(brojUlice) && !string.IsNullOrEmpty(grad) && !string.IsNullOrEmpty(drzava))
                    {
                        adresaStanovanja = new Adresa(ulica, brojUlice, grad, drzava);
                    }
                }
            }
        }

        public string AdresaStanovanja
        {
            get
            {
                /*if (adresaStanovanja.ToString() == ",,,")
                    return "";
                else*/
                    return adresaStanovanja.ToString();
            }
            set
            {
                if (value != adresaStanovanja.ToString())
                {
                    string[] adresa = value.Split(',');
                    if (adresa.Length == 4) //Ovako radi ali uvek pise zareze POGLEDAJ OVOOOOOOOO!!
                    {
                        adresaStanovanja.Ulica = adresa[0];
                        adresaStanovanja.Broj = adresa[1];
                        adresaStanovanja.Grad = adresa[2];
                        adresaStanovanja.Drzava = adresa[3];

                        OnPropertyChanged();
                    }
                }
            }
        }

        public string Telefon
        {
            get => telefon;
            set
            {
                if (value != telefon)
                {
                    telefon = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (value != email)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        public string BrIndeksa
        {
            get => brIndeksa;
            set
            {
                if (value != brIndeksa)
                {
                    brIndeksa = value;
                    OnPropertyChanged();
                }
            }
        }

        public int GodinaUpisa
        {
            get => godinaUpisa;
            set
            {
                    if (value != godinaUpisa)
                    {
                        //MessageBox.Show(value);
                        godinaUpisa = value;
                        //MessageBox.Show(godinaUpisa.ToString());
                        OnPropertyChanged();
                    }
            }
        }

        public string TrenutnaGodinaStudija
        {
            get => trenutnaGodinaStudija;
            set
            {
                if (value != trenutnaGodinaStudija)
                {
                    trenutnaGodinaStudija = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Status
        {
            get => (status == global::Status.BUDŽET)? "BUDŽET" : "SAMOFINANSIRANJE";
            set
            {
                if (value != ((status == global::Status.BUDŽET) ? "BUDŽET" : "SAMOFINANSIRANJE"))
                {
                    status = (value == "BUDŽET") ? global::Status.BUDŽET : global::Status.SAMOFINANSIRANJE;
                    OnPropertyChanged();
                }
            }
        }

        public override string ToString()
        {
            return $"{Ime} {Prezime} {DatumRodjenja} {Ulica} {BrojUlice} {Grad} {Drzava} {Telefon} {Email} {BrIndeksa} {GodinaUpisa} {TrenutnaGodinaStudija} {Status}";
        }

        public string Error => null;

        private Regex _imePrezimeRegex = new Regex("^[A-ZČĆŠĐŽa-zčćšđž -]+$");
        private Regex _datumRodjenjaRegex = new Regex("^([1-3][0-9])|(0[1-9])/([1-3][0-9])|(0[1-9])/[12][0-9]{3}$");
        private Regex _ulicaGradDrzavaRegex = new Regex("^[A-Za-zČĆŠĐŽčćšđž ]+$");
        private Regex _brojUliceRegex = new Regex("^[1-9][0-9]*[A-Za-zČĆŠĐŽčćšđž]*$");
        private Regex _telefonRegex = new Regex("^([+]38[17]6(([0-9]{8})|([0-9]{7})))$|^(06(([0-9]{8})|([0-9]{7})))$");
        private Regex _emailRegex = new Regex("^[A-Za-z0-9ČĆŠĐŽčćšđž.]*@[a-zčćšđž]*[.][a-zčćšđž.]*$");
        private Regex _brIndeksaRegex = new Regex("^RA[0-9]{2,3}/[0-9]{4}$");
        private Regex _godinaUpisaRegex = new Regex("^[0-9]{4}$");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Ime")
                {
                    if (string.IsNullOrEmpty(Ime))
                        return "*ime";

                    Match match = _imePrezimeRegex.Match(Ime);
                    if (!match.Success)
                        return "primer: Ime";
                }
                else if (columnName == "Prezime")
                {
                    if (string.IsNullOrEmpty(Prezime))
                        return "*prezime";

                    Match match = _imePrezimeRegex.Match(Prezime);
                    if (!match.Success)
                        return "primer: Prezime";
                }
                else if (columnName == "DatumPom")
                {
                    if (DatumPom == DateTime.Now)
                        return "*datum rodjenja";

                    Match match = _datumRodjenjaRegex.Match(new DateOnly(DatumPom.Year, DatumPom.Month, DatumPom.Day).ToString());
                    //if (!match.Success)
                      //  return "format: dd/mm/gggg";

                    if (DatumPom > DateTime.Now)
                        return "mora biti datum iz proslosti";
                }
                else if (columnName == "Ulica")
                {
                    if (string.IsNullOrEmpty(Ulica) )
                        return "*ulica";

                    Match match = _ulicaGradDrzavaRegex.Match(Ulica);
                    if (!match.Success)
                        return "format: Ulica";
                }
                else if (columnName == "BrojUlice")
                {
                    if (string.IsNullOrEmpty(BrojUlice))
                        return "*broj ulice";

                    Match match = _brojUliceRegex.Match(BrojUlice);
                    if (!match.Success)
                        return "format: 7/7a";
                }
                else if (columnName == "Grad")
                {
                    if (string.IsNullOrEmpty(Grad))
                        return "*grad";

                    Match match = _ulicaGradDrzavaRegex.Match(Grad);
                    if (!match.Success)
                        return "format: Grad";
                }
                else if (columnName == "Drzava")
                {
                    if (string.IsNullOrEmpty(Drzava))
                        return "*drzava";

                    Match match = _ulicaGradDrzavaRegex.Match(Drzava);
                    if (!match.Success)
                        return "format: Drzava";
                }
                else if (columnName == "Telefon")
                {
                    if (string.IsNullOrEmpty(Telefon))
                        return "*telefon";

                    Match match = _telefonRegex.Match(Telefon);
                    if (!match.Success)
                        return "format: +381 ili 06...";
                }
                else if (columnName == "Email")
                {
                    if (string.IsNullOrEmpty(Email))
                        return "*email";

                    Match match = _emailRegex.Match(Email);
                    if (!match.Success)
                        return "primer: email@gmail.com";
                }
                else if (columnName == "BrIndeksa")
                {
                    if (string.IsNullOrEmpty(BrIndeksa))
                        return "*broj indeksa";

                    Match match = _brIndeksaRegex.Match(BrIndeksa);
                    if (!match.Success)
                        return "format: RAXX/YYYY";

                    string[] deloviIndeksa = BrIndeksa.Split('/');
                    if (int.Parse(deloviIndeksa[1]) > DateTime.Now.Year)
                        return "godina u indeksu ne moze biti posle trenutne";
                }
                else if (columnName == "GodinaUpisa")
                {
                    if (string.IsNullOrEmpty(GodinaUpisa.ToString()))
                        return "*godina upisa";

                    Match match = _godinaUpisaRegex.Match(GodinaUpisa.ToString());
                    if (!match.Success)
                        return "format: 4 broja za godinu";

                    if (godinaUpisa > DateTime.Now.Year)
                        return "godina ne moze biti u buducnosti";
                }
                return null;
            }
        }

        private readonly string[] _validatedProperties = { "Ime", "Prezime", "DatumPom", "Ulica", "BrojUlice", "Grad", "Drzava", "Telefon", "Email", "BrIndeksa", "GodinaUpisa", "TrenutnaGodinaStudija", "Status" };

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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double ProsecnaOcena
        {
            get { return prosecnaOcena; }
            set { prosecnaOcena = value; }
        }

        public Dictionary<Predmet, Ocena> PolozeniIspiti
        {
            get { return polozeniIspiti; }
            set { polozeniIspiti = value; }
        }

        public List<Predmet> NepolozeniIspiti
        {
            get { return nepolozeniIspiti; }
            set { nepolozeniIspiti = value; }
        }

        static public bool ProveraIndeksa(string brin)
        {
            if(brin.Length!=9)
                return false;
            if (Char.IsNumber(brin[0])|| Char.IsNumber(brin[1]))
                return false;
            if (!Char.IsNumber(brin[2]) || !Char.IsNumber(brin[3]))
                return false;
            if (brin[4] != '/')
                return false;
            if (!Char.IsNumber(brin[5]) || !Char.IsNumber(brin[6]) || !Char.IsNumber(brin[7]) || !Char.IsNumber(brin[8]))
                return false;
            return true;

        }
        public string SifraNepolozenihPredmeta()
        {
            string s = "";

            foreach (Predmet p in nepolozeniIspiti)
            {
                s += p.Sifra.ToString() + ",";
            }
            if (s.Length > 0)
                s = s.Remove(s.Length - 1);
            return s;
        }

        public string SifraPolozenihIspita()
        {
            string s = "";

            foreach (KeyValuePair<Predmet, Ocena> pi in polozeniIspiti)
            {
                s += pi.Key.Sifra.ToString() + ",";
            }
            if(s.Length > 0)
                s = s.Remove(s.Length - 1);
            return s;
        }

        public void dodavanjeNepolozenihIspita(Predmet p)
        {
            nepolozeniIspiti.Add(p);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                prezime,
                ime,
                datumRodjenja.ToString("dd/MM/yyyy"),
                //adresaStanovanja.Ulica + "," + adresaStanovanja.Broj + "," + adresaStanovanja.Grad + "," + adresaStanovanja.Drzava,
                AdresaStanovanja,
                telefon,
                email,
                brIndeksa,
                godinaUpisa.ToString(),
                trenutnaGodinaStudija,
                (status == global :: Status.BUDŽET)? "B" : "S",
                Prosek().ToString(),
                SifraNepolozenihPredmeta(),
                SifraPolozenihIspita()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            prezime = values[0];
            ime = values[1];
            datumRodjenja = DateOnly.ParseExact(values[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            AdresaStanovanja = values[3];
            string[] adresa = values[3].Split(',');
            adresaStanovanja.Ulica = adresa[0];
            adresaStanovanja.Broj = adresa[1];
            adresaStanovanja.Grad = adresa[2];
            adresaStanovanja.Drzava = adresa[3];
            telefon = values[4];
            email = values[5];
            brIndeksa = values[6];
            godinaUpisa = int.Parse(values[7]);
            trenutnaGodinaStudija = values[8];
            status = (values[9] == "B") ? global::Status.BUDŽET : global::Status.SAMOFINANSIRANJE;
            prosecnaOcena = Double.Parse(values[10]);
            if (values[11].Length > 0)
            {
                string[] nepolozeni = values[11].Split(',');
                for (int i = 0; i < nepolozeni.Length; i++)
                {
                    Predmet p = new Predmet();
                    p.Sifra = nepolozeni[i];
                    nepolozeniIspiti.Add(p);
                }
            }
            if(values[12].Length > 0)
            {
                string[] polozeni = values[12].Split(',');
                for (int i = 0; i < polozeni.Length; i++)
                {
                    Predmet p = new Predmet();
                    p.Sifra = polozeni[i];
                    Ocena o=new Ocena();
                    OcenaController oc= new OcenaController();
                    List<Ocena> ocene = oc.GetAllLOcene();
                    if((o = ocene.Find(oca => oca.Student.BrIndeksa == BrIndeksa && oca.Predmet.Sifra == p.Sifra))!=null)
                        polozeniIspiti.Add(p, o);
                }
            }
            prosecnaOcena = Prosek();/////////////////////////////////PROVERI
        }

        public double Prosek()
        {
            double suma = 0;
            int i = 0;
            foreach (KeyValuePair<Predmet, Ocena> po in polozeniIspiti)
            {
                suma += po.Value.Vrednost;
                i++;
            }
            if (i > 0)
                return Math.Round(suma / i, 2);
            else
                return 0;
        }

        public Predmet Nadji_kljuc(string p)
        {
            Predmet predmet = new Predmet();
            foreach (KeyValuePair<Predmet, Ocena> po in polozeniIspiti)
            {
                if (po.Key.Sifra.ToString() == p)
                {
                    predmet = po.Key;
                    break;
                }
            }
            return predmet;
        }
        public int Koja_godina()
        {
            switch (TrenutnaGodinaStudija)
            {
                case "I (prva)": return 1;break;
                case "II (druga)": return 2; break;
                case "III (treća)": return 3; break;
                case "IV (četvrta)": return 4; break;
                case "V(peta)":return 5; break;
                default: return 1; break;
            }
        }
        /*override public string ToString()
        {
            string pp = "";
            string np = "";
            if (polozeniIspiti.Count > 0)
            {
                foreach (KeyValuePair<Predmet, Ocena> p in polozeniIspiti)
                {
                    pp += p.Key.Naziv_predmeta + " sa ocenom: " + p.Value.Vrednost + ",";
                }
                pp = pp.Remove(pp.Length - 1);
            }
            if(nepolozeniIspiti.Count > 0)
            {
                foreach (Predmet p in nepolozeniIspiti)
                {
                    np += p.Naziv_predmeta + ",";
                }
                np = np.Remove(np.Length - 1);
            }
            string s = "";
            if (status == Status.B)
                s = "B";
            else
                s = "S";
            prosecnaOcena = Prosek();
            return "-------------------------------------------------------------------\n" + "Student: " + ime + " " + prezime + "\nBroj indeksa: " + brIndeksa + "\nDatum rodjenja: " + datumRodjenja.ToString("dd/MM/yyyy") + "\nAdresa stanovanja: " + adresaStanovanja.ToString() + "\nBroj telefona: " + telefon + "\nEmail: " + email + "\nGodina upisa: " + godinaUpisa.ToString() + "\nTrenutna godina studija: " + trenutnaGodinaStudija.ToString() + "\nStatus: " + s + "\nProsecna ocena: " + prosecnaOcena.ToString() + "\nPolozeni predmeti: " + pp + "\nNepolozeni predmeti: " + np;
        }*/
    }
}
