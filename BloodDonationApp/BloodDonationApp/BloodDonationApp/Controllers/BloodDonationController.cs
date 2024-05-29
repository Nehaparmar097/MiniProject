using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodDonationController : ControllerBase
    {
        private readonly IBloodDonation _applicationService;
        private readonly IBloodStock _jobListingService;
        public BloodDonationController(IBloodDonation applicationService, IBloodStock jobListingService)
        {
            _applicationService = applicationService;
            _jobListingService = jobListingService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> SubmitApplication( int jobSeekerID,int jobID)
        {

            try
            {
                var response = await _applicationService.BlooddonationCompleted(jobID, jobSeekerID);
                return Ok(response);
            }
            catch (BloodStockNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch(RecipientNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (BloodDonationAlreadyExistException e)
            {
                return StatusCode(StatusCodes.Status409Conflict, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

        [HttpGet("GetAllJobs")]
        public async Task<IActionResult> GetAllJobs()
        {
            try
            {
                var response = await _jobListingService.GetAllBloodStocksAsync();
                return Ok(response);
            }
            catch (BloodStockNotFoundException e)
            {
                return NotFound("No job listings found.");
            }
        }
        [HttpPost("GetJobsFilterByLocation")]
        public async Task<IActionResult> GetJobsFilterByLocation( string location)
        {
            try
            {
                var response = await _jobListingService.GetBloodStocksByLocationAsync(location);
                return Ok(response);
            }
            catch (BloodStockNotFoundException e)
            {
                return NotFound("No job listings found.");
            }
        }

        [HttpPost("GetJobsFilterBySalary")]
        public async Task<IActionResult> GetJobsFilterBySalary(double sRange,double eRange)
        {
            try
            {
                var response = await _jobListingService.GetBloodStocksBySalaryAsync(sRange,eRange);
                return Ok(response);
            }
            catch (BloodStockNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost("GetJobsFilterByJobTitle")]
        public async Task<IActionResult> GetJobsFilterByTitle(string jobTitle)
        {
            try
            {
                var response = await _jobListingService.GetBloodStocksByTitleAsync(jobTitle);
                return Ok(response);
            }
            catch (BloodStockNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
       /* [HttpPost("GetJobsFilterByJobType")]
        public async Task<IActionResult> GetJobsFilterByJobType(string jobType)
        {
            try
            {
                var response = await _jobListingService.GetBloodStockByIdAsync(j);
                return Ok(response);
            }
            catch (BloodStockNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(InvalidBloodTypeException e)
            {
                return NotFound($"Invali Job Type {jobType}");
            }
        }*/
        [HttpPost("GetBloodDonationStatus")]
        public async Task<IActionResult> GetBloodDonationStatus(int applicationId)
        {
            try
            {
                var response = await _applicationService.GetBloodDonationStatus(applicationId);
                return Ok(response);
            }
            catch (BloodDonationNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        [HttpPost("GetBloodDonationByRecipientID")]
        public async Task<IActionResult> GetBloodDonationByRecipientID(int jobSeekerID)
        {
            try
            {
                var response = await _applicationService.GetBloodDonationByRecipientID(jobSeekerID);
                return Ok(response);
            }
            catch (BloodDonationNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch(RecipientNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}
