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
            var index= recipient.RecipientId;
            _context.Recipients[index].Name = recipient.Name;
            _context.Recipients[index].Address = recipient.Address;
            _context.Recipients[index].Phone = recipient.Phone;
        }

        public void DeleteRecipient(int id)
        {
            var index = _context.Recipients.FindIndex(r => r.RecipientId == id);
            _context.Recipients.RemoveAt(index);
        }
    }
}
