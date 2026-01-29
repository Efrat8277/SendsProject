using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SendsProject.Core.DTO;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Services;
using SendsProject.Models;
using SendsProject.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientController : ControllerBase
    {

        private readonly IRecipientService _recipientService;
        private readonly IMapper _mapper;

        public RecipientController(IRecipientService recipientService,IMapper mapper)
        {
            _recipientService = recipientService;
            _mapper = mapper;
        }

        // GET: api/<RecipientController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var recipients = await _recipientService.GetRecipientsAsync();
            return Ok(_mapper.Map<List<RecipientDTO>>(recipients));
        }

        // GET api/<RecipientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var recipient = await _recipientService.GetRecipientByIdAsync(id);
            if (recipient != null)
            {
                var rec = _mapper.Map<RecipientDTO>(recipient);
                return Ok(rec);
            }
            return NotFound();
        }

        // POST api/<RecipientController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RecipientPostModel value)
        {
            var recipient = await _recipientService.GetRecipientByIdAsync(value.RecipientId);
            if (recipient != null)
            {
                return Conflict();
            }
            var recipient1 = _mapper.Map<Recipient>(value);
            var rec=await _recipientService.PostRecipientAsync(recipient1);
            return Ok(rec);
        }

        // PUT api/<RecipientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Recipient value)
        {
            var index = await _recipientService.GetRecipientByIdAsync(id);
            if (index == null)
            {
                return Conflict();
            }
            await _recipientService.PutRecipientAsync(value);
            return Ok();
        }

        // DELETE api/<RecipientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var recipient =await _recipientService.GetRecipientByIdAsync(id);
            if (recipient == null)
            {
                return Conflict();
            }
           await _recipientService.DeleteRecipientAsync(id);
            return Ok(recipient);
        }
    }
}
