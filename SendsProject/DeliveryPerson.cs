using System.ComponentModel.DataAnnotations;

namespace SendsProject
{
    public class DeliveryPerson
    {
        [Key]
        public int DeliveryPersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public int WorkDays { get; set; }
        public TimeSpan StartTime { get; set; } 
        public TimeSpan EndTime { get; set; } 
       
    }
}
