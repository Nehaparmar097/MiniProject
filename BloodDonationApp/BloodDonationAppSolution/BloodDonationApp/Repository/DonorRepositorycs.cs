using BloodDonationApp.Context;
using BloodDonationApp.Interfaces;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;

namespace BloodDonationApp.Repository
{
    public class DonorRepository : IReposiroty<int, Donor>
    {
        private readonly BloodDonationContext _context;

        public DonorRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<Donor> Add(Donor item)
        {
            try
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new DonorRepositoryException("Error occurred while adding donor: " + ex.Message, ex);
            }
        }

        public async Task<Donor> DeleteByKey(int key)
        {
            try
            {
                var donor = await GetByKey(key);
                _context.Remove(donor);
                await _context.SaveChangesAsync(true);
                return donor;
            }
            catch (NotPresentException ex)
            {
                throw new DonorRepositoryException("Error occurred while deleting donor: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new DonorRepositoryException("Error occurred while deleting donor: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Donor>> GetAll()
        {
            try
            {
                var donors = await _context.Donors.ToListAsync();
                if (donors.Count <= 0)
                {
                    throw new NotPresentException("There are no donors present.");
                }
                return donors;
            }
            catch (NotPresentException ex)
            {
                throw new DonorRepositoryException("Error occurred while retrieving donors: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new DonorRepositoryException("Error occurred while retrieving donors: " + ex.Message, ex);
            }
        }

        public async Task<Donor> GetByKey(int key)
        {
            try
            {
                var donor = await _context.Donors.FirstOrDefaultAsync(d => d.DonorId == key);
                if (donor == null)
                    throw new NotPresentException("No such donor is present.");
                return donor;
            }
            catch (NotPresentException ex)
            {
                throw new DonorRepositoryException("Error occurred while retrieving donor: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new DonorRepositoryException("Error occurred while retrieving donor: " + ex.Message, ex);
            }
        }

        public async Task<Donor> Update(Donor item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync(true);
                return item;
            }
            catch (Exception ex)
            {
                throw new DonorRepositoryException("Error occurred while updating donor: " + ex.Message, ex);
            }
        }
    }
}
