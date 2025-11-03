using Microsoft.AspNetCore.Mvc;
using SendsProject.Classes;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IDataContext _dataContext;

        public PackageController(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }
        // GET: api/<PackageController>
        [HttpGet]
        public IEnumerable<Package> Get()
        {
            return _dataContext.Packages;
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var package = _dataContext.Packages.Find(x => x.Id == id);
            if (package != null)
            {
                return Ok(package);
            }
            return NotFound();
        }

        // POST api/<PackageController>
        [HttpPost]
        public ActionResult Post([FromBody] Package value)
        {
            var p =_dataContext.Packages.Find(x => x.Id==value.Id);
            if (p != null)
            {
                return Conflict();
            }
            _dataContext.Packages.Add(value);
            return Ok(value);

        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Package value)
        {
            var index = _dataContext.Packages.FindIndex(x => x.Id == id);
            _dataContext.Packages[index].Id = value.Id;
            _dataContext.Packages[index].Weight = value.Weight;
            _dataContext.Packages[index].SenderName = value.SenderName;
            _dataContext.Packages[index].SendDate = value.SendDate;
            _dataContext.Packages[index].IsSentToRecipient = value.IsSentToRecipient;
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var package = _dataContext.Packages.Find(x => x.Id == id);
            _dataContext.Packages.Remove(package);
        }
    }
}
