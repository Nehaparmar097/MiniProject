using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodDonationController : ControllerBase
    {
        private readonly IBloodDonation _bloodDonationService;
        public BloodDonationController(IBloodDonation bloodDonationService)
        {
            _bloodDonationService = bloodDonationService;
        }
        //   [Authorize]
        // [Authorize(Roles = "Admin,Recipient")]
        [HttpPost("AddBloodDonation")]
        public async Task<IActionResult> AddBloodDonation(BloodDonationRequestDTO bloodDonationRequestDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    BloodDonationResponseDTO responseDTO = await _bloodDonationService.BloodDonation(bloodDonationRequestDTO);
                    return StatusCode(StatusCodes.Status201Created,
                        new
                        {
                            Message = "Blood successfully donated",
                            responseDTO
                        });
                }
                catch (Exception ex)
                {
                    return Unauthorized(ex.Message);
                }
            }
            else
            {
                return BadRequest("All details are not provided");
            }
        }
        //[Authorize]
        // [Authorize(Roles = "Admin")]
        [HttpGet("GetAllBloodDonation")]
        public async Task<IActionResult> GetAllBloodDonations()
        {
            try
            {
                IList<BloodDonationResponseDTO> responseDTO = await _bloodDonationService.GetAll();
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }


        }
        // [Authorize]
        //[Authorize(Roles = "Donor")]
        [HttpGet("BloodDonatedTo")]
        public async Task<IActionResult> BloodDonatedTo(int DonorId)
        {
            try
            {
                BloodDonationResponseDTO responseDTO = await _bloodDonationService.BloodDonatedTo(DonorId);
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }


        }
        [HttpPost("donate")]
        public async Task<ActionResult<BloodDonationResponseDTO>> DonateBloodToRecipient(int recipientId)
        {
            try
            {
                var donationResult = await _bloodDonationService.DonateBloodToRecipient(recipientId);
                return Ok(donationResult);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



        [HttpPost("Prefered")]
        public async Task<IActionResult> DonateBlood([FromBody] BloodDonationRequestDTO request)
        {
            try
            {
                BloodDonationResponseDTO response = await _bloodDonationService.DonateBloodToRecipientpre(request.RecipientID, request.PreferredState, request.PreferredCity, request.PreferredHospital);
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }
       
        
    [HttpPost("Donatedonate")]
    public async Task<IActionResult> DonateBloodToRecipient([FromBody] BloodDonationRequestDTO request)
    {
        try
        {
            var bloodDonationResponse = await _bloodDonationService.DonateBloodToRecipient(request.RecipientID, request.PreferredState, request.PreferredCity, request.PreferredHospital);
            return Ok(bloodDonationResponse);
        }
        catch (NotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }


}
}
