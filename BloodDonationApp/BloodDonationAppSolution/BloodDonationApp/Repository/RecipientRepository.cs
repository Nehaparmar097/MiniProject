using BloodDonationApp.Context;
using BloodDonationApp.Interfaces;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;

namespace BloodDonationApp.Repository
{
    public class RecipientRepository : IReposiroty<int, Recipient>
    {
        private readonly BloodDonationContext _context;

        public RecipientRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<Recipient> Add(Recipient item)
        {
            try
            {
                _context.Add(item);
                await _context.SaveChangesAsync();
                return item;
            }
            catch (Exception ex)
            {
                throw new RecipientRepositoryException("Error occurred while adding recipient: " + ex.Message, ex);
            }
        }

        public async Task<Recipient> DeleteByKey(int key)
        {
            try
            {
                var recipient = await GetByKey(key);
                _context.Remove(recipient);
                await _context.SaveChangesAsync(true);
                return recipient;
            }
            catch (NotPresentException ex)
            {
                throw new RecipientRepositoryException("Error occurred while deleting recipient: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new RecipientRepositoryException("Error occurred while deleting recipient: " + ex.Message, ex);
            }
        }

        public async Task<IEnumerable<Recipient>> GetAll()
        {
            try
            {
                var recipients = await _context.Recipients.ToListAsync();
                if (recipients.Count <= 0)
                {
                    throw new NotPresentException("There are no recipients present.");
                }
                return recipients;
            }
            catch (NotPresentException ex)
            {
                throw new RecipientRepositoryException("Error occurred while retrieving recipients: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new RecipientRepositoryException("Error occurred while retrieving recipients: " + ex.Message, ex);
            }
        }

        public async Task<Recipient> GetByKey(int key)
        {
            try
            {
                var recipient = await _context.Recipients.FirstOrDefaultAsync(r => r.RecipientId == key);
                if (recipient == null)
                    throw new NotPresentException("No such recipient is present.");
                return recipient;
            }
            catch (NotPresentException ex)
            {
                throw new RecipientRepositoryException("Error occurred while retrieving recipient: " + ex.Message, ex);
            }
            catch (Exception ex)
            {
                throw new RecipientRepositoryException("Error occurred while retrieving recipient: " + ex.Message, ex);
            }
        }

        public async Task<Recipient> Update(Recipient item)
        {
            try
            {
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync(true);
                return item;
            }
            catch (Exception ex)
            {
                throw new RecipientRepositoryException("Error occurred while updating recipient: " + ex.Message, ex);
            }
        }
    }
}
