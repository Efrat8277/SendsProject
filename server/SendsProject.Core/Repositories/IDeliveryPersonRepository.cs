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
        public Task<List<DeliveryPerson>> GetDeliveryPersonAsync();
        public Task<DeliveryPerson> GetDeliveryPersonByIdAsync(int id);
        public DeliveryPerson PostDeliveryPerson(DeliveryPerson deliveryPerson);
        public Task PutDeliveryPersonAsync(DeliveryPerson deliveryPerson);
        public Task DeleteDeliveryPersonAsync(int id);

        public Task SaveAsync();

    }
}
