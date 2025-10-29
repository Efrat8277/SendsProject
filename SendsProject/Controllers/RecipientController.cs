using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientController : ControllerBase
    {
        public static List<Recipient> recipients = { new Recipient { RecipientId = 2, Identity = "123456789", Name = "Eli", Phone = "054789654123", Address = "hneviim" } };

        // GET: api/<RecipientController>
        [HttpGet]
        public IEnumerable<Recipient> Get()
        {
            return recipients;
        }

        // GET api/<RecipientController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var recipient = recipients.Find(x => x.Id == id);
            if (recipient != null)
            {
                return Ok(e);
            }
            return NotFound();
        }

        // POST api/<RecipientController>
        [HttpPost]
        public void Post([FromBody] Recipient value)
        {
            recipients.Add(value);
        }

        // PUT api/<RecipientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Recipient value)
        {
            var index = recipients.FindIndex(recipients => recipients.Id == id);
            recipients[index].RecipientId=value.RecipientId;
            recipients[index].Name=value.Name;
            recipients[index].Phone=value.Phone;
            recipients[index].Address=value.Address;
        }

        // DELETE api/<RecipientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var recipient = recipients.Find(x => x.Id == id);
            recipients.Remove(recipient);
        }
    }
}
