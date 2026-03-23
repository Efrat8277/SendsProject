using SendsProject.Core.Models.Classes;

namespace SendsProject.Data
{
    public interface IDataContext
    {
        public List<Package> Packages { get; set; }
        //לבדוק
        public List<Recipient> Recipients { get; set; }
        public List<DeliveryPerson> DeliveryPeople { get; set; }
    }
}
