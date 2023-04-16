using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Serializacja
{
    [Serializable]
    public class EmployeeXml
    {
        [XmlAttribute("id")]
        public int Id { get; set; }

        [XmlElement("Imie")]
        public string FirstName { get; set; }

        [XmlElement("Nazwisko")]
        public string LastName { get; set; }
        public bool? IsMenager { get; set; }
        public List<int> AccessRooms { get; set; }

        [XmlIgnore]
        public List<string> ExtraData { get; set; }
        public DateTime StartAt { get; set; }

        private string Token;

        public void SetToken(string token)
        {
            Token = token;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

    }
}
