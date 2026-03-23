using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;
using SendsProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Service
{
    public class DeliveryPersonSrvice : IDeliveryPersonService
    {
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        public DeliveryPersonSrvice(IDeliveryPersonRepository deliveryPersonRepository)
        {
            _deliveryPersonRepository = deliveryPersonRepository;
        }

        public async Task<List<DeliveryPerson>> GetDeliveryPersonAsync()
        {
            return await _deliveryPersonRepository.GetDeliveryPersonAsync();
        }

        public async Task<DeliveryPerson> GetDeliveryPersonByIdAsync(int id)
        {
            return await _deliveryPersonRepository.GetDeliveryPersonByIdAsync(id);
        }
        public async Task<DeliveryPerson> PostDeliveryPersonAsync(DeliveryPerson deliveryPerson)
        {
           var d=  _deliveryPersonRepository.PostDeliveryPerson(deliveryPerson);
           await _deliveryPersonRepository.SaveAsync();
            return d;


        }
        public async Task PutDeliveryPersonAsync(DeliveryPerson deliveryPerson)
        {
            await _deliveryPersonRepository.PutDeliveryPersonAsync(deliveryPerson);
            await  _deliveryPersonRepository.SaveAsync();

        }
        public async Task DeleteDeliveryPersonAsync(int id)
        {
           await _deliveryPersonRepository.DeleteDeliveryPersonAsync(id);
           await _deliveryPersonRepository.SaveAsync();

        }


    }
}
