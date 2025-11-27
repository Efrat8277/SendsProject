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
            deliver =  _deliveryPersonService.PostDeliveryPerson(value);
            return Ok(deliver);
        }

        // PUT api/<DeliveryPersonController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] DeliveryPerson value)
        {
            var index = _deliveryPersonService.GetDeliveryPerson().FindIndex(x=>x.DeliveryPersonId==id);
            if (index == -1)
            {
                return Conflict();
            }
            _deliveryPersonService.PutDeliveryPerson(value);
            return Ok();
        }

        // DELETE api/<DeliveryPersonController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deliver = _deliveryPersonService.GetDeliveryPersonById(id);
            if (deliver != null)
            {
                return Conflict();
            }
            _deliveryPersonService.DeleteDeliveryPerson(id);
            return Ok(deliver);
        }
    }
}
