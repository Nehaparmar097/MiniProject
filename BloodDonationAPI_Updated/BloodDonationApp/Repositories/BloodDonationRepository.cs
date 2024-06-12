using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_API.Repositories
{
    public class BloodDonationRepository : IRepository<int, BloodDonation>
    {
        private readonly BloodDonationAppContext _context;

        public BloodDonationRepository(BloodDonationAppContext context)
        {
            _context = context;
        }

        public async Task<BloodDonation> Add(BloodDonation entity)
        {
            await _context.BloodDonations.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BloodDonation> Update(BloodDonation entity)
        {
            var application = await _context.BloodDonations.FindAsync(entity.BloodDonationID);
            if (application == null)
            {
                throw new BloodDonationNotFoundException();
            }
            _context.BloodDonations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BloodDonation> DeleteById(int id)
        {
            var application = await _context.BloodDonations.FindAsync(id);
            if (application == null)
            {
                throw new BloodDonationNotFoundException();
            }
            _context.BloodDonations.Remove(application);
            await _context.SaveChangesAsync();
            return application;
        }

        public async Task<BloodDonation> GetById(int id)
        {
            var application = await _context.BloodDonations.FindAsync(id);
            if (application == null)
            {
                throw new BloodDonationNotFoundException();
            }
            return application;
        }

        public async Task<IEnumerable<BloodDonation>> GetAll()
        {
            return await _context.BloodDonations.ToListAsync();
        }
    }
}
