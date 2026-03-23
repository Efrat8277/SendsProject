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

        public async Task<List<Recipient>> GetRecipientsAsync()
        {
            return await _recipientRepository.GetRecipientsAsync();
        }
        public async Task<Recipient> GetRecipientByIdAsync(int recipientId)
        {
            return await _recipientRepository.GetRecipientByIdAsync(recipientId);
        }

        public async Task<Recipient> PostRecipientAsync(Recipient recipient)
        {
           await _recipientRepository.SaveAsync();

            var r= _recipientRepository.PostRecipient(recipient);
            return r;
        }

        public async Task PutRecipientAsync(Recipient recipient)
        {
           await _recipientRepository.PutRecipientAsync(recipient);
           await _recipientRepository.SaveAsync();

        }

        public async Task DeleteRecipientAsync(int id)
        {
           await _recipientRepository.DeleteRecipientAsync(id);
           await _recipientRepository.SaveAsync();

        }

    }
}
