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
    public class RecipientRepository : IRecipientRepository
    {
        private readonly DataContext _context;
        public RecipientRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Recipient>> GetRecipientsAsync()
        {
            return await _context.Recipients.Include(r=>r.Packages).ThenInclude(p=>p.DeliveryPerson).ToListAsync();
        }
        public async Task<Recipient> GetRecipientByIdAsync(int recipientId)
        {
            return await _context.Recipients.FirstOrDefaultAsync(r => r.RecipientId == recipientId);
        }

        public Recipient PostRecipient(Recipient recipient)
        {
            _context.Recipients.Add(recipient);
            return recipient;
        }

        public async Task PutRecipientAsync(Recipient recipient)
        {
            var rec= await GetRecipientByIdAsync(recipient.RecipientId);
            rec.Address = recipient.Address;
            rec.Phone = recipient.Phone;
            rec.Name = recipient.Name; 
        }

        public async Task DeleteRecipientAsync(int id)
        {
            var recipient = await GetRecipientByIdAsync(id);
            _context.Recipients.Remove(recipient);
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
