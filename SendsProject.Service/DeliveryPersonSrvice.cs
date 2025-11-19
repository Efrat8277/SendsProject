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

        public List<DeliveryPerson> GetDeliveryPerson()
        {
            return _deliveryPersonRepository.GetDeliveryPerson();
        }

        public DeliveryPerson GetDeliveryPerson(int id)
        {
            return _deliveryPersonRepository.GetDeliveryPersonById(id);
        }

    }
}
