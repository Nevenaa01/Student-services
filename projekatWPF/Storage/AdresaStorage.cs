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
    class AdresaStorage
    {
        private readonly string StoragePath = string.Format("..{0}..{0}..{0}Data{0}adresa.txt", Path.DirectorySeparatorChar);

        private readonly Serializer<Adresa> _serializer;


        public AdresaStorage()
        {
            _serializer = new Serializer<Adresa>();
        }

        public List<Adresa> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Adresa> adrese)
        {
            _serializer.toCSV(StoragePath, adrese);
        }
    }
}
