using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.DTO
{
    public class DeliveryPersonDTO
    {
        public int DeliveryPersonId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public eWorkDays WorkDays { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public List<PackageDTO> Packages { get; set; } 

    }
}
