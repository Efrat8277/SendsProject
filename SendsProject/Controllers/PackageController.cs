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
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        private readonly IMapper _mapper;


        public PackageController(IPackageService packageService,IMapper mapper)
        {
            _packageService = packageService;
            _mapper = mapper;
        }
        // GET: api/<PackageController>
        [HttpGet]
        public Task<ActionResult> Get()
        {
            var package = await _packageService.GetPackagesAsync();
             return Ok(_mapper.Map<List<PackageDTO>>(package));
            
        }

        // GET api/<PackageController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var package = await _packageService.GetPackageByIdAsync(id);
            if (package != null)
            {
                var p = _mapper.Map<PackageDTO>(package);
                return Ok(p);
            }
            return NotFound();
        }

        // POST api/<PackageController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] PackagePostModel value)
        {
            var p = await _packageService.GetPackageByIdAsync(value.Id);
            if (p != null)
            {
                return Conflict();
            }
            var package = _mapper.Map<Package>(value);
           var pack = _packageService.PostPackageAsync(package);
            return Ok(pack);

        }

        // PUT api/<PackageController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Package value)
        {
            var index = await _packageService.GetPackageByIdAsync(id);
           if (index == null)
            {
                return Conflict();
            }
          await _packageService.PutPackageAsync(value);
            return Ok();
        }

        // DELETE api/<PackageController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var p = await _packageService.GetPackageByIdAsync(id);
            if (p == null)
            {
                return Conflict();
            }
           await _packageService.DeletePackageAsync(id);
            return Ok();
        }
    }
}
