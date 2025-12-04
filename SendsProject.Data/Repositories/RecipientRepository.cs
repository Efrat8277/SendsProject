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
        public List<Recipient> GetRecipients()
        {
            return _context.Recipients;
        }
        public Recipient GetRecipientById(int recipientId)
        {
            return _context.Recipients.Find(r => r.RecipientId == recipientId);
        }

        public Recipient PostRecipient(Recipient recipient)
        {
            _context.Recipients.Add(recipient);
            return recipient;
        }

        public void PutRecipient(Recipient recipient)
        {
            var rec=GetRecipientById(recipient.RecipientId);
            rec.Address = recipient.Address;
            rec.Phone = recipient.Phone;
            rec.Name = recipient.Name; 
        }

        public void DeleteRecipient(int id)
        {
            var recipient = GetRecipientById(id);
            _context.Recipients.Remove(recipient);
        }
    }
}
