using Microsoft.AspNetCore.Mvc;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Services;
using SendsProject.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientController : ControllerBase
    {

        private readonly IRecipientService _recipientService;

        public RecipientController(IRecipientService recipientService)
        {
            _recipientService = recipientService;
        }

        // GET: api/<RecipientController>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_recipientService.GetRecipients());
        }

        // GET api/<RecipientController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var recipient = _recipientService.GetRecipientById(id);
            if (recipient != null)
            {
                return Ok(recipient);
            }
            return NotFound();
        }

        // POST api/<RecipientController>
        [HttpPost]
        public ActionResult Post([FromBody] Recipient value)
        {
            var recipient = _recipientService.GetRecipientById(value.RecipientId);
            if (recipient != null)
            {
                return Conflict();
            }
            recipient= _recipientService.PostRecipient(value);
            return Ok(recipient);
        }

        // PUT api/<RecipientController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Recipient value)
        {
            var index = _recipientService.GetRecipients().FindIndex(x => x.RecipientId == id);
            if (index == -1)
            {
                return Conflict();
            }
            _recipientService.PutRecipient(value);
            return Ok();
        }

        // DELETE api/<RecipientController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var recipient = _recipientService.GetRecipientById(id);
            if (recipient == null)
            {
                return Conflict();
            }
            _recipientService.DeleteRecipient(id);
            return Ok(recipient);
        }
    }
}
