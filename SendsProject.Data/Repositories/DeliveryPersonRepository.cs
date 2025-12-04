using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Data.Repositories
{
    public class DeliveryPersonRepository : IDeliveryPersonRepository
    {
        private readonly DataContext _context;
        public DeliveryPersonRepository(DataContext context)
        {
            _context = context;
        }
        public List<DeliveryPerson> GetDeliveryPerson()
        {
            return _context.DeliveryPeople;
        }
        public DeliveryPerson GetDeliveryPersonById(int id)
        {
            return _context.DeliveryPeople.Find(d => d.DeliveryPersonId == id);
        }
        public DeliveryPerson PostDeliveryPerson(DeliveryPerson deliveryPerson)
        {
             _context.DeliveryPeople.Add(deliveryPerson);
            return deliveryPerson;
        }
        public void PutDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            var d=GetDeliveryPersonById(deliveryPerson.DeliveryPersonId);
            d.WorkDays = deliveryPerson.WorkDays;
            d.StartTime = deliveryPerson.StartTime;
            d.EndTime = deliveryPerson.EndTime;
            d.Name = deliveryPerson.Name;
            d.Phone = deliveryPerson.Phone;
            
        }
        public void DeleteDeliveryPerson(int id)
        {
            var d = GetDeliveryPersonById(id);
            _context.DeliveryPeople.Remove(d);
        }
    }
}
