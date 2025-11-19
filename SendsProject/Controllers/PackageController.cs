using Microsoft.AspNetCore.Mvc;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;

        public PackageController(IPackageService packageService)
        {
            _packageService = packageService;
        }
        // GET: api/<PackageController>
        [HttpGet]
        public ActionResult Get()
        {
             return Ok(_packageService.GetPackages());
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var package = _packageService.GetPackageById(id);
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
            var p =_packageService.GetPackageById(value.Id);
            if (p != null)
            {
                return Conflict();
            }
            _packageService.GetPackages().Add(value);
            return Ok(value);

        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Package value)
        {
            var index = _packageService.GetPackages().FindIndex(x => x.Id == id);
            _packageService.GetPackages()[index].Id = value.Id;
            _packageService.GetPackages()[index].Weight = value.Weight;
            _packageService.GetPackages()[index].SenderName = value.SenderName;
            _packageService.GetPackages()[index].SendDate = value.SendDate;
            _packageService.GetPackages()[index].IsSentToRecipient = value.IsSentToRecipient;
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var package = _packageService.GetPackages().Find(x => x.Id == id);
            _packageService.GetPackages().Remove(package);
        }
    }
}
