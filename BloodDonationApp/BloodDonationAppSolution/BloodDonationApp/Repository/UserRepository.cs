using BloodDonationApp.Context;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;
using BloodDonationApp.Repository.Interfaces;

namespace BloodDonationApp.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BloodDonationContext _context;

        public UserRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<User> GetRecipientUserByEmail(string email)
        {
            var user = await _context.Users
                 .Include(u => u.Recipient)
                 .FirstOrDefaultAsync(u => u.Recipient.Email == email);
            return user;
        }

        public async Task<User> GetDonorUserByEmail(string email)
        {
            var user = await _context.Users
                 .Include(u => u.Donor)
                 .FirstOrDefaultAsync(u => u.Donor.Email == email);
            return user;
        }
    }
}