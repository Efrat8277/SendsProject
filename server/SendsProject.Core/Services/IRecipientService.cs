using SendsProject.Core.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Core.Services
{
    public interface IRecipientService
    {
        public Task<List<Recipient>> GetRecipientsAsync();
        public Task<Recipient> GetRecipientByIdAsync(int recipientId);
        public Task<Recipient> PostRecipientAsync(Recipient recipient);
        public Task PutRecipientAsync(Recipient recipient);
        public Task DeleteRecipientAsync(int id);
    }
}
