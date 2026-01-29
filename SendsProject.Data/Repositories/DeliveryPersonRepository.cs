using Microsoft.EntityFrameworkCore;
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
        public async Task<List<DeliveryPerson>> GetDeliveryPersonAsync()
        {
            return await _context.DeliveryPeople.Include(d => d.Packages).ToListAsync();
        }
        public async Task<DeliveryPerson> GetDeliveryPersonByIdAsync(int id)
        {
            return await _context.DeliveryPeople.FirstOrDefaultAsync(d => d.DeliveryPersonId == id);
        }
        public DeliveryPerson PostDeliveryPerson(DeliveryPerson deliveryPerson)
        {
            _context.DeliveryPeople.Add(deliveryPerson);
            return deliveryPerson;
        }
        public async Task PutDeliveryPersonAsync(DeliveryPerson deliveryPerson)
        {
            var d = await GetDeliveryPersonByIdAsync(deliveryPerson.DeliveryPersonId);
            d.WorkDays = deliveryPerson.WorkDays;
            d.StartTime = deliveryPerson.StartTime;
            d.EndTime = deliveryPerson.EndTime;
            d.Name = deliveryPerson.Name;
            d.Phone = deliveryPerson.Phone;

        }
        public async Task DeleteDeliveryPersonAsync(int id)
        {
            var d = await GetDeliveryPersonByIdAsync(id);
            _context.DeliveryPeople.Remove(d);
        }

        public async Task SaveAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
