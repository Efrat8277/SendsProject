using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SendsProject.Core.DTO;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Services;
using SendsProject.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly IDeliveryPersonService _deliveryPersonService;
        private readonly IMapper _mapper;


        public DeliveryPersonController(IDeliveryPersonService deliveryPersonService,IMapper mapper)
        {
            _deliveryPersonService=deliveryPersonService;
            _mapper = mapper;
        }
        // GET: api/<DeliveryPersonController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var deliver = await _deliveryPersonService.GetDeliveryPersonAsync();
            return Ok(_mapper.Map<List<DeliveryPersonDTO>>(deliver));
        }

        // GET api/<DeliveryPersonController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var deliver = await _deliveryPersonService.GetDeliveryPersonByIdAsync(id);
            if(deliver != null)
            {
                var d=_mapper.Map<DeliveryPersonDTO>(deliver);
                return Ok(d);
            }
            return NotFound();
        }

        // POST api/<DeliveryPersonController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DeliveryPersonPostModel value)
        {
            var deliver = await _deliveryPersonService.GetDeliveryPersonByIdAsync(value.DeliveryPersonId);
            if (deliver != null)
            {
                return Conflict();
            }
            var d =_mapper.Map<DeliveryPerson>(value);
            var del = await  _deliveryPersonService.PostDeliveryPersonAsync(d);
            return Ok(del);
        }

        // PUT api/<DeliveryPersonController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DeliveryPerson value)
        {
            var index =await _deliveryPersonService.GetDeliveryPersonByIdAsync(id);
            if (index == null)
            {
                return Conflict();
            }
            await _deliveryPersonService.PostDeliveryPersonAsync(value);
            return Ok();
        }

        // DELETE api/<DeliveryPersonController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deliver = await _deliveryPersonService.GetDeliveryPersonByIdAsync(id);
            if (deliver == null)
            {
                return Conflict();
            }
            await _deliveryPersonService.DeleteDeliveryPersonAsync(id);
            return Ok(deliver);
        }
    }
}
