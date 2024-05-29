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

            await _context.BloodStocks.AddAsync(entity);
            await _context.SaveChangesAsync();
            var result = await _context.BloodStocks
               .Include(jl => jl.DonorBloods)
               .OrderByDescending(jl => jl.ID)
               .FirstOrDefaultAsync();
            


            //foreach (var skill in result.DonorBlood)
            //{
            //    skill.JobID = result.JobID;
            //    await _context.DonorBlood.AddAsync(skill);
            //    await _context.SaveChangesAsync();
            //}
            return result;
           

           
        }


        public async Task<BloodStock> Update(BloodStock entity)
        {
            var jobListing = await _context.BloodStocks
                 .Include(jl => jl.DonorBloods)
                 .FirstOrDefaultAsync(jl => jl.ID == entity.ID);
            if (jobListing == null)
            {
                throw new BloodStockNotFoundException();
            }

            _context.Entry(jobListing).CurrentValues.SetValues(entity);
            jobListing.DonorBloods.Clear();
            jobListing.DonorBloods = entity.DonorBloods;
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<BloodStock> DeleteById(int id)
        {
            var jobListing = await _context.BloodStocks
                .Include(jl => jl.DonorBloods)
                .FirstOrDefaultAsync(jl => jl.ID == id);
            if (jobListing == null)
            {
                throw new BloodStockNotFoundException();
            }
            _context.BloodStocks.Remove(jobListing);
            await _context.SaveChangesAsync();
            return jobListing;
        }

        public async Task<BloodStock> GetById(int id)
        {
            var jobListing = await _context.BloodStocks
                .Include(jl => jl.DonorBloods)
                .FirstOrDefaultAsync(jl => jl.ID == id);
            if (jobListing == null)
            {
                throw new BloodStockNotFoundException();
            }
            return jobListing;
        }

        public async Task<IEnumerable<BloodStock>> GetAll()
        {
            var results = await _context.BloodStocks
                .Include(jl => jl.DonorBloods)
                .ToListAsync();
            return results;
        }
    }
}
