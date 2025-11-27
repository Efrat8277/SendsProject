using SendsProject.Core.Models.Classes;
using SendsProject.Core.Repositories;
using SendsProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendsProject.Service
{
    public class RecipientService : IRecipientService
    {
        public readonly IRecipientRepository _recipientRepository;
        public RecipientService(IRecipientRepository recipientRepository)
        {
            _recipientRepository = recipientRepository;
        }

        public List<Recipient> GetRecipients()
        {
            return _recipientRepository.GetRecipients();
        }
        public Recipient GetRecipientById(int recipientId)
        {
            return _recipientRepository.GetRecipientById(recipientId);
        }

        public Recipient PostRecipient(Recipient recipient)
        {
            return _recipientRepository.PostRecipient(recipient);
        }

        public void PutRecipient(Recipient recipient)
        {
            _recipientRepository.PutRecipient(recipient);
        }

        public void DeleteRecipient(int id)
        {
            _recipientRepository.DeleteRecipient(id);
        }
    }
}
