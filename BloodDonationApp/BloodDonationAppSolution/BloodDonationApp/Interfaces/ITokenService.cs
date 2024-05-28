using BloodDonationApp.Models;

namespace BloodDonationApp.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
