using BloodDonationApp.Context;
using BloodDonationApp.Interfaces;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;

namespace BloodDonationApp.Repository
{
    public class BloodDonationRepository : IReposiroty<int, BloodDonation>  
    {
        private readonly BloodDonationContext _context;

        public BloodDonationRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<BloodDonation> Add(BloodDonation item)
        {
            try
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while adding blood donation: " + ex.Message, ex);
            }
        }

        public async Task<BloodDonation> DeleteByKey(int key)
        {
            try
            {
                var bloodDonation = await GetByKey(key);
                _context.Remove(bloodDonation);
                await _context.SaveChangesAsync(true);
                return bloodDonation;
            }
            catch (NotPresentException ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while deleting blood donation: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while deleting blood donation: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<BloodDonation>> GetAll()
        {
            try
            {
                var bloodDonations = await _context.BloodDonations.ToListAsync();
                if (bloodDonations.Count <= 0)
                {
                    throw new NotPresentException("There are no blood donations present.");
                }
                return bloodDonations;
            }
            catch (NotPresentException ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while retrieving blood donations: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while retrieving blood donations: " + ex.Message, ex);
            }
        }

        public async Task<BloodDonation> GetByKey(int key)
        {
            try
            {
                var bloodDonation = await _context.BloodDonations.FirstOrDefaultAsync(bd => bd.BloodDonationId == key);
                if (bloodDonation == null)
                    throw new NotPresentException("No such blood donation is present.");
                return bloodDonation;
            }
            catch (NotPresentException ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while retrieving blood donation: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while retrieving blood donation: " + ex.Message, ex);
            }
        }

        public async Task<BloodDonation> Update(BloodDonation item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync(true);
                return item;
            }
            catch (Exception ex)
            {
                throw new BloodDonationRepositoryException("Error occurred while updating blood donation: " + ex.Message, ex);
            }
        }
    }
}
