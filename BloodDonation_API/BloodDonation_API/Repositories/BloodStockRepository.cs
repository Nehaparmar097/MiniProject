using Job_Portal_API.Context;
using Job_Portal_API.Exceptions;
using Job_Portal_API.Interfaces;
using Job_Portal_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Job_Portal_API.Repositories
{
    public class BloodStockRepository : IRepository<int, BloodStock>
    {
        private readonly BloodDonationAppContext _context;

        public BloodStockRepository(BloodDonationAppContext context)
        {
            _context = context;
        }

        public async Task<BloodStock> Add(BloodStock entity)
        {
          _context.Add(entity); 
            await _context.SaveChangesAsync();
            return entity;         
        }


        public async Task<BloodStock> Update(BloodStock entity)
        {
            var jobListing = await GetById(entity.ID);
            if (jobListing == null)
            {
                throw new BloodStockNotFoundException();
            }

            _context.Update(entity);
            await _context.SaveChangesAsync(true);
            return entity;
        }

        public async Task<BloodStock> DeleteById(int id)
        {
            var jobListing = await GetById(id);
            if (jobListing == null)
            {
                throw new BloodStockNotFoundException();
            }
            _context.Remove(jobListing);
            await _context.SaveChangesAsync();
            return jobListing;
        }

        public async Task<BloodStock> GetById(int id)
        {
            
            var stock=await _context.BloodStocks.FirstOrDefaultAsync(jl => jl.ID == id);
            return stock;
        }

        public async Task<IEnumerable<BloodStock>> GetAll()
        {
            var results = await _context.BloodStocks.ToListAsync();
            return results;
        }
    }
}
