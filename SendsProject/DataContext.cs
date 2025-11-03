using SendsProject.Classes;

namespace SendsProject
{
    public class DataContext:IDataContext
    {
        public List<Package> Packages { get; set; }
        public List<Recipient> Recipients { get; set; }
        public List<DeliveryPerson> DeliveryPeople { get; set; }


        public DataContext() 
        {
            Packages= new List<Package>() { new Package { Id = 1, Weight = 3.5, SenderName = "shimshon", SendDate = new DateTime(), IsSentToRecipient = false } };
            Recipients = new List<Recipient>() { new Recipient { RecipientId = 2, Identity = "789654123", Name = "lol", Phone = "0512369874", Address = "joi" } };
            DeliveryPeople = new List<DeliveryPerson> { new DeliveryPerson { DeliveryPersonId = 1, Name = "hgkjfd", Phone = "055555555" } };

    }
}
}
