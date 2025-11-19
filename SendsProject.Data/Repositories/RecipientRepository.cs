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
    }
}
