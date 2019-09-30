using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using MediaSystem_Persistency;
using System.Runtime.Serialization.Formatters.Binary;

namespace MediaTest
{
    [TestClass]
    public class UnitTest1
    {
        string txtFileName = "datafile.txt";
        string binFileName = "datafile.bin";

        [TestInitialize]
        public void Setup()
        {
            StreamWriter writer = null;

            try
            {
                FileStream stream = new FileStream(txtFileName, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(stream);
                writer.WriteLine("Sgt. Pepper's Lonely Hearts Club Band, 1967, The Beatles, 40, +");
                writer.WriteLine("Are you Experienced, 1967, Jimi Hendrix, 40, -");
                writer.WriteLine("Abbey Road, 1969, The Beatles, 47, +");
                writer.WriteLine("Revolver, 1966, The Beatles, 35, +");
                writer.WriteLine("Pet Sounds, 1966, The Beach Boys,35,  -");
                writer.WriteLine("Highway 61 Revisited, 1965, Bob Dylan, 51, -");
                writer.WriteLine("The Dark Side of the Moon, 1973, Pink Floyd, 42, +");
            }
            catch (IOException e)
            {
                Console.WriteLine("I/O Error occurred: " + e.Message);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        [TestMethod]
        public void TestSaveBinary()
        {
            List<CD> cds = readTextFile(txtFileName);
            saveBinaryFile(binFileName, cds);
            List<CD> cdsFromBinary = readBinaryFile(binFileName);
            Assert.AreEqual(7, cdsFromBinary.Count);
            Assert.AreEqual(0, cdsFromBinary[0].PlayingTime);
            
        }

        private void saveBinaryFile(string fileName, List<CD> cds)
        {
            Stream stream = null;
            try
            {
                stream = File.Create(fileName);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(stream, cds);
            }
            catch (IOException e)
            {
                Assert.Fail("IOException occurred: {0}", e.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
        }

        private List<CD> readBinaryFile(string fileName)
        {
            List<CD> result = new List<CD>();
            Stream stream = null;
            try
            {
                if (File.Exists(fileName))
                {
                    stream = File.OpenRead("datafile.bin");
                    BinaryFormatter deserializer = new BinaryFormatter();
                    result = (List<CD>)deserializer.Deserialize(stream);
                }
            }
            catch (IOException e)
            {
                Assert.Fail("IOException occurred: {0}", e.Message);
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return result;
        }

        private List<CD> readTextFile(string fileName)
        {
            StreamReader reader = null;
            List<CD> cds = new List<CD>();
            CD cd;
            try
            {
                FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                reader = new StreamReader(stream);
                while (reader.Peek() != -1) //Continue until end of file
                {
                    string line = reader.ReadLine(); //read a line from the file
                    string[] fields = line.Split(',');//split the line into fields using a comma as delimiter
                    string title = fields[0];
                    int year = int.Parse(fields[1]);
                    string artist = fields[2];
                    int time = int.Parse(fields[3]);
                    cd = new CD(title, time, year, artist); //create the CD object
                    cds.Add(cd); //add to list of CDs
                }
                Assert.AreEqual(7, cds.Count, "Incorrect number of objects in the list of CDs");

            }
            catch (IOException e)
            {
                Assert.Fail("IOException occurred: {0}", e.Message);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
            return cds;
        }
    }
}
