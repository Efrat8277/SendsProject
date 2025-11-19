using System.ComponentModel.DataAnnotations;

namespace SendsProject.Core.Models.Classes
{
    public class Recipient
    {
        public int RecipientId { get; set; }
        public string Identity { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

    }
}
