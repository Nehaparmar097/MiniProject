using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_API.Repositories
{
    public class RecipientBloodRepository : IRangeRepository<int, RecipientBlood>
    {
        private readonly BloodDonationAppContext _context;

        public RecipientBloodRepository(BloodDonationAppContext context)
        {
            _context = context;
        }

        public async Task<RecipientBlood> Add(RecipientBlood entity)
        {
            await _context.RecipientBloods.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecipientBlood> Update(RecipientBlood entity)
        {
            var jobSeekerSkill = await _context.RecipientBloods.FindAsync(entity.RecipientBloodID);
            if (jobSeekerSkill == null)
            {
                throw new RecipinetBloodNotFoundException();
            }
            _context.RecipientBloods.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<RecipientBlood> DeleteById(int id)
        {
            var jobSeekerSkill = await _context.RecipientBloods.FindAsync(id);
            if (jobSeekerSkill == null)
            {
                throw new RecipinetBloodNotFoundException();
            }
            _context.RecipientBloods.Remove(jobSeekerSkill);
            await _context.SaveChangesAsync();
            return jobSeekerSkill;
        }

        public async Task<RecipientBlood> GetById(int id)
        {
            var jobSeekerSkill = await _context.RecipientBloods.FindAsync(id);
            if (jobSeekerSkill == null)
            {
                throw new RecipinetBloodNotFoundException();
            }
            return jobSeekerSkill;
        }

        public async Task<IEnumerable<RecipientBlood>> GetAll()
        {
            return await _context.RecipientBloods.ToListAsync();
        }

        public async Task<IEnumerable<RecipientBlood>> AddRange(IEnumerable<RecipientBlood> entities)
        {
            // Add a collection of JobSeekerSkill entities to the database
            // and save changes asynchronously
            await _context.RecipientBloods.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            // Return the added entities
            return entities;
        }
    }
}
