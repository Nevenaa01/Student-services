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
    class ProfesorStorage
    {
        private readonly string StoragePath = string.Format("..{0}..{0}..{0}Data{0}profesor.txt", Path.DirectorySeparatorChar);
        private readonly Serializer<Profesor> _serializer;

        public ProfesorStorage()
        {
            _serializer = new Serializer<Profesor>();
        }

        public List<Profesor> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Profesor> profesori)
        {
            _serializer.toCSV(StoragePath, profesori);
        }
    }
}
