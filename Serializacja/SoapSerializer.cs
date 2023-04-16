using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Threading.Tasks;

namespace Serializacja
{
    internal class SoapSerializer
    {
        public static void Create()
        {
            EmployeeSoap emp1 = new EmployeeSoap()
            {
                Id = 123,
                FirstName = "Jan",
                LastName = "Kowalski",
                IsMenager = false,
                StartAt = new DateTime(2022, 1, 1)
            };
            EmployeeSoap emp2 = new EmployeeSoap()
            {
                Id = 123,
                FirstName = "Marek",
                LastName = "Kowalski",
                IsMenager = false,
                StartAt = new DateTime(2022, 1, 1)
            };
            EmployeeSoap emp3 = new EmployeeSoap()
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
            EmployeeSoap[] empArray = new EmployeeSoap[]
            {
                emp1, emp2, emp3
            };

            // Serializacja (binarna)
            using (FileStream fs = new FileStream("dumpSoap.xml",FileMode.Create))
            {
                SoapFormatter sf = new SoapFormatter();
                sf.Serialize(fs, empArray);
            }

            // Deserializacja (binarna)
            using (FileStream fs = new FileStream("dumpSoap.xml", FileMode.Open))
            {
                SoapFormatter sf = new SoapFormatter();
                EmployeeSoap[] empS = sf.Deserialize(fs) as EmployeeSoap[];
                if (empS != null)
                {
                    Console.WriteLine(empS.Length);
                }
            }

        }

    }
}
