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
            var index = deliveryPerson.DeliveryPersonId;
            _context.DeliveryPeople[index].Name=deliveryPerson.Name;
            _context.DeliveryPeople[index].Phone=deliveryPerson.Phone;
            _context.DeliveryPeople[index].WorkDays=deliveryPerson.WorkDays;
            _context.DeliveryPeople[index].StartTime=deliveryPerson.StartTime;
            _context.DeliveryPeople[index].EndTime=deliveryPerson.EndTime;
        }
        public void DeleteDeliveryPerson(int id)
        {
            var index = _context.DeliveryPeople.FindIndex(d => d.DeliveryPersonId==id);
             _context.DeliveryPeople.RemoveAt(index);
        }
    }
}
