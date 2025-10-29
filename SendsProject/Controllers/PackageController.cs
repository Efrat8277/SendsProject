using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        public static List<Package> packages= new List<Package>{ new Package { Id=1, RecipientId = 1, Weight = 3.5, SenderName = "shimshon", SendDate = new DateTime() , IsSentToRecipient =false, DeliveryPersonId =1} };
        // GET: api/<PackageController>
        [HttpGet]
        public IEnumerable<Package> Get()
        {
            return packages;
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var package = packages.Find(x => x.Id == id);
            if (package != null)
            {
                return Ok(package);
            }
            return NotFound();
        }

        // POST api/<PackageController>
        [HttpPost]
        public void Post([FromBody] Package value)
        {
            packages.Add(value);
        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Package value)
        {
            var index = packages.FindIndex(x => x.Id == id);
            packages[index].RecipientId = value.RecipientId;
            packages[index].Weight = value.Weight;
            packages[index].SenderName = value.SenderName;
            packages[index].SendDate = value.SendDate;
            packages[index].IsSentToRecipient = value.IsSentToRecipient;
            packages[index].DeliveryPersonId = value.DeliveryPersonId;
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var package = packages.Find(x => x.Id == id);
            packages.Remove(package);
        }
    }
}
