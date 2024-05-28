using BloodDonationApp.Context;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;
using BloodDonationApp.Repository.Interfaces;

namespace BloodDonationApp.Repository
{
    public class HospitalRepository : IReposiroty<int, Hospital>
    {
        private readonly BloodDonationContext _context;

        public HospitalRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<Hospital> Add(Hospital hospital)
        {
            try
            {
                _context.Add(hospital);
                await _context.SaveChangesAsync();
                return hospital;
            }
            catch (Exception ex)
            {
                throw new HospitalRepositoryException("Error occurred while adding hospital: " + ex.Message, ex);
            }
        }

        public async Task<Hospital> DeleteByKey(int key)
        {
            try
            {
                var hospital = await GetByKey(key);
                _context.Remove(hospital);
                await _context.SaveChangesAsync(true);
                return hospital;
            }
            catch (NotPresentException ex)
            {
                throw new HospitalRepositoryException("Error occurred while deleting hospital: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new HospitalRepositoryException("Error occurred while deleting hospital: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Hospital>> GetAll()
        {
            try
            {
                var hospitals = await _context.Hospitals.ToListAsync();
                if (hospitals.Count <= 0)
                {
                    throw new NotPresentException("There are no hospitals present.");
                }
                return hospitals;
            }
            catch (NotPresentException ex)
            {
                throw new HospitalRepositoryException("Error occurred while retrieving hospitals: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new HospitalRepositoryException("Error occurred while retrieving hospitals: " + ex.Message, ex);
            }
        }

        public async Task<Hospital> GetByKey(int key)
        {
            try
            {
                var hospital = await _context.Hospitals.FirstOrDefaultAsync(h => h.HospitalId == key);
                if (hospital == null)
                    throw new NotPresentException("No such hospital is present.");
                return hospital;
            }
            catch (NotPresentException ex)
            {
                throw new HospitalRepositoryException("Error occurred while retrieving hospital: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new HospitalRepositoryException("Error occurred while retrieving hospital: " + ex.Message, ex);
            }
        }

        public async Task<Hospital> Update(Hospital hospital)
        {
            try
            {
                _context.Entry(hospital).State = EntityState.Modified;
                await _context.SaveChangesAsync(true);
                return hospital;
            }
            catch (Exception ex)
            {
                throw new HospitalRepositoryException("Error occurred while updating hospital: " + ex.Message, ex);
            }
        }
    }
}
