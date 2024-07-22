
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using Job_Portal_API.Services;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCors")]
    public class UserController : ControllerBase
    {
        private readonly IUser _service;
       

        public UserController(IUser service)
        {
            _service = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ReturnUserDTO>> Register(RegisterUserDTO user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _service.RegisterUser(user);
                    return Ok(result);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status409Conflict, e.Message);
                }
            }
            return BadRequest("All fields are required!!");
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ReturnLoginDTO>> Login(LoginUserDTO userDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("All fields are required!!");
            }

            try
            {
                var result = await _service.LoginUser(userDTO);
                return Ok(result);
            }
            catch (UnauthorizedUserException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

      //  [Authorize]

       // [Authorize(Roles = "Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<ReturnUserDTO>>> GetAllUsers()
        {
            try
            {
                var result = await _service.GetAllUsers();
                return Ok(result);
            }
            catch (NoUsersFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

      //  [Authorize]
       // [Authorize(Roles = "Admin")]
        [HttpDelete("DeleteUser/{id}")]
        public async Task<ActionResult<ReturnUserDTO>> DeleteUser(int id)
        {
            try
            {
                var result = await _service.DeleteUserById(id);
                return Ok(result);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }

       
        [HttpPost("GetUserById")]
        public async Task<ActionResult<ReturnUserDTO>> GetUserById(int id)
        {
            try
            {
                var result = await _service.GetUserById(id);
                return Ok(result);
            }
            catch (UserNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }


        [HttpPut("UpdateUser/{id}")]
        public async Task<ActionResult<ReturnUserDTO>> PutUser(int id, UpdateUserDTO updateUserDTO)
        {
            if (id != updateUserDTO.UserID)
            {
                return BadRequest("UserID in the URL does not match UserID in the payload.");
            }

            try
            {
                var updatedUser = await _service.UpdateUser(id, updateUserDTO);
                return Ok(updatedUser);
            }
            catch (UserNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
/*
using Job_Portal_API.Context;
using Job_Portal_API.Models;
using Job_Portal_API.Models.DTOs;
using Job_Portal_API.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Job_Portal_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BloodDonationAppContext _context;

        public UserController(BloodDonationAppContext context)
        {
            _context = context;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if email already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == registerUserDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("Email already exists.");
            }

            // Generate password hash and salt
            using var hmac = new HMACSHA512();
            var user = new User
            {
                Email = registerUserDTO.Email,
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Password = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUserDTO.Password)),
                HashKey = hmac.Key,
                ContactNumber = registerUserDTO.ContactNumber,
                UserType = Enum.Parse<UserType>(registerUserDTO.UserType),
                DateOfRegistration = DateTime.Now
            };

            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

            return Ok(new ReturnUserDTO
            {
                UserID = user.UserID,
                Email = user.Email,
                Name = $"{user.FirstName} {user.LastName}",
                ContactNumber = user.ContactNumber,
                Role = user.UserType.ToString()
            });
        }
    }
}
*/