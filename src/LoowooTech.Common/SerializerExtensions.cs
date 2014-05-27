using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Newtonsoft.Json;

namespace LoowooTech.Common
{
    public static class SerializerExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static T ToObject<T>(this string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static byte[] ToBinary(this object obj)
        {
            return Encoding.UTF8.GetBytes(obj.ToJson());
        }

        public static T ToObject<T>(this byte[] datas)
        {
            var json = Encoding.UTF8.GetString(datas);
            return json.ToObject<T>();
        }
    }
}
