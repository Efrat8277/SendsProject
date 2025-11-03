using Microsoft.AspNetCore.Mvc;
using SendsProject.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly IDataContext _dataContext;

        public DeliveryPersonController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        // GET: api/<DeliveryPersonController>
        [HttpGet]
        public IEnumerable<DeliveryPerson> Get()
        {
            return _dataContext.DeliveryPeople;
        }

        // GET api/<DeliveryPersonController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var deliver = _dataContext.DeliveryPeople.Find(x => x.DeliveryPersonId == id);
            if(deliver != null)
            {
                return Ok(deliver);
            }
            return NotFound();
        }

        // POST api/<DeliveryPersonController>
        [HttpPost]
        public ActionResult Post([FromBody] DeliveryPerson value)
        {
            var deliver = _dataContext.DeliveryPeople.Find(x => x.DeliveryPersonId == value.DeliveryPersonId);
            if (deliver != null)
            {
                return Conflict();
            }
            _dataContext.DeliveryPeople.Add(value);
            return Ok(value);
        }

        // PUT api/<DeliveryPersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DeliveryPerson value)
        {
            var index = _dataContext.DeliveryPeople.FindIndex(x => x.DeliveryPersonId == id);
            _dataContext.DeliveryPeople[index].Name=value.Name;
            _dataContext.DeliveryPeople[index].Phone=value.Phone;
            _dataContext.DeliveryPeople[index].WorkDays=value.WorkDays;
            _dataContext.DeliveryPeople[index].StartTime=value.StartTime;
            _dataContext.DeliveryPeople[index].EndTime = value.EndTime;
        }

        // DELETE api/<DeliveryPersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deliver = _dataContext.DeliveryPeople.Find(x => x.DeliveryPersonId == id);
            _dataContext.DeliveryPeople.Remove(deliver);
        }
    }
}
