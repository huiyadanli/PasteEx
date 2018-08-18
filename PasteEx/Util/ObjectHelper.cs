using System;
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

        /// <summary>
        /// Can't work.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [Obsolete]
        public static string ComputeMD5(object obj)
        {
            if (obj == null)
            {
                return null;
            }
            if(!obj.GetType().IsSerializable) 
            {
                return null;
            }

            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);

            int bufferSize = 1024 * 16; //自定义缓冲区大小16K
            byte[] buffer = new byte[bufferSize];

            HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
            int readLength = 0;
            var output = new byte[bufferSize];
            while ((readLength = ms.Read(buffer, 0, buffer.Length)) > 0)
            {
                hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
            }
            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            string md5 = BitConverter.ToString(hashAlgorithm.Hash);
            hashAlgorithm.Clear();
            ms.Close();
            md5 = md5.Replace("-", "");
            return md5;
        }

    }
}
