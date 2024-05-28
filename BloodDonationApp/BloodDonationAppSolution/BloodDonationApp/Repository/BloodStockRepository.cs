using BloodDonationApp.Context;
using BloodDonationApp.Interfaces;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;

namespace BloodDonationApp.Repository
{
    public class BloodStockRepository : IReposiroty<int, BloodStock>
    {
        private readonly BloodDonationContext _context;

        public BloodStockRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<BloodStock> Add(BloodStock item)
        {
            try
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new BloodStockRepositoryException("Error occurred while adding blood stock: " + ex.Message, ex);
            }
        }

        public async Task<BloodStock> DeleteByKey(int key)
        {
            try
            {
                var bloodStock = await GetByKey(key);
                _context.Remove(bloodStock);
                await _context.SaveChangesAsync(true);
                return bloodStock;
            }
            catch (NotPresentException ex)
            {
                throw new BloodStockRepositoryException("Error occurred while deleting blood stock: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BloodStockRepositoryException("Error occurred while deleting blood stock: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<BloodStock>> GetAll()
        {
            try
            {
                var bloodStocks = await _context.BloodStocks.ToListAsync();
                if (bloodStocks.Count <= 0)
                {
                    throw new NotPresentException("There are no blood stocks present.");
                }
                return bloodStocks;
            }
            catch (NotPresentException ex)
            {
                throw new BloodStockRepositoryException("Error occurred while retrieving blood stocks: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BloodStockRepositoryException("Error occurred while retrieving blood stocks: " + ex.Message, ex);
            }
        }

        public async Task<BloodStock> GetByKey(int key)
        {
            try
            {
                var bloodStock = await _context.BloodStocks.FirstOrDefaultAsync(bs => bs.BloodStockId == key);
                if (bloodStock == null)
                    throw new NotPresentException("No such blood stock is present.");
                return bloodStock;
            }
            catch (NotPresentException ex)
            {
                throw new BloodStockRepositoryException("Error occurred while retrieving blood stock: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BloodStockRepositoryException("Error occurred while retrieving blood stock: " + ex.Message, ex);
            }
        }

        public async Task<BloodStock> Update(BloodStock item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync(true);
                return item;
            }
            catch (Exception ex)
            {
                throw new BloodStockRepositoryException("Error occurred while updating blood stock: " + ex.Message, ex);
            }
        }
    }
}
