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
    class KatedraStorage
    {
        private readonly string StoragePath = string.Format("..{0}..{0}..{0}Data{0}katedra.txt", Path.DirectorySeparatorChar);

        private readonly Serializer<Katedra> _serializer;


        public KatedraStorage()
        {
            _serializer = new Serializer<Katedra>();
        }

        public List<Katedra> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Katedra> katedre)
        {
            _serializer.toCSV(StoragePath, katedre);
        }
    }
}
