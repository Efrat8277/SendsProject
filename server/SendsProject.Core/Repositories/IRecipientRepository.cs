using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.Repositories
{
    public interface IRecipientRepository
    {
        public Task<List<Recipient>> GetRecipientsAsync();
        public Task<Recipient> GetRecipientByIdAsync(int recipientId);
        public Recipient PostRecipient(Recipient recipient);
        public Task PutRecipientAsync(Recipient recipient);
        public Task DeleteRecipientAsync(int id);
        public Task SaveAsync();

    }
}
