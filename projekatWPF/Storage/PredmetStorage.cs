using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using projekatWPF.Serializer;
using projekatWPF.Model;
using System.IO;

namespace projekatWPF.Storage
{
    class PredmetStorage
    {
        private readonly string StoragePath = string.Format("..{0}..{0}..{0}Data{0}predmet.txt", Path.DirectorySeparatorChar);

        private readonly Serializer<Predmet> _serializer;


        public PredmetStorage()
        {
            _serializer = new Serializer<Predmet>();
        }

        public List<Predmet> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Predmet> predmeti)
        {
            _serializer.toCSV(StoragePath, predmeti);
        }
    }
}
