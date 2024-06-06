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
    class OcenaStorage
    {
        private readonly string StoragePath = string.Format("..{0}..{0}..{0}Data{0}ocena.txt", Path.DirectorySeparatorChar);

        private readonly Serializer<Ocena> _serializer;


        public OcenaStorage()
        {
            _serializer = new Serializer<Ocena>();
        }

        public List<Ocena> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Ocena> ocene)
        {
            _serializer.toCSV(StoragePath, ocene);
        }
    }
}
