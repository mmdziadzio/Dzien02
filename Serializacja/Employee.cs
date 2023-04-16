using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializacja
{
    [Serializable]
    internal class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsMenager { get; set; }
        public List<int> AccessRooms { get; set; }
        public List<string> ExtraData { get; set; } 
        public DateTime? StartAt { get; set; }

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
