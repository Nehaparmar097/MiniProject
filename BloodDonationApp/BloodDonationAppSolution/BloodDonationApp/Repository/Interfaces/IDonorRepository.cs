using BloodDonationApp.Models;

namespace BloodDonationApp.Repository.Interfaces
{
    public interface IDonorRepository
    {
        Task<Donor> GetDonorByEmail(string email);
    }
}
