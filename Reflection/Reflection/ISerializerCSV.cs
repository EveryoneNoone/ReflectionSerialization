using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    internal interface ISerializerCSV
    {
        public string Serialize(object obj, char separator);
        public T Deserialize<T>(string obj, char separator);
    }
}
