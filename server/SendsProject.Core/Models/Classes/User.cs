using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SendsProject.Core.Models.Classes
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public eRole Role { get; set; }


    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum eRole
    {
        recipient=0, delivary_person=1, admin=2
    }
}
