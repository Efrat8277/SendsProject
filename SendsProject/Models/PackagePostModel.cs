namespace SendsProject.Models
{
    public class PackagePostModel
    {
        public int Id { get; set; }
        public double Weight { get; set; }
        public string SenderName { get; set; } // שם שולח
        public DateTime SendDate { get; set; } // תאריך שליחה
        public bool IsSentToRecipient { get; set; } // חבילה נשלחה לנמען

        public int DeliveryPersonId {  get; set; }
        public int RcipientId {  get; set; }

    }
}
