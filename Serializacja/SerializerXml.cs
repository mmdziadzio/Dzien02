using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Serializacja
{
    internal class SerializerXml
    {
        public static void Create()
        {
            EmployeeXml emp1 = new EmployeeXml()
            {
                Id = 123,
                FirstName = "Jan",
                LastName = "Kowalski",
                IsMenager = false,
                StartAt = new DateTime(2022, 1, 1),
                ExtraData = new List<string>() { "AAA", "BBB" }
            };
            EmployeeXml emp2 = new EmployeeXml()
            {
                Id = 123,
                FirstName = "Marek",
                LastName = "Kowalski",
                IsMenager = true,
                AccessRooms = new List<int>() { 4, 5, 8 },
                StartAt = new DateTime(2022, 1, 1)
            };
            EmployeeXml emp3 = new EmployeeXml()
            {
                Id = 123,
                FirstName = "Sebastian",
                LastName = "Kowalski",
                IsMenager = false,
                StartAt = new DateTime(2022, 1, 1)
            };
            emp1.SetToken(Guid.NewGuid().ToString());
            emp2.SetToken(Guid.NewGuid().ToString());
            emp3.SetToken(Guid.NewGuid().ToString());
            EmployeeXml[] empArray = new EmployeeXml[]
            {
                emp1, emp2, emp3
            };

            // Serializacja (binarna)
            using (FileStream fs = new FileStream("dumpXml.xml",FileMode.Create))
            {
                XmlSerializer xs = new XmlSerializer(typeof(EmployeeXml[]));
                xs.Serialize(fs, empArray);
            }

            // Deserializacja (binarna)
            using (FileStream fs = new FileStream("dumpXml.xml", FileMode.Open))
            {
                XmlSerializer xs = new XmlSerializer(typeof(EmployeeXml[]));
                //EmployeeXml[] empS = xs.Deserialize(fs) as EmployeeXml[];
                EmployeeXml[] empS = (EmployeeXml[])xs.Deserialize(fs);
                if (empS != null)
                {
                    Console.WriteLine(empS.Length);
                }
            }

        }

    }
}
