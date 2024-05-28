using BloodDonationApp.Models;

namespace BloodDonationApp.Repository.Interfaces
{
    public interface IRecipientRepository
    {
        Task<Recipient> GetRecipientByEmail(string email);
    }
}
