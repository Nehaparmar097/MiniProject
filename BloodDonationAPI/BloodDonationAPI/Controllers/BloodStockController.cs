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
        //[Authorize(Roles = "Donor,Admin")]
        [HttpPost("AddBloodStock")]
        public async Task<IActionResult> AddBloodStock(BloodStockDTO bloodStockDto)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    BloodStockResponseDTO responseDTO = await _service.AddBloodStockAsync(bloodStockDto);
                    return StatusCode(StatusCodes.Status201Created,
                        new
                        {
                            Message = "Blood added in the stock",
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

        //[Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllBloodStocks()
        {
              try
                {
                    IList<BloodStockResponseDTO> responseDTO = await _service.GetAllBloodStocksAsync();
                return StatusCode(StatusCodes.Status201Created, responseDTO);
                }
                catch (Exception ex)
                {
                    return Unauthorized(ex.Message);
                }
            
            
        }
        //[Authorize("Admin")]
        [HttpGet("ByDonorId")]
        public async Task<IActionResult> GetBloodStocksByDonorId(int donorId)
        {
            try
            {
                IList<BloodStockResponseDTO> responseDTO = await _service.GetBloodStocksByDonorIdAsync(donorId);
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[Authorize("Admin")]
        [HttpGet("ByStockId")]
        public async Task<IActionResult> GetBloodStocksByStockId(int stockId)
        {
            try
            {
                BloodStockResponseDTO responseDTO = await _service.GetBloodStockByIdAsync(stockId);
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[Authorize("Recipient")]
        [HttpGet("ByCity")]
        public async Task<IActionResult> GetBloodStocksBycity(string city)
        {
            try
            {
                IList<BloodStockResponseDTO> responseDTO = await _service.GetBloodStocksByCityAsync(city);
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[Authorize("Recipient")]
        [HttpGet("ByState")]
        public async Task<IActionResult> GetBloodStocksBystate(string state)
        {
            try
            {
                IList<BloodStockResponseDTO> responseDTO = await _service.GetBloodStocksByStateAsync(state);
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[Authorize("Recipient")]
        [HttpGet("ByHospital")]
        public async Task<IActionResult> GetBloodStocksByHospital(string hospital)
        {
            try
            {
                IList<BloodStockResponseDTO> responseDTO = await _service.GetBloodStocksByHospitalAsync(hospital);
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        //[Authorize("Admin,Recipient")]
        [HttpGet("Available")]
        public async Task<IActionResult> GetBloodStocksAvailable ()
        {
            try
            {
                IList<BloodStockResponseDTO> responseDTO = await _service.GetBloodStocksByAvailableAsync();
                return StatusCode(StatusCodes.Status201Created, responseDTO);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

    }
}

