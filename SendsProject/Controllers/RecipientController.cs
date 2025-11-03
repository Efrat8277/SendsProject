using Microsoft.AspNetCore.Mvc;
using SendsProject.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientController : ControllerBase
    {

        private readonly IDataContext _dataContext;

        public RecipientController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: api/<RecipientController>
        [HttpGet]
        public IEnumerable<Recipient> Get()
        {
            return _dataContext.Recipients;
        }

        // GET api/<RecipientController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var recipient = _dataContext.Recipients.Find(x => x.RecipientId == id);
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
            var recipient = _dataContext.Recipients.Find(x=>x.RecipientId == value.RecipientId);
            if (recipient != null)
            {
                return Conflict();
            }
            _dataContext.Recipients.Add(value);
            return Ok(value);
        }

        // PUT api/<RecipientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Recipient value)
        {
            var index = _dataContext.Recipients.FindIndex(recipients => recipients.RecipientId == id);
            _dataContext.Recipients[index].RecipientId=value.RecipientId;
            _dataContext.Recipients[index].Name=value.Name;
            _dataContext.Recipients[index].Phone=value.Phone;
            _dataContext.Recipients[index].Address=value.Address;
        }

        // DELETE api/<RecipientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var recipient = _dataContext.Recipients.Find(x => x.RecipientId == id);
            _dataContext.Recipients.Remove(recipient);
        }
    }
}
