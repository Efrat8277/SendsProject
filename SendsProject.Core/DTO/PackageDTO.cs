using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.DTO
{
    public class PackageDTO
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public string SenderName { get; set; } // שם שולח
        public DateTime SendDate { get; set; } // תאריך שליחה
        public bool IsSentToRecipient { get; set; } // חבילה נשלחה לנמען
        public int DeliveryPersonId { get; set; }
        public int RecipientId { get; set; }
    }
}
