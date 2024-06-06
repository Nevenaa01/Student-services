using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using projekatWPF.Serializer;

namespace projekatWPF.Model
{
    public class Profesor : Serializable, INotifyPropertyChanged, IDataErrorInfo
    {
        string prezime;
        string ime;
        DateTime datumPom;
        DateOnly datum_rodjenja;

        Adresa adresa_stanovanja;
        string ulicaStanovanja;
        string brojUliceStanovanja;
        string gradStanovanja;
        string drzavaStanovanja;

        string telefon;
        string email;
        string imePrezime;
        Adresa adresa_kancelarije;
        string ulicaKancelarije;
        string brojUliceKancelarije;
        string gradKancelarije;
        string drzavaKancelarije;

        string br_lk;
        string zvanje;
        int godine_staza;
        List<Predmet> spisak_predmeta;

        public Profesor()
        {
            prezime = "";
            ime = "";
            datumPom = DateTime.Now;
            datum_rodjenja = new DateOnly(DatumPom.Year, DatumPom.Month, DatumPom.Day);
            adresa_stanovanja = new Adresa();
            ulicaStanovanja = brojUliceStanovanja = gradStanovanja = drzavaStanovanja = "";
            telefon = "";
            email = "";
            adresa_kancelarije = new Adresa();
            ulicaKancelarije = brojUliceKancelarije = gradKancelarije = drzavaKancelarije = "";
            br_lk = "";
            zvanje = "";
            godine_staza = 0;
            spisak_predmeta = new List<Predmet>();
        }
        public Profesor(string prezime, string ime, DateOnly datum_rodjenja, Adresa adresa_stanovanja, string telefon, string email, Adresa adresa_kancelarije, string br_lk, string zvanje, int godine_staza)
        {
            this.prezime = prezime;
            this.ime = ime;
            this.datum_rodjenja = datum_rodjenja;
            this.adresa_stanovanja = adresa_stanovanja;
            this.telefon = telefon;
            this.email = email;
            this.adresa_kancelarije = adresa_kancelarije;
            this.br_lk = br_lk;
            this.zvanje = zvanje;
            this.godine_staza = godine_staza;
            spisak_predmeta = new List<Predmet>();
        }

