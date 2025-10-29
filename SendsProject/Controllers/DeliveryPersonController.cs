using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        public static List<DeliveryPerson> delivers = { new DeliveryPerson { DeliveryPersonId = 1, Name = "yoni", Phone = "0588888888", WorkDays = "25" } };
        // GET: api/<DeliveryPersonController>
        [HttpGet]
        public IEnumerable<DeliveryPerson> Get()
        {
            return delivers;
        }

        // GET api/<DeliveryPersonController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var deliver = delivers.Find(x => x.Id == id);
            if(deliver != null)
            {
                return Ok(deliver);
            }
            return NotFound();
        }

        // POST api/<DeliveryPersonController>
        [HttpPost]
        public void Post([FromBody] DeliveryPerson value)
        {
            delivers.Add(value);
        }

        // PUT api/<DeliveryPersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DeliveryPerson value)
        {
            var index = delivers.FindIndex(x => x.Id == id);
            delivers[index].Name=value.Name;
            delivers[index].Phone=value.Phone;
            delivers[index].WorkDays=value.WorkDays;
            delivers[index].StartTime=value.StartTime;
            delivers[index].EndTime = value.EndTime;
        }

        // DELETE api/<DeliveryPersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deliver = delivers.Find(x => x.Id == id);
            delivers.Remove(deliver);
        }
    }
}
