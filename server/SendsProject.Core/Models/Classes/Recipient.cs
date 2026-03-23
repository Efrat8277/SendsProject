using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SendsProject.Core.Models.Classes
{
    public class Recipient
    {
        public int RecipientId { get; set; }
        public string Identity { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public List<Package> Packages { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
}
