using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SendsProject.Core.DTO;
using SendsProject.Core.Models.Classes;
using SendsProject.Core.Services;
using SendsProject.Models;
using SendsProject.Service;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SendsProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize (Roles = "recipient,admin")]
    public class RecipientController : ControllerBase
    {

        private readonly IRecipientService _recipientService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public RecipientController(IRecipientService recipientService,IMapper mapper, IUserService userService)
        {
            _recipientService = recipientService;
            _mapper = mapper;
            _userService = userService;
        }

        // GET: api/<RecipientController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var recipients = await _recipientService.GetRecipientsAsync();
            return Ok(_mapper.Map<List<RecipientDTO>>(recipients));
        }

        // GET api/<RecipientController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var recipient = await _recipientService.GetRecipientByIdAsync(id);
            if (recipient != null)
            {
                var rec = _mapper.Map<RecipientDTO>(recipient);
                return Ok(rec);
            }
            return NotFound();
        }

        // POST api/<RecipientController>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] RecipientPostModel value)
        {
            var user = new User { UserName = value.Name, Password = value.Password ,Role=eRole.recipient};
            var recipient = await _recipientService.GetRecipientByIdAsync(value.RecipientId);
            if (recipient != null)
            {
                return Conflict();
            }
            var User= await _userService.AddUserAsync(user);
            var recipient1 = _mapper.Map<Recipient>(value);
            recipient1.User = User;
            recipient1.UserId = User.Id;
            var rec=await _recipientService.PostRecipientAsync(recipient1);
           
            return Ok(rec);
        }

        // PUT api/<RecipientController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] RecipientDTO value)
        {
            var index = await _recipientService.GetRecipientByIdAsync(id);
            if (index == null)
            {
                return Conflict();
            }
            var recipient = _mapper.Map<Recipient>(value);
            await _recipientService.PutRecipientAsync(recipient);
            return Ok();
        }   

        // DELETE api/<RecipientController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var recipient = await _recipientService.GetRecipientByIdAsync(id);
            if (recipient == null)
            {
                return NotFound(new { message = "הנמען לא נמצא במערכת" });
            }

            try
            {
                await _recipientService.DeleteRecipientAsync(id);

                return Ok(recipient);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // 5. הגנה מפני שגיאות לא צ פויות אחרות
                return StatusCode(500, new { message = "שגיאה פנימית בשרת במחיקת הנמען" });
            }
        }
    }
}
