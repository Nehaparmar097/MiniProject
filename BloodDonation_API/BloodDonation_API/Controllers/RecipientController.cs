
using System;
using System.Threading.Tasks;
using BloodDonationApp.Exceptions;
using BloodDonationApp.Models.DTOs;
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
            _recipientService = recipientService ?? throw new ArgumentNullException(nameof(recipientService));
        }

        [HttpPost("AddRecipientDetails")]
        public async Task<IActionResult> AddRecipientDetails([FromBody] AddRecipientDTO recipient)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _recipientService.AddRecipientDetails(recipient);
                return Ok(result);
            }
            catch (RecipientServiceException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateAge/{id}")]
        public async Task<IActionResult> UpdateAge(int id, int age)
        {
            try
            {
                var result = await _recipientService.UpdateAge(id, age);
                return Ok(result);
            }
            catch (RecipientServiceException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateRequiredBloodType/{id}")]
        public async Task<IActionResult> UpdateRequiredBloodType(int id, string requiredBloodType)
        {
            try
            {
                var result = await _recipientService.UpdateRequiredBloodType(id, requiredBloodType);
                return Ok(result);
            }
            catch (RecipientServiceException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateBloodRequiredDate/{id}")]
        public async Task<IActionResult> UpdateBloodRequiredDate(int id, DateTime bloodRequiredDate)
        {
            try
            {
                var result = await _recipientService.UpdateBloodRequiredDate(id, bloodRequiredDate);
                return Ok(result);
            }
            catch (RecipientServiceException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("GetAllRecipientsByUserId/{userId}")]
        public async Task<IActionResult> GetAllRecipientsByUserId(int userId)
        {
            try
            {
                var result = await _recipientService.GetAllRecipientsByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetRecipientById/{id}")]
        public async Task<IActionResult> GetRecipientById(int id)
        {
            try
            {
                var result = await _recipientService.GetRecipientById(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetRecipientsByRequiredBloodTypeAndDate")]
        public async Task<IActionResult> GetRecipientsByRequiredBloodTypeAndDate([FromQuery] string requiredBloodType, [FromQuery] DateTime bloodRequiredDate)
        {
            try
            {
                var result = await _recipientService.GetRecipientsByRequiredBloodTypeAndDate(requiredBloodType, bloodRequiredDate);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{userId}/not-in-blood-donations")]
        public async Task<ActionResult<IEnumerable<RecipientResponseDTO>>> GetRecipientsNotInBloodDonations(int userId)
        {
            try
            {
                var recipients = await _recipientService.GetRecipientsNotInBloodDonations(userId);
                return Ok(recipients);
            }
            catch (RecipientServiceException ex)
            {
                // Handle exception accordingly
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("{userId}/in-blood-donations")]
        public async Task<ActionResult<IEnumerable<RecipientResponseDTO>>> GetRecipientsInBloodDonations(int userId)
        {
            try
            {
                var recipients = await _recipientService.GetRecipientsInBloodDonations(userId);
                return Ok(recipients);
            }
            catch (RecipientServiceException ex)
            {
                // Handle exception accordingly
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipientByIdAsync(int id)
        {
            try
            {
                var result = await _recipientService.DeleteRecipientByIdAsync(id);
                if (!result)
                {
                    return NotFound(); // Return 404 Not Found if recipient not found or deletion failed
                }

                return NoContent(); // Return 204 No Content on successful deletion
            }
            catch (RecipientServiceException ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

