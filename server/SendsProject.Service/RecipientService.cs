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
            var r = _recipientRepository.PostRecipient(recipient);
            await _recipientRepository.SaveAsync();

           
            return r;
        }

        public async Task PutRecipientAsync(Recipient recipient)
        {
           await _recipientRepository.PutRecipientAsync(recipient);
           await _recipientRepository.SaveAsync();

        }

        public async Task DeleteRecipientAsync(int id)
        {
            var recipient = await _recipientRepository.GetRecipientByIdAsync(id);
            if (recipient == null) return;
            if (recipient.Packages != null && recipient.Packages.Any())
            {
                // זריקת שגיאה שתחזור ל-React כ-400 Bad Request עם הודעה ברורה
                throw new InvalidOperationException("לא ניתן למחוק נמען שיש לו חבילות במערכת.");
            }
            await _recipientRepository.DeleteRecipientAsync(id);
           await _recipientRepository.SaveAsync();

        }

    }
}
