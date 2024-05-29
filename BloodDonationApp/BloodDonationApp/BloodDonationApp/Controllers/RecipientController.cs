using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipientController : ControllerBase
    {
        private readonly IRecipient _recipientService;

        public RecipientController(IRecipient recipientService)
        {
            _recipientService = recipientService;
        }

        // Endpoint to add a new job seeker experience
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobSeekerById(int id)
        {
            try
            {
                var result = await _recipientService.GetResumeByRecipientId(id);
                return Ok(result);
            }
            catch (RecipientNotFoundException)
            {
                return NotFound(new { message = "Job Seeker not found" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddJobSeekerSkills([FromBody] RecipientBloodDTO recipientSkillDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var addedSkills = await _recipientService.AddReciepientBloodAsync(recipientSkillDto);
                return Ok(addedSkills);

            }
            catch(RecipientNotFoundException e)
            {
                return NotFound("Recipient not found");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
           

            
        }
    }
}
