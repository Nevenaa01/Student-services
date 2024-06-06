using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace projekatWPF.Model.DTO
{
    public class PredmetOcenaDTO : INotifyPropertyChanged
    {
        private string sifraPredmeta;
        private string nazivPredmeta;
        private int ESPBBodovi;
        private int ocena;
        private DateOnly datumPolaganja;

        public PredmetOcenaDTO()
        {
            sifraPredmeta = "";
            nazivPredmeta = "";
            ESPBBodovi = 0;
            ocena = 5;
            datumPolaganja = new DateOnly();
        }

        public PredmetOcenaDTO(string sifraPredmeta, string nazivPredmeta, int ESPBBodovi, int ocena, DateOnly datumPolaganja)
        {
            this.sifraPredmeta = sifraPredmeta;
            this.nazivPredmeta = nazivPredmeta;
            this.ESPBBodovi = ESPBBodovi;
            this.ocena = ocena;
            this.datumPolaganja = datumPolaganja;
        }

        public string Sifra
        {
            get => sifraPredmeta;
            set
            {
                if (value != sifraPredmeta)
                {
                    sifraPredmeta = value;
                    OnPropertyChanged();
                }

            }
        }

        public string Naziv
        {
            get => nazivPredmeta;
            set
            {
                if (value != nazivPredmeta)
                {
                    nazivPredmeta = value;
                    OnPropertyChanged();
                }

            }
        }

        public int ESPB
        {
            get => ESPBBodovi;
            set
            {
                if (value != ESPBBodovi)
                {
                    ESPBBodovi = value;
                    OnPropertyChanged();
                }

            }
        }

        public int Ocena
        {
            get => ocena;
            set
            {
                ocena = value;
                OnPropertyChanged();
            }
        }

        public DateOnly DatumPolaganja
        {
            get => datumPolaganja;
            set => datumPolaganja = value;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
