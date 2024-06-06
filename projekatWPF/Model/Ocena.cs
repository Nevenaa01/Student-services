//using ConsoleApp.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using projekatWPF.Serializer;

namespace projekatWPF.Model
{
    public class Ocena : Serializable
    {
        Student student;
        Predmet predmet;
        int vrednost;
        DateOnly datumPolaganja;
        DateTime datumPom;

        public Ocena()
        {
            student = new Student(); 
            predmet = new Predmet();
            datumPom = DateTime.Now;
            datumPolaganja = new DateOnly(datumPom.Year, datumPom.Month, datumPom.Day);
        }

        public Ocena(Predmet predmet, int vrednost, DateOnly datumPolaganja)
        {
            student = new Student();
            this.predmet = predmet;
            this.vrednost = vrednost;
            this.datumPolaganja = datumPolaganja;
        }

        public int Vrednost
        {
            get => vrednost;
            set
            {
                if (value != vrednost)
                {
                    vrednost = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateOnly DatumPolaganja
        {
            get { return datumPolaganja; }
            set { datumPolaganja = value; }
        }

        public Student Student
        {
            get { return student;}
            set { student = value; }
        }
        public Predmet Predmet
        {
            get { return predmet; }
            set { predmet = value; }
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
                    DatumPolaganja = new DateOnly(DatumPom.Year, DatumPom.Month, DatumPom.Day);
                }
            }
        }
        public string Error => null;
        private Regex _vrednostRegex = new Regex("6|7|8|9|10");
        private Regex _datumPolaganjaRegex = new Regex("([1-3][0-9])|(0[1-9])/([1-3][0-9])|(0[1-9])/[12][0-9]{3}$");
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Vrednost")
                {
                    if (string.IsNullOrEmpty(Vrednost.ToString()))
                        return "*vrednst";

                    Match match = _vrednostRegex.Match(Vrednost.ToString());
                    if (!match.Success)
                        return "Broj od 6 do 10";
                }
                
                else if (columnName == "DatumPom")
                {
                    if (DatumPom == DateTime.Now)
                        return "*datum rodjenja";

                    Match match = _datumPolaganjaRegex.Match(new DateOnly(DatumPom.Year, DatumPom.Month, DatumPom.Day).ToString());
                    if (!match.Success)
                        return "format: dd/mm/gggg";

                    if (DatumPom > DateTime.Now)
                        return "mora biti datum iz proslosti";
                }
               
                return null;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private readonly string[] _validatedProperties = { "DatumPom", "Vrednost" };

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




        /*public string IndeksStudenta()
        {
            string s = "";
            foreach (Student st in polozili)
                if (polozili.Last() == st && polozili.First() == st)
                    s = s + st.BrIndeksa;
                else
                    s = s + ", " + st.BrIndeksa;
            return s;
        }
        
        public void dodavanjeStudenta(Student s)
        {
            polozili.Add(s);
        }*/

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                student.BrIndeksa,
                predmet.Sifra.ToString(),
                vrednost.ToString(),
                datumPolaganja.ToString("dd/MM/yyyy")
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            student.BrIndeksa = values[0];
            predmet.Sifra = values[1];
            vrednost = int.Parse(values[2]);
            DatumPolaganja = DateOnly.ParseExact(values[3], "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }
        public string ToString()
        {
            string ispis = "";
                ispis = "-------------------------------------------------------------------\n" + "Student: " + student.Ime+" "+student.Prezime + "\nPredmet: " + predmet.Naziv + "\nOcena: " + vrednost.ToString() + "\nDatum polaganja: " + datumPolaganja.ToString("dd/MM/yyyy");

            return ispis;
        }
    }
}
