using SendsProject.Classes;

namespace SendsProject
{
    public interface IDataContext
    {
        public List<Package> Packages { get; set; }
        //לבדוק
        public List<Recipient> Recipients { get; set; }
        public List<DeliveryPerson> DeliveryPeople { get; set; }
    }
}
