using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodStockController : ControllerBase
    {
        private readonly IBloodStock _service;

        public BloodStockController(IBloodStock service)
        {
            _service = service;
        }
        [Authorize(Roles = "Donor,Admin")]
        [HttpPost("AddBloodStock")]
        public async Task<IActionResult> AddBloodStock([FromBody] BloodStockDTO jobListingDto)
        {
            if (jobListingDto == null)
            {
                return BadRequest("Invalid Blood stock data.");
            }

            try
            {
                var result = await _service.AddBloodStockAsync(jobListingDto);
                return Ok(result);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                               
            }
            catch (Exception e)
            {
                return NotFound("Donor not found.");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBloodStocks()
        {
            try
            {
                var jobListings = await _service.GetAllBloodStocksAsync();
                return Ok(jobListings);
            }
            catch (BloodStockNotFoundException e)
            {
                return NotFound("No Blood stock found.");
            }
        }
        [HttpGet("by-employer/{donorId}")]
        
        public async Task<IActionResult> GetBloodStocksByEmployerId(int employerId)
        {
            try
            {
                var jobListings = await _service.GetBloodStocksByDonorIdAsync(employerId);
                return Ok(jobListings);
            }
            catch (UserNotFoundException e)
            {
                return NotFound("Donor not found.");
            }
            catch(BloodStockNotFoundException e)
            {
                return NotFound("No Blood stock found.");
            }
        }

    }
}
