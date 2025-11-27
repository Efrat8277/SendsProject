using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.Repositories
{
    public interface IDeliveryPersonRepository
    {
        public List<DeliveryPerson> GetDeliveryPerson();
        public DeliveryPerson GetDeliveryPersonById(int id);
        public DeliveryPerson PostDeliveryPerson(DeliveryPerson deliveryPerson);
        public void PutDeliveryPerson(DeliveryPerson deliveryPerson);
        public void DeleteDeliveryPerson(int id);


    }
}
