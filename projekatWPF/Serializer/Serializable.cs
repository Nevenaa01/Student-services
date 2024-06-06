using System;
using System.Collections.Generic;
using System.Text;

namespace projekatWPF.Serializer
{
    public interface Serializable
    {

        string[] ToCSV();

        void FromCSV(string[] values);

    }
}
