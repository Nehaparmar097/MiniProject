using BloodDonationApp.Context;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;
using BloodDonationApp.Repository.Interfaces;

namespace BloodDonationApp.Repository
{

    public class DonorRepository : IDonorRepository
    {
        private readonly BloodDonationContext _context;

        public DonorRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<Donor> GetDonorByEmail(string email)
        {
            var donor = await _context.Donors
                    .FirstOrDefaultAsync(d => d.Email == email);
            if (donor == null)
            {
                throw new DonorNotFoundException("Donor not found.");
            }
            return donor;
        }
    }
}
