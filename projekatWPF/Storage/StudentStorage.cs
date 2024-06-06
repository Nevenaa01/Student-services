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
    class StudentStorage
    {
        private readonly string StoragePath = string.Format("..{0}..{0}..{0}Data{0}student.txt", Path.DirectorySeparatorChar);

        private readonly Serializer<Student> _serializer;

        public StudentStorage()
        {
            _serializer = new Serializer<Student>();
        }

        public List<Student> Load()
        {
            return _serializer.fromCSV(StoragePath);
        }

        public void Save(List<Student> students)
        {
            _serializer.toCSV(StoragePath, students);
        }
    }
}
