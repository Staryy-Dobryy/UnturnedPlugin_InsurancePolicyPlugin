using Newtonsoft.Json;
using Rocket.Core.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsurancePolicyPlugin.DataBase
{
    public class DataContext<T> where T : class, new()
    {
        public string DataPath { get; private set; }
        public DataContext(string dir, string fileName)
        {
            DataPath = Path.Combine(dir, fileName);
        }
        public void Save(T obj)
        {
            string objData = JsonConvert.SerializeObject(obj, Formatting.Indented);

            using (StreamWriter stream = new StreamWriter(DataPath, false))
            {
                stream.Write(objData);
            }
        }
        public T Read()
        {
            if (File.Exists(DataPath))
            {
                string dataText;
                using (StreamReader stream = File.OpenText(DataPath))
                {
                    dataText = stream.ReadToEnd();
                }
                if (dataText == null || dataText == "") return new T();
                return JsonConvert.DeserializeObject<T>(dataText);
            }
            else
            {
                return null;
            }
        }
    }
}
