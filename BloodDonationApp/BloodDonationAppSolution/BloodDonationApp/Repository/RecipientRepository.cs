using BloodDonationApp.Context;
using BloodDonationApp.Models;
using Microsoft.EntityFrameworkCore;
using BloodDonationApp.Exceptions.RepositoryException;
using BloodDonationApp.Repository.Interfaces;

namespace BloodDonationApp.Repository
{
    public class RecipientRepository : IRecipientRepository
    {
        private readonly BloodDonationContext _context;

        public RecipientRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task<Recipient> GetRecipientByEmail(string email)
        {
            var recipient = await _context.Recipients
                    .FirstOrDefaultAsync(r => r.Email == email);
            if (recipient == null)
            {
                throw new RecipientNotFoundException("Recipient not found.");
            }
            return recipient;
        }
    }
    
}
