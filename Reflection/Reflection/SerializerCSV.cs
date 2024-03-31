using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Reflection
{
    internal class SerializerCSV : ISerializerCSV
    {
        public T Deserialize<T>(string obj, char separator)
        {
            var rows = obj.Split("\n");
            var fields = rows[0].Split(separator).ToList();
            var values = rows[1].Split(separator);
            Type type = typeof(T);
            dynamic res = Activator.CreateInstance(type);
            var fieldsRes = type.GetFields();
            for(int i = 0; i < fieldsRes.Length; i++)
            {
                if (fieldsRes[i] != null && fields.Contains(fieldsRes[i].Name))
                {
                    var index = fields.IndexOf(fieldsRes[i].Name);
                    var f = res.GetType().GetField(fieldsRes[i].Name);
                    Type t = Nullable.GetUnderlyingType(f.FieldType) ?? f.FieldType;
                    var v = Convert.ChangeType(values[index], t);
                    f.SetValue(res, v);
                }
            }
            return (T)res;
        }

        public string Serialize(object obj, char separator)
        {
            StringBuilder result = new StringBuilder();
            var type = obj.GetType();
            if (type != null)
            {
                var fields = type.GetFields();
                var fieldsCount = fields.Length;
                for (int i = 0; i < fieldsCount; i++)
                {
                    result.Append(fields[i].Name);
                    if (i != fieldsCount - 1)
                    {
                        result.Append(separator);
                    }
                }
                result.Append("\n");
                for (int i = 0; i < fieldsCount; i ++)
                {
                    result.Append(fields[i].GetValue(obj));
                    if (i != fieldsCount - 1)
                    {
                        result.Append(separator);
                    }
                }
            }
            return result.ToString();
        }
    }
}
