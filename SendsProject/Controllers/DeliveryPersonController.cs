using Microsoft.AspNetCore.Mvc;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly IDeliveryPersonService _deliveryPersonService;

        public DeliveryPersonController(IDeliveryPersonService deliveryPersonService)
        {
            _deliveryPersonService=deliveryPersonService;
        }
        // GET: api/<DeliveryPersonController>
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_deliveryPersonService.GetDeliveryPerson());
        }

        // GET api/<DeliveryPersonController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var deliver = _deliveryPersonService.GetDeliveryPersonById(id);
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
            var deliver = _deliveryPersonService.GetDeliveryPersonById(value.DeliveryPersonId);
            if (deliver != null)
            {
                return Conflict();
            }
            _deliveryPersonService.GetDeliveryPerson().Add(value);
            return Ok(value);
        }

        // PUT api/<DeliveryPersonController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] DeliveryPerson value)
        {
            var index = _deliveryPersonService.GetDeliveryPerson().FindIndex(x=>x.DeliveryPersonId==id);
            _deliveryPersonService.GetDeliveryPerson()[index].Name=value.Name;
            _deliveryPersonService.GetDeliveryPerson()[index].Phone=value.Phone;
            _deliveryPersonService.GetDeliveryPerson()[index].WorkDays=value.WorkDays;
            _deliveryPersonService.GetDeliveryPerson()[index].StartTime=value.StartTime;
            _deliveryPersonService.GetDeliveryPerson()[index].EndTime = value.EndTime;
        }

        // DELETE api/<DeliveryPersonController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var deliver = _deliveryPersonService.GetDeliveryPerson().Find(x => x.DeliveryPersonId == id);
            _deliveryPersonService.GetDeliveryPerson().Remove(deliver);
        }
    }
}
