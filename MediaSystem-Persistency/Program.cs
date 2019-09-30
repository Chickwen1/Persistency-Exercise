using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MediaSystem_Persistency
{

    class Program
    {

        static void Main(string[] args)
        {
        }

        public static void SaveCDs(List<CD> cds)
        {
            //Should also have try/catch/finally clauses included
            Stream stream = File.Create("datafile.bin");
            BinaryFormatter serializer = new BinaryFormatter();
            serializer.Serialize(stream, cds);
            stream.Close();
        }

        public static List<CD> GetAllCDs()
        {
            List<CD> cds = null;
            if (File.Exists("datafile.bin"))
            {
                Stream stream = File.OpenRead("datafile.bin");
                BinaryFormatter deserializer = new BinaryFormatter();
                cds = (List<CD>)deserializer.Deserialize(stream);
                stream.Close();
            }
            return cds;
        }
    }
}
