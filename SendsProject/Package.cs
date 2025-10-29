using System.ComponentModel.DataAnnotations;

namespace SendsProject
{
    public class Package
    {
        [Key]
        public int Id { get; set; }

        public int RecipientId { get; set; }
        public Recipient Recipient { get; set; }

        public double Weight { get; set; } // משקל

        public string SenderName { get; set; } // שם שולח
        public DateTime SendDate { get; set; } // תאריך שליחה
        public bool IsSentToRecipient { get; set; } // חבילה נשלחה לנמען
        public int DeliveryPersonId { get; set; } // קוד שליח
        public DeliveryPerson DeliveryPerson { get; set; } // הקשר עם שליח


    }
}
