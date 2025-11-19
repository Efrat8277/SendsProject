using System.ComponentModel.DataAnnotations;

namespace SendsProject.Core.Models.Classes
{
    public class DeliveryPerson
    {
        public int DeliveryPersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int WorkDays { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
