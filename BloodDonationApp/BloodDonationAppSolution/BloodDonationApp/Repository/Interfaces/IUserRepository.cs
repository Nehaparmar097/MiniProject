using BloodDonationApp.Models;

namespace BloodDonationApp.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetRecipientUserByEmail(string email);
        Task<User> GetDonorUserByEmail(string email);
    }
}
