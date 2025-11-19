using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Data.Repositories
{
    public class DeliveryPersonRepository
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
        public DeliveryPerson GetDeliveryPerson(int id)
        {
            return _context.DeliveryPeople.Find(d => d.DeliveryPersonId == id);
        }
    }
}
