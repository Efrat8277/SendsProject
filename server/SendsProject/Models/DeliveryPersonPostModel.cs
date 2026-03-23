using SendsProject.Core.Models.Classes;

namespace SendsProject.Models
{
    public class DeliveryPersonPostModel
    {
        public int DeliveryPersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public eWorkDays WorkDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}
