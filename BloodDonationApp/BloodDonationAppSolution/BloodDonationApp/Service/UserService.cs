using BloodDonationApp.Interfaces;
using BloodDonationApp.Models.DTOs;
using BloodDonationApp.Models;
using System.Security.Cryptography;
using System.Text;
using BloodDonationApp.Context;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.Service
{
    public class UserService : IUserService
    {
        private readonly BloodDonationContext _context;
        private readonly ITokenService _tokenService; // Assuming you have a token service for generating JWT tokens

        public UserService(BloodDonationContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<LoginReturnDTO> Login(UserLoginDTO userLoginDTO)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Id == userLoginDTO.UserId);
            if (user == null || !VerifyPassword(userLoginDTO.Password, user.Password, user.PasswordHashKey))
            {
                return null;
            }

            var token = _tokenService.GenerateToken(user);

            return new LoginReturnDTO
            {
                UserID = user.Id,
                Token = token,
                Role = user.Role.ToString()
            };
        }

        public async Task<UserRegisterReturnDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            if (await _context.Users.AnyAsync(u => u.Email == userRegisterDTO.Email))
            {
                return new UserRegisterReturnDTO { Success = false, Message = "Email already exists" };
            }

            CreatePasswordHash(userRegisterDTO.Password, out byte[] passwordHash, out byte[] passwordHashKey);

            var user = new User
            {
                Name = userRegisterDTO.Name,
                Email = userRegisterDTO.Email,
                Phone = userRegisterDTO.Phone,
                Password = passwordHash,
                PasswordHashKey = passwordHashKey,
                Role = UserRole.User // Default role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserRegisterReturnDTO { Success = true, Message = "Registration successful" };
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordHashKey)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordHashKey = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedHashKey)
        {
            using (var hmac = new HMACSHA512(storedHashKey))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }
    }
}