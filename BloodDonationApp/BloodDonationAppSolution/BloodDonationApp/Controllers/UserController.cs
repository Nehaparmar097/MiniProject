using BloodDonationApp.Models;
using BloodDonationApp.Models.DTOs;
using BloodDonationApp.Repository;
using BloodDonationApp.Service;
using BloodDonationApp.Service.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BloodDonationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        #region RecipientRegister
        [HttpPost("register/recipient")]
        public async Task<IActionResult> RegisterRecipient(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var result = await _userService.RecipientRegister(userRegisterDTO);
                return Ok(result);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (PasswordMismatchException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnableToRegisterException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        #endregion RecipientRegister

        #region RecipientLogin
        [HttpPost("login/recipient")]
        public async Task<IActionResult> LoginRecipient(UserLoginDTO userLoginDTO)
        {
            try
            {
                var result = await _userService.RecipientLogin(userLoginDTO);
                return Ok(result);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (NotAbelToLoginException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        #endregion RecipientLogin

        #region DonorRegister
        [HttpPost("register/donor")]
        public async Task<IActionResult> RegisterDonor(UserRegisterDTO userRegisterDTO)
        {
            try
            {
                var result = await _userService.DonorRegister(userRegisterDTO);
                return Ok(result);
            }
            catch (UserAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (PasswordMismatchException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnableToRegisterException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        #endregion DonorRegister

        #region DonorLogin
        [HttpPost("login/donor")]
        public async Task<IActionResult> LoginDonor(UserLoginDTO userLoginDTO)
        {
            try
            {
                var result = await _userService.DonorLogin(userLoginDTO);
                return Ok(result);
            }
            catch (UnauthorizedUserException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (NotAbelToLoginException ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
        #endregion DonorLogin
    }
}

