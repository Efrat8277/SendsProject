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
        public List<Recipient> GetRecipients();
        public Recipient GetRecipientById(int recipientId);
        public Recipient PostRecipient(Recipient recipient);
        public void PutRecipient(Recipient recipient);
        public void DeleteRecipient(int id);
    }
}
