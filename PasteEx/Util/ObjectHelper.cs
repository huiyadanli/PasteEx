using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace PasteEx.Util
{
    public class ObjectHelper
    {
        public static byte[] SerializeObject(object obj)
        {
            if (obj == null)
            {
                return null;
            }

            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            byte[] bytes = ms.GetBuffer();
            return bytes;
        }

        public static object DeserializeObject(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            object obj = null;
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(ms);
            ms.Close();
            return obj;
        }

        public static string ComputeMD5(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            MD5 md5 = MD5.Create();
            byte[] retVal = md5.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < retVal.Length; i++)
            {
                sb.Append(retVal[i].ToString("x2"));
            }
            return sb.ToString();
        }

    }
}
