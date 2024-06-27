using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models.DTOs;
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
        [Authorize]
        [Authorize(Roles = "Admin,Recipient")]
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
        [Authorize]
        [Authorize(Roles = "Admin")]
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
        [Authorize]
        [Authorize(Roles = "Donor")]
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

    }
}
