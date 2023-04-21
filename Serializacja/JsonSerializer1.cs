using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;  //WEW.Extension

namespace Serializacja
{
    internal class JsonSerializer1
    {
        class Rates
        {
            [JsonProperty("currency")]
            public string CurrencyName { get; set; }

            [JsonProperty("code")]
            public string CurrencyCode { get; set; }

            [JsonProperty("mid")]
            public double AverageRate { get; set; }
        }

        public static void NBP()
        {
            WebClient wb = new WebClient();
            string s = wb.DownloadString("http://api.nbp.pl/api/exchangerates/tables/A/?format=json");
            JArray ja = JArray.Parse(s);
            IList<JToken> results = ja[0]["rates"].Children().ToList();

            List<Rates> rates = new List<Rates>();  
            foreach (JToken token in results)
            {
                Rates rate = token.ToObject<Rates>();
                rates.Add(rate);
                Console.WriteLine($"code : {rate.CurrencyCode}  mid : {rate.AverageRate}");
            }
            Console.ReadKey();
        }

        class MyUser
        {
            public string FName { get; set; }
            public string LName { get; set; }
        }
        public static void ApplyJason()
        {
            //string s = @"
            //    'fname' : 'Jan',
            //    ''lname' : 'Kowalski',
            //    'menager' : false
            //";
            string s = "{ \"fname\" : \"Jan\", \"lname\" : \"Kowalski\" \"menager\" : false}";
            MyUser user = JsonConvert.DeserializeObject<MyUser>(s,new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore
            });
            Console.WriteLine($"{user.FName} {user.LName}");
            Console.ReadKey();  
        }
        public static void Create()
        {
            EmployeeJson emp1 = new EmployeeJson()
            {
                Id = 123,
                FirstName = "Jan",
                LastName = "Kowalski",
                IsMenager = false,
                StartAt = new DateTime(2022, 1, 1)
            };
            EmployeeJson emp2 = new EmployeeJson()
            {
                Id = 123,
                FirstName = "Marek",
                LastName = "Kowalski",
                IsMenager = false,
                StartAt = new DateTime(2022, 1, 1)
            };
            EmployeeJson emp3 = new EmployeeJson()
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
            EmployeeJson[] empArray = new EmployeeJson[]
            {
                emp1, emp2, emp3
            };

            // Serializacja (binarna)
            using (FileStream fs = new FileStream("json1.json",FileMode.Create))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(EmployeeJson[]));
                serializer.WriteObject(fs, empArray);
            }

            // Deserializacja (binarna)
            using (FileStream fs = new FileStream("json1.json", FileMode.Open))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(EmployeeJson[]));
                EmployeeJson[] empS = serializer.ReadObject(fs) as EmployeeJson[];
                if (empS != null)
                {
                    Console.WriteLine(empS.Length);
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string s = js.Serialize(empArray);
            File.WriteAllText("Json2.json", s);

            EmployeeJson[] emps2 = js.Deserialize<EmployeeJson[]>(s);
            Console.WriteLine(emps2.Length);
            Console.ReadKey();

            // Serializacja NewtonSoftJson
            s = JsonConvert.SerializeObject(empArray, Formatting.Indented, new JsonSerializerSettings 
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            File.WriteAllText("Json3.json", s);

            emps2 = JsonConvert.DeserializeObject<EmployeeJson[]>(s);
            Console.WriteLine(emps2.Length);
        }

    }
}
