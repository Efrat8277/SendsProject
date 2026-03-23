using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SendsProject.Core.Models.Classes
{
    public class DeliveryPerson
    {
        public int DeliveryPersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public eWorkDays WorkDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        [JsonIgnore]
        public List<Package> Packages { get; set; }

        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }

    }
    [Flags]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum eWorkDays
    {
        None = 0,
        Sunday = 1,
        Monday = 2,
        Tuesday = 4,
        Wednesday = 8,
        Thursday = 16,
        Friday = 32,
        Saturday = 64
    }
}