        public string Ime
        {
            get => ime;
            set
            {
                if (value != ime)
                {
                    ime = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ImePrezime
        {
            get => ime+" "+prezime;
            set
            {
                if (value != ime+" "+prezime)
                {
                    imePrezime = value;
                    OnPropertyChanged();
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
            get => datum_rodjenja;
            set
            {
                if (value != datum_rodjenja)
                {
                    datum_rodjenja = value;
                    //MessageBox.Show(datumRodjenja.ToString());
                    OnPropertyChanged();

                }
            }
        }

        public string UlicaStanovanja
        {
            get => ulicaStanovanja;
            set
            {
                if(value != ulicaStanovanja)
                {
                    ulicaStanovanja = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaStanovanja) && !string.IsNullOrEmpty(brojUliceStanovanja) && !string.IsNullOrEmpty(gradStanovanja) && !string.IsNullOrEmpty(drzavaStanovanja))
                    {
                        adresa_stanovanja = new Adresa(ulicaStanovanja, brojUliceStanovanja, gradStanovanja, drzavaStanovanja);
                    }
                }
            }
        }

        public string BrojUliceStanovanja
        {
            get => brojUliceStanovanja;
            set
            {
                if (value != brojUliceStanovanja)
                {
                    brojUliceStanovanja = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaStanovanja) && !string.IsNullOrEmpty(brojUliceStanovanja) && !string.IsNullOrEmpty(gradStanovanja) && !string.IsNullOrEmpty(drzavaStanovanja))
                    {
                        adresa_stanovanja = new Adresa(ulicaStanovanja, brojUliceStanovanja, gradStanovanja, drzavaStanovanja);
                    }
                }
            }
        }

        public string GradStanovanja
        {
            get => gradStanovanja;
            set
            {
                if (value != gradStanovanja)
                {
                    gradStanovanja = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaStanovanja) && !string.IsNullOrEmpty(brojUliceStanovanja) && !string.IsNullOrEmpty(gradStanovanja) && !string.IsNullOrEmpty(drzavaStanovanja))
                    {
                        adresa_stanovanja = new Adresa(ulicaStanovanja, brojUliceStanovanja, gradStanovanja, drzavaStanovanja);
                    }
                }
            }
        }
        public string DrzavaStanovanja
        {
            get => drzavaStanovanja;
            set
            {
                if (value != drzavaStanovanja)
                {
                    drzavaStanovanja = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaStanovanja) && !string.IsNullOrEmpty(brojUliceStanovanja) && !string.IsNullOrEmpty(gradStanovanja) && !string.IsNullOrEmpty(drzavaStanovanja))
                    {
                        adresa_stanovanja = new Adresa(ulicaStanovanja, brojUliceStanovanja, gradStanovanja, drzavaStanovanja);
                    }
                }
            }
        }

        public string AdresaStanovanja
        {
            get => (adresa_stanovanja.Ulica + "," + adresa_stanovanja.Broj + "," + adresa_stanovanja.Grad + "," + adresa_stanovanja.Drzava);
            set
            {
                if (value != ((adresa_stanovanja.Ulica + "," + adresa_stanovanja.Broj + "," + adresa_stanovanja.Grad + "," + adresa_stanovanja.Drzava)))
                {
                    try
                    {
                        string[] adresa = value.Split(',');
                        adresa_stanovanja.Ulica = adresa[0];
                        adresa_stanovanja.Broj = adresa[1];
                        adresa_stanovanja.Grad = adresa[2];
                        adresa_stanovanja.Drzava = adresa[3];

                    }
                    catch
                    {

                    }

                    OnPropertyChanged();
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

        public string UlicaKancelarije
        {
            get => ulicaKancelarije;
            set
            {
                if (value != ulicaKancelarije)
                {
                    ulicaKancelarije = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaKancelarije) && !string.IsNullOrEmpty(brojUliceKancelarije) && !string.IsNullOrEmpty(gradKancelarije) && !string.IsNullOrEmpty(drzavaKancelarije))
                    {
                        adresa_kancelarije = new Adresa(ulicaKancelarije, brojUliceKancelarije, gradKancelarije, drzavaKancelarije);
                    }
                }
            }
        }

        public string BrojUliceKancelarije
        {
            get => brojUliceKancelarije;
            set
            {
                if (value != brojUliceKancelarije)
                {
                    brojUliceKancelarije = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaKancelarije) && !string.IsNullOrEmpty(brojUliceKancelarije) && !string.IsNullOrEmpty(gradKancelarije) && !string.IsNullOrEmpty(drzavaKancelarije))
                    {
                        adresa_kancelarije = new Adresa(ulicaKancelarije, brojUliceKancelarije, gradKancelarije, drzavaKancelarije);
                    }
                }
            }
        }

        public string GradKancelarije
        {
            get => gradKancelarije;
            set
            {
                if (value != gradKancelarije)
                {
                    gradKancelarije = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaKancelarije) && !string.IsNullOrEmpty(brojUliceKancelarije) && !string.IsNullOrEmpty(gradKancelarije) && !string.IsNullOrEmpty(drzavaKancelarije))
                    {
                        adresa_kancelarije = new Adresa(ulicaKancelarije, brojUliceKancelarije, gradKancelarije, drzavaKancelarije);
                    }
                }
            }
        }
        public string DrzavaKancelarije
        {
            get => drzavaKancelarije;
            set
            {
                if (value != drzavaKancelarije)
                {
                    drzavaKancelarije = value;
                    OnPropertyChanged();

                    if (!string.IsNullOrEmpty(ulicaKancelarije) && !string.IsNullOrEmpty(brojUliceKancelarije) && !string.IsNullOrEmpty(gradKancelarije) && !string.IsNullOrEmpty(drzavaKancelarije))
                    {
                        adresa_kancelarije = new Adresa(ulicaKancelarije, brojUliceKancelarije, gradKancelarije, drzavaKancelarije);
                    }
                }
            }
        }

        public string AdresaKancelarije
        {
            get => (adresa_kancelarije.Ulica + "," + adresa_kancelarije.Broj + "," + adresa_kancelarije.Grad + "," + adresa_kancelarije.Drzava);
            set
            {
                if (value != (adresa_kancelarije.Ulica + "," + adresa_kancelarije.Broj + "," + adresa_kancelarije.Grad + ","  + adresa_kancelarije.Drzava))
                {
                    string[] adresa = value.Split(',');
                    adresa_kancelarije.Ulica = adresa[0];
                    adresa_kancelarije.Broj = adresa[1];
                    adresa_kancelarije.Grad = adresa[2];
                    adresa_kancelarije.Drzava = adresa[3];

                    OnPropertyChanged();
                }
            }
        }

        public string BrojLicneKarte
        {
            get => br_lk;
            set
            {
                if (br_lk != value)
                {
                    br_lk = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Zvanje
        {
            get => zvanje;
            set
            {
                if (value != zvanje)
                {
                    zvanje = value;
                    OnPropertyChanged();
                }
            }
        }

        public int GodineStaza
        {
            get => godine_staza;
            set
            {
                if (value != godine_staza)
                {
                    godine_staza = value;
                    OnPropertyChanged();

                }
            }
        }
        public List<Predmet> Spisak_predmeta
        {
            get { return spisak_predmeta; }
            set { spisak_predmeta = value; }
        }

        private Regex _imePrezimeRegex = new Regex("^[A-ZČĆŠĐŽa-zčćšđž -]+$");
        private Regex _datumRodjenjaRegex = new Regex("([1-3][0-9])|(0[1-9])/([1-3][0-9])|(0[1-9])/[12][0-9]{3}$");
        private Regex _ulicaGradDrzavaRegex = new Regex("^[A-Za-zČĆŠĐŽčćšđž ]+$");
        private Regex _brojUliceRegex = new Regex("^[1-9][0-9]*[A-Za-zČĆŠĐŽčćšđž]*$");
        private Regex _telefonRegex = new Regex("^([+]38[17]6(([0-9]{8})|([0-9]{7})))$|^(06(([0-9]{8})|([0-9]{7})))$");
        private Regex _emailRegex = new Regex("^[A-Za-zČĆŠĐŽčćšđž0-9.]*@[a-zčćšđž]*[.][a-zčćšđž.]*$");
        private Regex _zvanjeRegex = new Regex("^[A-ZČĆŠĐŽa-zčćšđž_ ]+$");
        private Regex _brojLicneKarteRegex = new Regex("^[0-9]{9}$");
        private Regex _godineStazaRegex = new Regex("^[0-9]{1,2}$");

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
                        //return "format: dd/mm/gggg";

                    if (DatumPom > DateTime.Now)
                        return "mora biti datum iz proslosti";
                }
                else if (columnName == "UlicaStanovanja")
                {
                    if (string.IsNullOrEmpty(UlicaStanovanja))
                        return "*ulica";

                    Match match = _ulicaGradDrzavaRegex.Match(UlicaStanovanja);
                    if (!match.Success)
                        return "format: Ulica";
                }
                else if (columnName == "BrojUliceStanovanja")
                {
                    if (string.IsNullOrEmpty(BrojUliceStanovanja))
                        return "*broj ulice";

                    Match match = _brojUliceRegex.Match(BrojUliceStanovanja);
                    if (!match.Success)
                        return "format: 7/7a";
                }
                else if (columnName == "GradStanovanja")
                {
                    if (string.IsNullOrEmpty(GradStanovanja))
                        return "*grad";

                    Match match = _ulicaGradDrzavaRegex.Match(GradStanovanja);
                    if (!match.Success)
                        return "format: Grad";
                }
                else if (columnName == "DrzavaStanovanja")
                {
                    if (string.IsNullOrEmpty(DrzavaStanovanja))
                        return "*drzava";

                    Match match = _ulicaGradDrzavaRegex.Match(DrzavaStanovanja);
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
                else if (columnName == "UlicaKancelarije")
                {
                    if (string.IsNullOrEmpty(UlicaKancelarije))
                        return "*ulica";

                    Match match = _ulicaGradDrzavaRegex.Match(UlicaKancelarije);
                    if (!match.Success)
                        return "format: Ulica";
                }
                else if (columnName == "BrojUliceKancelarije")
                {
                    if (string.IsNullOrEmpty(BrojUliceKancelarije))
                        return "*broj ulice";

                    Match match = _brojUliceRegex.Match(BrojUliceKancelarije);
                    if (!match.Success)
                        return "format: 7/7a";
                }
                else if (columnName == "GradKancelarije")
                {
                    if (string.IsNullOrEmpty(GradKancelarije))
                        return "*grad";

                    Match match = _ulicaGradDrzavaRegex.Match(GradKancelarije);
                    if (!match.Success)
                        return "format: Grad";
                }
                else if (columnName == "DrzavaKancelarije")
                {
                    if (string.IsNullOrEmpty(DrzavaKancelarije))
                        return "*drzava";

                    Match match = _ulicaGradDrzavaRegex.Match(DrzavaKancelarije);
                    if (!match.Success)
                        return "format: Drzava";
                }
                else if (columnName == "BrojLicneKarte")
                {
                    if (string.IsNullOrEmpty(BrojLicneKarte))
                        return "*broj licne karte";

                    Match match = _brojLicneKarteRegex.Match(BrojLicneKarte);
                    if (!match.Success)
                        return "format: 9 cifara";
                }
                else if (columnName == "Zvanje")
                {
                    if (string.IsNullOrEmpty(Zvanje))
                        return "*zvanje";

                    Match match = _zvanjeRegex.Match(Zvanje);
                    if (!match.Success)
                        return "format: Zvanje/zvanje";
                }
                else if (columnName == "GodineStaza")
                {
                    if (GodineStaza == 0)
                        return "*godine staza";

                    Match match = _godineStazaRegex.Match(GodineStaza.ToString());
                    if (!match.Success)
                        return "format: 1 do 2 broja";
                }
                return null;
            }
        }

        public string Error => null;

        private readonly string[] _validatedProperties = { "Ime", "Prezime", "DatumPom", "UlicaStanovanja", "BrojUliceStanovanja", "GradStanovanja", "DrzavaStanovanja", "Telefon",
            "Email", "UlicaKancelarije", "BrojUliceKancelarije", "GradKancelarije", "DrzavaKancelarije", "BrojLicneKarte", "Zvanje", "GodineStaza" };

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
        public void FromCSV(string[] values)
        {
            prezime = values[0];
            ime = values[1];
            datum_rodjenja = DateOnly.ParseExact(values[2], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string[] adresa = values[3].Split(',');
            adresa_stanovanja.Ulica = adresa[0];
            adresa_stanovanja.Broj = adresa[1];
            adresa_stanovanja.Grad = adresa[2];
            adresa_stanovanja.Drzava = adresa[3];
            Telefon = values[4];
            adresa = values[5].Split(',');
            adresa_kancelarije.Ulica = adresa[0];
            adresa_kancelarije.Broj = adresa[1];
            adresa_kancelarije.Grad = adresa[2];
            adresa_kancelarije.Drzava = adresa[3];
            br_lk = values[6];
            zvanje = values[7];
            email= values[8];
            godine_staza = int.Parse(values[9]);
            string[] predmeti = values[10].Split(',');
            if (values[10].Length>0)
            {
                foreach (string sp in predmeti)
                {
                    Predmet p = new Predmet();
                    spisak_predmeta.Add(p);
                    p.Sifra = sp;
                }
            }
        }

        public string[] ToCSV()
        {
            string adresas = adresa_stanovanja.Ulica + "," + adresa_stanovanja.Broj + "," + adresa_stanovanja.Grad + "," + adresa_stanovanja.Drzava;
            string adresak = adresa_kancelarije.Ulica + "," + adresa_kancelarije.Broj + "," + adresa_kancelarije.Grad + "," + adresa_kancelarije.Drzava;
            string predmeti = "";
            bool pred=false;
            foreach (Predmet p in spisak_predmeta)
            {
                predmeti += p.Sifra.ToString() + ",";
                pred=true;
            }
            if(pred)
                predmeti = predmeti.Remove(predmeti.Length - 1);
            string[] csvValues =
            {
                prezime,
                ime,
                datum_rodjenja.ToString("dd/MM/yyyy"),
                adresas,
                telefon.ToString(),
                adresak,
                br_lk,
                zvanje,
                email,
                godine_staza.ToString(),
                predmeti
            };
            return csvValues;
        }
 /*       public string ToString()
        {
            string predmet = "";
            if (spisak_predmeta.Count>0)
            {
                foreach (Predmet p in spisak_predmeta)
                {
                    predmet += p.Naziv + ", ";
                }
                predmet = predmet.Remove(predmet.Length - 2);
            }
            return "-------------------------------------------------------------------\n"+"Profesor: " + zvanje + " " + ime + " " + prezime + "\nDatum rodjenja: " + datum_rodjenja.ToString() + "\nAdresa stanovanja: " + adresa_stanovanja.ToString() + "\nAdresa kancelarije: " + adresa_kancelarije.ToString() + "\nBroj licne karte: " + br_lk.ToString()+ "\nBroj telefona: "+ telefon+ "\nGodine staza: "+godine_staza.ToString()+ "\nPredmeti: "+predmet;

        }*/
    }
}
