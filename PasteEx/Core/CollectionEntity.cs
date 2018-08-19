using PasteEx.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PasteEx.Core
{
    public class CollectionEntity
    {
        public string Id { get; set; }

        public DataObject Storage { get; set; }

        public string Extension { get; set; }

        public object ResultObject { get; set; }

        public string FilePath { get; set; }

        private byte[] dataBytes;

        public CollectionEntity(DataObject dataObject)
        {
            Storage = dataObject;

            Hashtable hashtable = new Hashtable();
            string[] formats = dataObject.GetFormats();
            foreach (string format in formats)
            {
                if (dataObject.GetDataPresent(format, false))
                {
                    hashtable.Add(format, dataObject.GetData(format));
                }
            }
            dataBytes = ObjectHelper.SerializeObject(hashtable);
            Id = ObjectHelper.ComputeMD5(dataBytes);
        }
    }
}
