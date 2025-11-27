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
            p= _packageService.PostPackage(value);
            return Ok(p);

        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Package value)
        {
            var index = _packageService.GetPackages().FindIndex(x => x.Id == id);
           if (index != -1)
            {
                return Conflict();
            }
           _packageService.PutPackage(value);
            return Ok();
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var p = _packageService.GetPackageById(id);
            if (p != null)
            {
                return Conflict();
            }
            _packageService.DeletePackage(id);
            return Ok();
        }
    }
}
