using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.Services
{
    public interface IDeliveryPersonService
    {
        public Task<List<DeliveryPerson>> GetDeliveryPersonAsync();
        public Task<DeliveryPerson> GetDeliveryPersonByIdAsync(int id);
        public Task<DeliveryPerson> PostDeliveryPersonAsync(DeliveryPerson deliveryPerson);
        public Task PutDeliveryPersonAsync(DeliveryPerson deliveryPerson);
        public Task DeleteDeliveryPersonAsync(int id);
    }
}
