using System.ComponentModel.DataAnnotations;

namespace SendsProject.Classes
{
    public class Package
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public string SenderName { get; set; } // שם שולח
        public DateTime SendDate { get; set; } // תאריך שליחה
        public bool IsSentToRecipient { get; set; } // חבילה נשלחה לנמען


    }
}
