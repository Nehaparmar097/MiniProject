using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Microsoft.EntityFrameworkCore;
namespace Job_Portal_API.Repositories
{
    public class DonorRepository : IRepository<int, Donor>
    {
        private readonly BloodDonationAppContext _context;

        public DonorRepository(BloodDonationAppContext context)
        {
            _context = context;
        }
        public  async Task<Donor> Add(Donor entity)
        {
            try
            {
                await _context.Donors.AddAsync(entity);
                await _context.SaveChangesAsync();
                var result = _context.Donors.FirstOrDefault(e => e.UserID == entity.UserID);
                return result;
            }
            catch (Exception e)
            {
                throw new UserAlreadyExistException("Donor Already Exist");
            }
          
        }

        public  async Task<Donor> DeleteById(int id)
        {
           var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                throw new UserNotFoundException("Donor Not Found In the Database");
            }
            _context.Donors.Remove(donor);
            await _context.SaveChangesAsync();
            return donor;
        }

        public  async Task<IEnumerable<Donor>> GetAll()
        {
             var donors = await _context.Donors.ToListAsync();
            return donors;
        }

        public async Task<Donor> GetById(int id)
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                throw new UserNotFoundException("Donor Not Exist");
            }
            return donor;
        }

        public async Task<Donor> Update(Donor entity)
        {
            var donor = await _context.Donors.FindAsync(entity.DonorID);
            if (donor == null)
            {
                throw new UserNotFoundException("Donor Not Exist");
            }
            _context.Donors.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
