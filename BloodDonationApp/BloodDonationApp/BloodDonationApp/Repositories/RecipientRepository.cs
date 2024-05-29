using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_API.Repositories
{
    public class RecipientRepository : IRepository<int, Recipient>
    {
        private readonly BloodDonationAppContext _context;

        public RecipientRepository(BloodDonationAppContext context)
        {
            _context = context;
        }

        public async Task<Recipient> Add(Recipient entity)
        {
            
          
            await _context.Recipients.AddAsync(entity);
            await _context.SaveChangesAsync();
            var jobSeeker= await _context.Recipients.FirstOrDefaultAsync(js=>js.UserID==entity.UserID);
            return jobSeeker;
        }

        public async Task<Recipient> Update(Recipient entity)
        {
            var jobSeeker = await _context.Recipients.FindAsync(entity.RecipientID);
            if (jobSeeker == null)
            {
                throw new UserNotFoundException("Recipient Not Found");
            }
            _context.Recipients.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Recipient> DeleteById(int id)
        {
            var jobSeeker = await _context.Recipients.FindAsync(id);
            if (jobSeeker == null)
            {
                throw new UserNotFoundException("Recipient Not Found");
            }
            _context.Recipients.Remove(jobSeeker);
            await _context.SaveChangesAsync();
            return jobSeeker;
        }

        public async Task<Recipient> GetById(int id)
        {
            var jobSeeker = await _context.Recipients
                .Include(js => js.User)
                .Include(js => js.RecipientBloods)
                
                .FirstOrDefaultAsync(js => js.RecipientID == id);

            if (jobSeeker == null)
            {
                throw new RecipientNotFoundException();
            }
            return jobSeeker;
        }

        public async Task<IEnumerable<Recipient>> GetAll()
        {
            return await _context.Recipients.ToListAsync();
        }
    }
}
