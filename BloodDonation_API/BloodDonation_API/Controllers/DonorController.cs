/*using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorController : ControllerBase
    {
        private readonly IDonor _service;

        public DonorController(IDonor service)
        {
            _service = service;
        }
        [Authorize(Roles = "Donor,Admin")]
        [HttpPost("AddDonorDetails")]
        public async Task<ActionResult<ReturnDonorDTO>> CreateDonorDetails(AddDonorDTO employer)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _service.AddDonorDetails(employer);
                    return Ok(result);
                }
                catch (UserNotFoundException e)
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
            }
            return BadRequest("All fields are required!!");
        }
        [Authorize(Roles = "Donor,Admin")]
        [HttpPut("UpdateHospitalDescription/{id}")]
        public async Task<ActionResult<ReturnDonorDTO>> UpdateHospitalDescription(int id, string hospitalDescription)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _service.UpdateHospitalDescription(id, hospitalDescription);
                    return Ok(result);
                }
                catch (UserNotFoundException e)
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
            }
            return BadRequest("All fields are required!!");
        }
        [Authorize(Roles = "Donor,Admin")]
        [HttpPut("UpdateHospitalLocation/{id}")]
        public async Task<ActionResult<ReturnDonorDTO>> UpdateHospitalLocation(int id, string hospitalLocation)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _service.UpdateHospitalLocation(id, hospitalLocation);
                    return Ok(result);
                }
                catch (UserNotFoundException e)
                {
                    return StatusCode(StatusCodes.Status404NotFound, e.Message);
                }
            }
            return BadRequest("All fields are required!!");
        }
    }
}
*/
using System;
using System.Threading.Tasks;
using BloodDonationApp.Exceptions;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class DonorController : ControllerBase
    {
        private readonly IDonor _service;

        public DonorController(IDonor service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

      //  [Authorize]
        //[Authorize(Roles = "Donor")]

        [HttpPost("AddDonorDetails")]
        public async Task<ActionResult<ReturnDonorDTO>> AddDonorDetails(AddDonorDTO donor)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _service.AddDonorDetails(donor);
                    return Ok(result);
                }
                catch (DonorServiceException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("All fields are required!!");
        }

       
        [HttpPut("UpdateAge/{id}")]
        public async Task<ActionResult<ReturnDonorDTO>> UpdateAge(int id, int age)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _service.UpdateAge(id, age);
                    return Ok(result);
                }
                catch (DonorServiceException ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return BadRequest("All fields are required!!");
        }
    }
}
