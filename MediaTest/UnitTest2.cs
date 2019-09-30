using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using MediaSystem_Persistency;
using System.IO;

namespace MediaTest
{
    [TestClass]
    public class UnitTest2
    {
        string fileName = "datafile.csv";
        [TestMethod]
        public void TestMethod1()
        {
            List<CD> cds = new List<CD>();
            cds.Add(new CD("Yellow Submarine", 40, 1964, "The Beatles"));
            cds.Add(new CD("Abbey Road", 47, 1969, "The Beatles"));
            StreamWriter writer = null;
            try {
                FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(stream);
                foreach (CD cd in cds)
                {
                    writer.WriteLine(cd.Title + "," + cd.YearReleased + "," + cd.Artist+","+cd.PlayingTime);
                }

            }
            catch (IOException e)
            {
                Console.WriteLine("IO Error occurred: " + e.Message);
                Assert.Fail(e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
            }
        }

        [TestMethod]
        public void TestReadFile()
        {
            StreamReader reader = null;
            List<CD> cdIn = new List<CD>();
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            reader = new StreamReader(stream);
            CD cd;
            while (reader.Peek() != -1)
            {
                string line = reader.ReadLine();
                string[] fields = line.Split(',');
                string title = fields[0];
                string artist = fields[2];
                int year = int.Parse(fields[1]);
                int time = int.Parse(fields[3]);
                cd = new CD(title, time, year, artist);
                cdIn.Add(cd);
            }
            Assert.AreEqual(2, cdIn.Count);
            Assert.AreEqual(1964, cdIn[0].YearReleased);
                
        }
    }
}
